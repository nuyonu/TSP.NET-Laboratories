using AutoMapper;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PostCommentDatabase;
using PostCommentDatabase.Exceptions;
using PostCommentDatabase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcPostComment.Services
{
    public class PostCommentService : PostComment.PostCommentBase
    {
        private readonly ILogger<PostCommentService> _logger;
        private readonly IMapper _mapper;
        public PostCommentService(ILogger<PostCommentService> logger)
        {
            _logger = logger;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DateTime, Timestamp>().ConvertUsing(d => d.ToUniversalTime().ToTimestamp());
                cfg.CreateMap<Post, PostResponse>();
                cfg.CreateMap<Comment, CommentResponse>().ConvertUsing(c => ConvertToCommentResponse(c));
                cfg.CreateMap<List<Comment>, RepeatedField<CommentResponse>>().ConvertUsing(r => ConvertCommentsToRepeteadField(r));
            });
            configuration.AssertConfigurationIsValid();
            _mapper = configuration.CreateMapper();
        }

        public override Task<PostResponse> AddPost(PostRequest request, ServerCallContext context)
        {
            
            Post postToAdd = Post.Create(request.Description, request.Domain, DateTime.Now);
            if (request.Comments.Count > 0)
            {
                foreach (var comment in request.Comments)
                {
                    postToAdd.AddComment(Comment.Create(comment.Text));
                }
            }

            Post post = PostCommentAPI.AddPost(postToAdd);

            //PostResponse postResponse = _mapper.Map<PostResponse>(post);

            PostResponse postResponse = new PostResponse()
            {
                PostId = post.PostId.ToString(),
                Domain = post.Domain,
                Description = post.Description,
                Date = DateTimeOffset.Now.ToTimestamp()
            };

            foreach (var comment in post.Comments)
            {
                postResponse.Comments.Add(new CommentResponse() { CommentId = comment.CommentId.ToString(), Text = comment.Text });
            }

            return Task.FromResult(postResponse);
        }
        public override Task<UniversalResponse> DeletePostById(Id request, ServerCallContext context)
        {
            try
            {
                PostCommentAPI.DeletePost(Guid.Parse(request.Id_));
            }
            catch
            {
                return Task.FromResult(new UniversalResponse() { 
                    Succes = false, 
                    ErrorMessage = String.Format("Not found post with id {0}", request.Id_) 
                });
            }

            return Task.FromResult(new UniversalResponse() { Succes = true });
        }

        public override Task<PostResponse> GetPostById(Id request, ServerCallContext context)
        {
            Post post = PostCommentAPI.GetPostById(Guid.Parse(request.Id_));
            if(post != null)
                return Task.FromResult(ConvertPostToPostResponse(post));

            return Task.FromResult(new PostResponse());
        }
        public override Task<PostsRespose> GetAllPosts(Empty request, ServerCallContext context)
        {
            PostsRespose postsRespose = new PostsRespose();

            foreach(var post in PostCommentAPI.GetAllPosts())
                postsRespose.Posts.Add(ConvertPostToPostResponse(post));

            return Task.FromResult(postsRespose);
        }

        private CommentResponse ConvertToCommentResponse(Comment comment)
        {
            return new CommentResponse
            {
                CommentId = comment.CommentId.ToString(),
                Text = comment.Text
            };
        }

        private RepeatedField<CommentResponse> ConvertCommentsToRepeteadField(List<Comment> comments)
        {
            RepeatedField<CommentResponse> result = new RepeatedField<CommentResponse>();

            foreach(var comment in comments)
            {
                result.Add(ConvertToCommentResponse(comment));
            }
            return result;
        }

        private PostResponse ConvertPostToPostResponse(Post post)
        {
            PostResponse postResponse = new PostResponse()
            {
                PostId = post.PostId.ToString(),
                Domain = post.Domain,
                Description = post.Description,
                Date = DateTimeOffset.Now.ToTimestamp()
            };
            foreach (var comment in post.Comments)
            {
                postResponse.Comments.Add(new CommentResponse() { CommentId = comment.CommentId.ToString(), Text = comment.Text });
            }
            return postResponse;
        }
    }
}
