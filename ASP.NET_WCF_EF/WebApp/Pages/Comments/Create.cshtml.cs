using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;

namespace WebApp.Pages.Comments
{
    public class CreateModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();
        public CreateModel()
        {
            CommentDTO = new CommentDTO();
        }
        [BindProperty]
        public CommentDTO CommentDTO { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var itemPost = await pcc.GetPostByIdAsync(id.Value);
                ViewData["id"] = id.Value.ToString() + " : " + itemPost.Description;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var itemPost = await pcc.GetPostByIdAsync(id.Value);
            CommentDTO.PostPostId = itemPost.PostId;
            CommentDTO.Post = itemPost;
            var result = await pcc.SubmitCommentAsync(CommentDTO);
            if (result == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToPage("/Posts/Index");
        }

    }
}