using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;

namespace WebApp
{
    public class DeleteModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();
        [BindProperty]
        public PostDTO PostDTO { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();
            PostDTO = await pcc.GetPostByIdAsync(id.Value);
            if (PostDTO != null)
                return Page();
            else
                return NotFound();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int result = 0;
            try
            {
                result = await pcc.DeletePostAsync(id.Value);
                // result ar trebui valorificat mai departe in cod...
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("./Index");
        }

    }
}