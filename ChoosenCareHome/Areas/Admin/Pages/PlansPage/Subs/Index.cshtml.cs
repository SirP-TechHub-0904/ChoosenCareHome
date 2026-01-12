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
    public class IndexModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;

        public IndexModel(ChoosenCareHome.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CommunityPlanSchedule> CommunityPlanSchedule { get;set; } = default!;

        public async Task OnGetAsync()
        {
            CommunityPlanSchedule = await _context.CommunityPlanSchedules
                .Include(c => c.CommunityPlan).ToListAsync();
        }
    }
}
