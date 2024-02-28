using ChoosenCareHome.Data.Model;

namespace ChoosenCareHome.Data
{
    public class UserTimeSheet
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }

        public TimeSpan StartTime { get; set; } // Represents the start time of work
        public TimeSpan EndTime { get; set; }   // Represents the end time of work
        public string? Report { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public decimal RatePerHour { get; set; }
        public int Break { get; set; }

        public int? TimeSheetId { get; set; }
        public TimeSheet TimeSheet { get; set; }
    }
}
