using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Staff.Pages.Dashboard
{
     [Authorize]
    public class TimeSheetModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public TimeSheetModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UserTimeSheet> TimeSheet { get; set; }
 
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            TimeSheet = await _context.UserTimeSheets
                .Include(x => x.TimeSheet)
                .Where(x => x.UserId == user.Id).ToListAsync();
 
        }
    }
}
