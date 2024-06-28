using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class TimesheetDetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public TimesheetDetailsModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UserTimeSheet> UserTimeSheets { get; set; }
        public Profile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            UserProfile = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (UserProfile == null)
            {
                return NotFound();
            }

            UserTimeSheets = await _context.UserTimeSheets
                .Include(x=>x.TimeSheet)
                .Where(uts => uts.UserId == id && uts.Paid == false)
                .ToListAsync();

            return Page();
        }
    }

}
