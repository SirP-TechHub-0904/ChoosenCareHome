using ChoosenCareHome.Data.Model;
using ChoosenCareHome.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChoosenCareHome.Areas.Admin.Pages.TimeSheetPage
{
    public class GenerateInvoiceModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public GenerateInvoiceModel(ChoosenCareHome.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UserTimeSheet> UserTimeSheets { get; set; }
        public Profile UserProfile { get; set; }
        public string Fullname { get; set; }


        [BindProperty]
        public Invoice Invoice { get; set; }

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




            Fullname = UserProfile.FirstName + " " + UserProfile.Surname;
            UserTimeSheets = await _context.UserTimeSheets
                .Where(uts => uts.UserId == id && uts.Paid == false)
                .ToListAsync();


            if (UserTimeSheets.Any())
            {
                Invoice = new Invoice();
                Invoice.PeriodStart = UserTimeSheets.Min(uts => uts.Date);
                Invoice.PeriodEnd = UserTimeSheets.Max(uts => uts.Date);
                Invoice.Rate = UserTimeSheets.First().RatePerHour; // Assuming the rate is consistent across sheets
                Invoice.TotalHours = Convert.ToDecimal(UserTimeSheets.Sum(uts => (uts.EndTime - uts.StartTime).TotalHours - (uts.Break / 60.0)));
                Invoice.TotalPay = Invoice.TotalHours * Invoice.Rate;
                Invoice.NetPay = Invoice.TotalPay - Invoice.IncomeTax - Invoice.NationalInsurance;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try { 

                Invoice.TotalPay = Invoice.TotalHours * Invoice.Rate;
                Invoice.NetPay = Invoice.TotalPay - Invoice.IncomeTax - Invoice.NationalInsurance;
            Invoice.InvoiceDate = DateTime.UtcNow.AddHours(1);
            Invoice.InvoiceStatus = Data.Model.Enum.InvoiceStatus.Pending;
            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Invoice", new {id = Invoice.Id});
            }catch(Exception ex) {

                return Page();
            }
        }
    }
}
