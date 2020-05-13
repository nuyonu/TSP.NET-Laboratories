using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;

namespace WebApp
{
    public class CreateModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();

        [BindProperty]
        public PostDTO PostDTO { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await pcc.SubmitPostAsync(PostDTO);
            if (result == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToPage("./Index");
        }
    }
}