using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PostComment
{
    [DataContract(IsReference = true)]
    public partial class CommentDTO
    {
        [DataMember]
        public int CommentId { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public int PostPostId { get; set; }
        [DataMember]
        public virtual PostDTO Post { get; set; }
    }
    [DataContract]
    public partial class PostDTO
    {
        public PostDTO()
        {
            this.Comments = new List<CommentDTO>();
        }
        [DataMember]
        public int PostId { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Domain { get; set; }
        [DataMember]
        public System.DateTime Date { get; set; }
        [DataMember]
        public virtual List<CommentDTO> Comments { get; set; }
    }
}
