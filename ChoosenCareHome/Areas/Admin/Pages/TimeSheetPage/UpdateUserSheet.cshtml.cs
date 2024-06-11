using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class UpdateUserSheetModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public UpdateUserSheetModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public UserTimeSheet UserTimeSheet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserTimeSheets == null)
            {
                return NotFound();
            }

            var usertimesheet = await _context.UserTimeSheets
                .Include(x => x.TimeSheet)
                .Include(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usertimesheet == null)
            {
                return NotFound();
            }
            else
            {
                UserTimeSheet = usertimesheet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
             
            var usertimesheet = await _context.UserTimeSheets
               .Include(x=>x.TimeSheet)
                .FirstOrDefaultAsync(m => m.Id == UserTimeSheet.Id);
            if (usertimesheet == null)
            {
                return NotFound();
            }

            usertimesheet.StartTime = UserTimeSheet.StartTime;
            usertimesheet.EndTime = UserTimeSheet.EndTime;
            usertimesheet.Address = UserTimeSheet.Address;
            usertimesheet.PostCode = UserTimeSheet.PostCode;
            usertimesheet.RatePerHour = UserTimeSheet.RatePerHour;
            usertimesheet.Break = UserTimeSheet.Break;


            _context.Attach(usertimesheet).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            

            return RedirectToPage("./Details", new {date = usertimesheet.TimeSheet.Date.ToString("dd/MM/yyyy") });
        }
    }
}
