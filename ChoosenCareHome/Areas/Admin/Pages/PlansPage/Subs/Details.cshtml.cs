using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;

namespace ChoosenCareHome.Areas.Admin.Pages.PlansPage.Subs
{
    public class DetailsModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public DetailsModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CommunityPlanSchedule CommunityPlanSchedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communityplanschedule = await _context.CommunityPlanSchedules.FirstOrDefaultAsync(m => m.Id == id);

            if (communityplanschedule is not null)
            {
                CommunityPlanSchedule = communityplanschedule;

                return Page();
            }

            return NotFound();
        }
    }
}
