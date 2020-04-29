using Microsoft.EntityFrameworkCore;
using PostCommentDatabase.Exceptions;
using PostCommentDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostCommentDatabase
{
    public class PostCommentAPI
    {
        public static Post AddPost(Post post)
        {
            using PostCommentContext context = new PostCommentContext();
            context.Posts.Add(post);
            context.SaveChanges();
            return post;
        }
        public static Post UpdatePost(Post newPost)
        {
            throw new NotImplementedException();
        }
        public static void DeletePost(Guid id)
        {
            using PostCommentContext context = new PostCommentContext();
            Post post = GetPostById(id);
            if (post == null)
                throw new NotFoundException();
            context.Posts.Remove(post);
            context.SaveChanges();
        }
        public static Post GetPostById(Guid id)
        {
            using PostCommentContext context = new PostCommentContext();
            return context.Posts.Where(p => p.PostId == id).FirstOrDefault();
        }
        public static List<Post> GetAllPosts()
        {
            using PostCommentContext context = new PostCommentContext();
            return context.Posts.Include(p => p.Comments).ToList();
        }
        public static Comment AddComment(Comment comment)
        {
            using PostCommentContext context = new PostCommentContext();
            context.Comments.Add(comment);
            context.SaveChanges();
            return comment;
        }
        public static Comment UpdateComment(Comment newComment)
        {
            throw new NotImplementedException();
        }
        public static Comment GetCommentById(Guid id)
        {
            using PostCommentContext context = new PostCommentContext();
            return context.Comments.Where(c => c.CommentId == id).FirstOrDefault();
        }
    }
}
