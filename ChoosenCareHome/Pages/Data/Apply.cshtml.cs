using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
     public class ApplyModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public ApplyModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Application Application { get; set; } = default!;
        
        // Honeypot property
        [BindProperty]
        public string HoneyPot { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // 🚫 Bot check
            if (!string.IsNullOrEmpty(HoneyPot))
            {
                // Looks like a bot → just ignore and reload page
                ModelState.AddModelError(string.Empty, "Invalid submission detected.");
                return Page();
            }

            Application.Date = DateTime.UtcNow.AddHours(1);
            _context.Applications.Add(Application);
            await _context.SaveChangesAsync();

            var getap = await _context.Applications.FindAsync(Application.Id);
            if (getap != null)
            {
                getap.IdNumber = "CHC-01" + Application.Id.ToString("0000");
                _context.Attach(getap).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./SuccessFeedback", new { id = Application.Id });
        }

         
    }

}
