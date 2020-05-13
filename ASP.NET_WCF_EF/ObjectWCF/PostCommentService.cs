using AutoMapper;
using PostComment;
using System.Collections.Generic;
using System.ServiceModel;

namespace ObjectWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServicePostComment : IPostComment
    {
        MapperConfiguration config;
        IMapper iMapper;
        public ServicePostComment()
        {
            config = new MapperConfiguration(
            cfg => {
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Comment, CommentDTO>();
                cfg.CreateMap<PostDTO, Post>();
                cfg.CreateMap<CommentDTO, Comment>();
            });
            iMapper = config.CreateMapper();
        }
        public void Cleanup()
        {
            Cleanup();
        }
        public List<PostDTO> GetAllPosts()
        {
            var lp = API.GetAllPosts();
            List<PostDTO> lpDto = new List<PostDTO>();
            lpDto = iMapper.Map<List<Post>, List<PostDTO>>(lp);
            return lpDto;
        }
        public void DeleteComment(CommentDTO comment)
        {
            Comment comm = new Comment();
            comm = iMapper.Map<CommentDTO, Comment>(comment);
            API.DeleteComment(comm);
        }
        public PostDTO GetPostByTitle(string title)
        {
            Post post = API.GetPostByTitle(title);
            if (post != null)
            {
                PostDTO postDTO = iMapper.Map<Post, PostDTO>(post);
                return postDTO;
            }
            return null;
        }
        public PostDTO GetPostById(int id)
        {
            Post post = API.GetPostById(id);
            PostDTO postDTO = iMapper.Map<Post, PostDTO>(post);
            return postDTO;
        }
        public PostDTO SubmitPost(PostDTO postDTO)
        {
            var post = iMapper.Map<PostDTO, Post>(postDTO);
            post = API.SubmitPost(post);
            postDTO = iMapper.Map<Post, PostDTO>(post);
            return postDTO;
        }
        public PostDTO UpdatePost(PostDTO newPost)
        {
            Post post = iMapper.Map<PostDTO, Post>(newPost);
            post = API.UpdatePost(post);
            PostDTO postDTO = iMapper.Map<Post, PostDTO>(post);
            return postDTO;
        }
        public int DeletePost(int postId)
        {
            return API.DeletePost(postId);
        }
        public CommentDTO GetCommentById(int id)
        {
            Comment comment = API.GetCommentById(id);
            CommentDTO commentDTO = iMapper.Map<Comment, CommentDTO>(comment);
            return commentDTO;
        }
        public CommentDTO SubmitComment(CommentDTO commentDTO)
        {
            Comment comment = iMapper.Map<CommentDTO, Comment>(commentDTO);
            comment = API.SubmitComment(comment);
            CommentDTO commDTO = iMapper.Map<Comment, CommentDTO>(comment);
            return commDTO;
        }
        public CommentDTO SubmitComment(int postId, CommentDTO commentDTO)
        {
            Comment comment = iMapper.Map<CommentDTO, Comment>(commentDTO);
            comment = API.SubmitComment(postId, comment);
            CommentDTO comm = iMapper.Map<Comment, CommentDTO>(comment);
            return comm;
        }
        public CommentDTO UpdateComment(CommentDTO oldCommentDTO,
       CommentDTO newCommentDTO)
        {
            Comment oldComment = iMapper.Map<CommentDTO, Comment>(oldCommentDTO);
            Comment newComment = iMapper.Map<CommentDTO, Comment>(newCommentDTO);
            Comment comment = API.UpdateComment(oldComment, newComment);
            CommentDTO comm = iMapper.Map<Comment, CommentDTO>(comment);
            return comm;
        }
        public bool DeleteComment(int commentId)
        {
            return API.DeleteComment(commentId);
        }
        List<PostDTO> ILoadData.GetAllPostsAndRelatedComments()
        {
            return GetAllPosts();
        }
    }
}
