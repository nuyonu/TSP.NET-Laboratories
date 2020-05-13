using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;

namespace WebApp
{
    public class IndexModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();
        public List<PostDTO> Posts { get; set; }

        public IndexModel()
        {
            Posts = new List<PostDTO>();
        }
        public async Task OnGetAsync()
        {
            Posts = await pcc.GetAllPostsAsync();
        }

    }
}