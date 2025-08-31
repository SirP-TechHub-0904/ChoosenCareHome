using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChoosenCareHome.Pages.Data
{
    public class BookAppointmentModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public BookAppointmentModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;


        // Honeypot property
        [BindProperty]
        public string HoneyPot { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // 🚫 Bot check
            if (!string.IsNullOrEmpty(HoneyPot))
            {
                ModelState.AddModelError(string.Empty, "Invalid submission detected.");
                return Page();
            }
            //if (!ModelState.IsValid || _context.Appointments == null || Appointment == null)
            //{
            //    return Page();
            //}

            _context.Appointments.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./AppointmentSuccess");
        }
    }

}
