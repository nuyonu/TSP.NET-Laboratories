using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;

namespace WebApp
{
    public class ListModel : PageModel
    {
            PostCommentClient pcc = new PostCommentClient();
            public List<CommentDTO> Comments { get; set; }
            public ListModel()
            {
                Comments = new List<CommentDTO>();
            }
            public async Task OnGetAsync(int? id)
            {
                if (!id.HasValue)
                    return;
                var item = await pcc.GetPostByIdAsync(id.Value);
                ViewData["Post"] = item.PostId.ToString() + " : " + item.Description.Trim();
                Comments = item.Comments;
            }
    }
}