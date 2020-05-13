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
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                bool bResult = false;
                if (post.PostId == 0)
                {
                    var it = ctx.Entry<Post>(post).State = EntityState.Added;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }
        public static Post UpdatePost(Post newPost)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                // Ce e in bd. PK nu poate fi modificata
                Post oldPost = ctx.Posts.Find(newPost.PostId);
                if (oldPost == null) // nu exista in bd
                {
                    return null;
                }
                oldPost.Description = newPost.Description;
                oldPost.Domain = newPost.Domain;
                oldPost.Date = newPost.Date;
                ctx.SaveChanges();
                return oldPost;
            }
        }
        public static int DeletePost(int id)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                return ctx.Database.ExecuteSqlCommand("Delete From Post where postid =@p0", id);
            }
        }
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
        public static List<Post> GetAllPosts()
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                return ctx.Posts.Include("Comments").ToList<Post>();
            }
        }

        public static Post SubmitPost(Post post)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                bool bResult = false;
                if (post.PostId == 0)
                {
                    var it = ctx.Entry<Post>(post).State = EntityState.Added;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return post;
            }
        }
        public static bool AddComment(Comment comment)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                bool bResult = false;
                if (comment == null || comment.PostPostId == 0)
                    return bResult;
                if (comment.CommentId == 0)
                {
                    ctx.Entry<Comment>(comment).State = EntityState.Added;
                    Post p = ctx.Posts.Find(comment.PostPostId);
                    ctx.Entry<Post>(p).State = EntityState.Unchanged;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }

        public static Comment SubmitComment(int postId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public static Comment SubmitComment(Comment comment)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                bool bResult = false;
                if (comment == null || comment.PostPostId == 0)
                    return null;
                if (comment.CommentId == 0)
                {
                    ctx.Entry<Comment>(comment).State = EntityState.Added;
                    Post p = ctx.Posts.Find(comment.PostPostId);
                    ctx.Entry<Post>(p).State = EntityState.Unchanged;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return comment;
            }
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
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                if (newComment.Text != null)
                    oldComment.Text = newComment.Text;
                if ((oldComment.PostPostId != newComment.PostPostId)
               && (newComment.PostPostId != 0))
                {
                    oldComment.PostPostId = newComment.PostPostId;
                }
                ctx.SaveChanges();
                return oldComment;
            }
        }

        public static Comment UpdateComment(Comment newComment)
        {
            using (ModelPostCommentContainer ctx = new ModelPostCommentContainer())
            {
                Comment oldComment = ctx.Comments.Find(newComment.CommentId);
                if (newComment.Text != null)
                    oldComment.Text = newComment.Text;
                if ((oldComment.PostPostId != newComment.PostPostId)
               && (newComment.PostPostId != 0))
                {
                    oldComment.PostPostId = newComment.PostPostId;
                }
                ctx.SaveChanges();
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
