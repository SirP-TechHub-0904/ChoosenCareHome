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
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public TimeSheet TimeSheet { get; set; } = default!;
        public string Date { get; set; }
        public DateTime DateView { get; set; }
        public async Task<IActionResult> OnGetAsync(string? date)
        {
            Date = date;

            //if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            //{
            //    return NotFound();
            //}
            DateTime xdate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateView = xdate;

            var timesheet = await _context.TimeSheets
                .Include(x => x.UserTimeSheet).ThenInclude(x => x.User)
.FirstOrDefaultAsync(m => m.Date.Date == xdate.Date);
            if (timesheet == null)
            {
                TempData["error"] = "Timesheet Not Available";
                return Page();
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

            UserTimeSheet.Date = DateTime.UtcNow.AddHours(1);
            _context.UserTimeSheets.Add(UserTimeSheet);
            await _context.SaveChangesAsync();
            TempData["success"] = "successful";
            var timesheet = await _context.TimeSheets.FirstOrDefaultAsync(x=>x.Id == UserTimeSheet.TimeSheetId);
            return RedirectToPage("./Details", new { date = timesheet.Date.ToString("dd/MM/yyyy") });

         }
    }
}
