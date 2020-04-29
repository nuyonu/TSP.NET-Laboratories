using System;

namespace PostCommentDatabase.Models
{
    public class Comment
    {
        protected Comment()
        { }
        public Guid CommentId { get; private set; }
        public string Text { get; private set; }

        public Post Post { get; set; }

        public static Comment Create(string text)
        {
            return new Comment()
            {
                CommentId = Guid.NewGuid(),
                Text = text,
            };
        }
    }
}
