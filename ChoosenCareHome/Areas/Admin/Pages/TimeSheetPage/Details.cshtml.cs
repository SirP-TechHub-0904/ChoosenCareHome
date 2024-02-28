using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public TimeSheet TimeSheet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheet = await _context.TimeSheets
                .Include(x => x.UserTimeSheet).ThenInclude(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (timesheet == null)
            {
                return NotFound();
            }
            else
            {
                TimeSheet = timesheet;
            }

            // Filter users to exclude the one associated with the timesheet
            var excludedUserId = timesheet.UserTimeSheet.FirstOrDefault()?.UserId;
            ViewData["UserId"] = new SelectList(_context.Users.Where(x => x.Email != "info@chosenhealthcare.co.uk" && x.Id != excludedUserId), "Id", "Email");

            return Page();
        }


        [BindProperty]
        public UserTimeSheet UserTimeSheet { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            

            _context.UserTimeSheets.Add(UserTimeSheet);
            await _context.SaveChangesAsync();
            TempData["success"] = "successful";
            return RedirectToPage("./Details", new {id = UserTimeSheet.TimeSheetId});
        }
    }
}
