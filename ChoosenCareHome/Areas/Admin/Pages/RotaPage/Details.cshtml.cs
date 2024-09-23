using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using System.Globalization;

namespace ChoosenCareHome.Areas.Admin.Pages.RotaPage
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public UserRota UserRota { get; set; } = default!;
        public List<UserTimeSheet> UserTimeSheets { get; set; } = default!;
        public string DateTitle { get; set; } = string.Empty; 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
       

        public async Task<IActionResult> OnGetAsync(int? id, DateTime? startDate, DateTime? endDate)
        {
            if (id == null || _context.UserRotas == null)
            {
                return NotFound();
            }

            var userclient = await _context.UserRotas.FirstOrDefaultAsync(m => m.Id == id);
            if (userclient == null)
            {
                return NotFound();
            }

            UserRota = userclient;

            IQueryable<UserTimeSheet> query = _context.UserTimeSheets
                .Include(x => x.User)
                .Include(x => x.TimeSheet)
                .Where(m => m.PostCode == userclient.PostCode && m.TimesheetAcceptance == ChoosenCareHome.Data.Model.Enum.TimesheetAcceptance.Accepted);

            // Default to "all" dates if none are specified
            if (!startDate.HasValue && !endDate.HasValue)
            {
                StartDate = null; // no filter (all records)
                EndDate = null;   // no filter (all records)
                DateTitle = "All Records";
            }
            else
            {
                // If only one date is provided, assume today for both start and end dates
                if (!startDate.HasValue) startDate = DateTime.Today;
                if (!endDate.HasValue) endDate = DateTime.Today;

                StartDate = startDate;
                EndDate = endDate;

                query = query.Where(m => m.TimeSheet.Date >= startDate && m.TimeSheet.Date <= endDate);
                DateTitle = $"From {startDate.Value.ToString("dd MMM yyyy")} to {endDate.Value.ToString("dd MMM yyyy")}";
            }

            UserTimeSheets = await query.OrderBy(x => x.TimeSheet.Date).ToListAsync();

            return Page();
        }
        
    }
}
