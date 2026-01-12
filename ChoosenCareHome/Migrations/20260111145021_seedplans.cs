using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoosenCareHome.Migrations
{
    public partial class seedplans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // COMMUNITY PLANS
            migrationBuilder.InsertData(
                table: "CommunityPlans",
                columns: new[] { "Id", "Title", "PackageType", "DayType" },
                values: new object[,]
                {
                    { 1, "Community Independence Hub", "Standard Package", "Full Day" },
                    { 2, "Community Independence Hub", "Standard Package", "Half Day" },

                    { 3, "Community Independence Hub", "Medium Package", "Full Day" },
                    { 4, "Community Independence Hub", "Medium Package", "Half Day" },

                    { 5, "Community Independence Hub", "Enhanced Package", "Full Day" },
                    { 6, "Community Independence Hub", "Enhanced Package", "Half Day" },

                    { 7, "Community Independence Hub", "Super Enhanced Package", "Full Day" },
                    { 8, "Community Independence Hub", "Super Enhanced Package", "Half Day" }
                }
            );

            // FULL DAY SCHEDULES
            InsertFullDay(migrationBuilder, 1);
            InsertFullDay(migrationBuilder, 3);
            InsertFullDay(migrationBuilder, 5);
            InsertFullDay(migrationBuilder, 7);

            // HALF DAY SCHEDULES
            InsertHalfDay(migrationBuilder, 2);
            InsertHalfDay(migrationBuilder, 4);
            InsertHalfDay(migrationBuilder, 6);
            InsertHalfDay(migrationBuilder, 8);
        }

        private static void InsertFullDay(MigrationBuilder migrationBuilder, int planId)
        {
            migrationBuilder.InsertData(
                table: "CommunityPlanSchedules",
                columns: new[] { "CommunityPlanId", "Day", "TimeSlot" },
                values: new object[,]
                {
                    { planId, "Tuesday", "9:30am" },
                    { planId, "Tuesday", "4:30pm" },
                    { planId, "Tuesday", "7:30pm" },

                    { planId, "Wednesday", "9:30am" },
                    { planId, "Wednesday", "4:30pm" },
                    { planId, "Wednesday", "7:30pm" },

                    { planId, "Thursday", "9:30am" },
                    { planId, "Thursday", "4:30pm" },
                    { planId, "Thursday", "7:30pm" }
                }
            );
        }

        private static void InsertHalfDay(MigrationBuilder migrationBuilder, int planId)
        {
            migrationBuilder.InsertData(
                table: "CommunityPlanSchedules",
                columns: new[] { "CommunityPlanId", "Day", "TimeSlot" },
                values: new object[,]
                {
                    { planId, "Monday", "9:30am – 12:30pm" },
                    { planId, "Monday", "1:30pm – 4:30pm" },

                    { planId, "Friday", "9:30am – 12:30pm" },
                    { planId, "Friday", "1:30pm – 4:30pm" },

                    { planId, "Saturday", "9:30am – 12:30pm" },
                    { planId, "Saturday", "1:30pm – 4:30pm" },

                    { planId, "Sunday", "9:30am – 12:30pm" },
                    { planId, "Sunday", "1:30pm – 4:30pm" }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CommunityPlanSchedules");
            migrationBuilder.Sql("DELETE FROM CommunityPlans");
        }
    }
}
