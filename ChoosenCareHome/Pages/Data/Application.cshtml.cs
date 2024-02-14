using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChoosenCareHome.Pages.Data
{
        public class ApplicationModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public ApplicationModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Application Application { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Applications == null || Application == null)
            {
                return Page();
            }

            _context.Applications.Add(Application);
            await _context.SaveChangesAsync();

            return RedirectToPage("./BookAppointment", new {id = Application.Id});
        }
    }

}
