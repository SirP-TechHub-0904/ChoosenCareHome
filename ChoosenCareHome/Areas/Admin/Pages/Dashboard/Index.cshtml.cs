using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public IndexModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public int Appointment { get; set; } 
        public int Application { get; set; }

        public async Task OnGetAsync()
        {
             
                Appointment = await _context.Appointments.CountAsync();
            Application = await _context.Applications.CountAsync();
             
        }
    }
}
