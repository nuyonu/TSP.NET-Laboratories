using PostComment;
using System.Collections.Generic;
using System.ServiceModel;

namespace ObjectWCF
{
    [ServiceContract]
    public interface IPost
    {
        [OperationContract]
        void Cleanup();
        [OperationContract]
        PostDTO GetPostById(int id);
        [OperationContract]
        PostDTO GetPostByTitle(string title);
        // Insert, Update, Delete Post
        [OperationContract]
        PostDTO SubmitPost(PostDTO post);
        [OperationContract]
        PostDTO UpdatePost(PostDTO newPost);
        [OperationContract]
        int DeletePost(int postId);
        [OperationContract]
        List<PostDTO> GetAllPosts();
    }
    [ServiceContract]
    public interface IComment
    {
        // Insert, Update, Delete Comment
        [OperationContract]
        CommentDTO GetCommentById(int id);
        [OperationContract]
        CommentDTO SubmitComment(CommentDTO comment);
        [OperationContract(Name = "AddCommment")]
        CommentDTO SubmitComment(int postId, CommentDTO comment);
        [OperationContract]
        CommentDTO UpdateComment(CommentDTO oldComment, CommentDTO newComment);
        [OperationContract]
        bool DeleteComment(int commentId);
    }
    [ServiceContract]
    public interface ILoadData
    {
        [OperationContract]
        List<PostDTO> GetAllPostsAndRelatedComments();
    }
    [ServiceContract]
    public interface IPostComment : IPost, IComment, ILoadData
    { }
}
