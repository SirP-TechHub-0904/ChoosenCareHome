using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CommunityPlan> Plans { get; set; }

        public async Task OnGetAsync()
        {
            Plans = await _context.CommunityPlans
                .Include(x => x.Schedules)
                .OrderBy(x => x.PackageType)
                .ThenBy(x => x.DayType)
                .ToListAsync();
        }
    }
}
