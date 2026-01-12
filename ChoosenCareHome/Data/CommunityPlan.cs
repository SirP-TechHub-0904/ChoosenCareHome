namespace ChoosenCareHome.Data
{
    public class CommunityPlan
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PackageType { get; set; }
        public string DayType { get; set; }

        public ICollection<CommunityPlanSchedule> Schedules { get; set; }
    }

    public class CommunityPlanSchedule
    {
        public int Id { get; set; }
        public int CommunityPlanId { get; set; }

        public string Day { get; set; }
        public string TimeSlot { get; set; }

        public CommunityPlan CommunityPlan { get; set; }
    }

}
