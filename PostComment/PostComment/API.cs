using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PostComment
{
    public static class API
    {
        public static bool AddPost(Post post)
        {
            return post.AddPost() != null;
        }
        public static Post UpdatePost(Post newPost)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                Post oldPost = ctx.Posts.Find(newPost.PostId);
                return oldPost?.UpdatePost(newPost);
            }
        }
        public static int DeletePost(int id)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                return ctx.Database.ExecuteSqlCommand("Delete From Post where postid =@p0", id);
            }
        }
        /// <summary>
        /// Returnez un Post si toate Comment-urile asociate lui
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Post GetPostById(int id)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                var items = from p in ctx.Posts where (p.PostId == id) select p;
                if (items != null)
                    return items.Include(c => c.Comments).SingleOrDefault();
                return null;
            }
        }

        public static void DeleteComment(Comment comm)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                ctx.Comments.Remove(comm);
                ctx.SaveChanges();
            }
        }

        public static Post GetPostByTitle(string title)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                return ctx.Posts.Where(c => c.Domain == title).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returnez toate Post-urile si Comment-urile corespunzatoare
        /// </summary>
        /// <returns></returns>
        public static List<Post> GetAllPosts()
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                return ctx.Posts.Include("Comments").ToList<Post>();
            }
        }

        public static Post SubmitPost(Post post)
        {
            return post.AddPost();
        }

        // Comment table
        public static bool AddComment(Comment comment)
        {
            return comment.AddComment() != null;
        }

        public static Comment SubmitComment(int postId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public static Comment SubmitComment(Comment comment)
        {
            return comment.AddComment();
        }

        public static bool DeleteComment(int commentId)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                return ctx.Database.ExecuteSqlCommand("Delete From Comment where postid = @p0", commentId) > 0;
            }
        }

        public static Comment UpdateComment(Comment oldComment, Comment newComment)
        {
            return oldComment.UpdateComment(newComment);
        }

        public static Comment UpdateComment(Comment newComment)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                Comment oldComment = ctx.Comments.Find(newComment.CommentId);
                if (newComment.Text != null)
                    return oldComment.UpdateComment(newComment);
                else
                    return oldComment;
            }
        }
        public static Comment GetCommentById(int id)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                var items = from c in ctx.Comments where (c.CommentId == id) select c;
                return items.Include(p => p.Post).SingleOrDefault();
            }
        }
    }
}
