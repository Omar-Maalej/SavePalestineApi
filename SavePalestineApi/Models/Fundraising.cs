namespace SavePalestineApi.Models
{
    public class Fundraising
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

        public float GoalSum { get; set; }
        public float CurrentSum { get; set; }
        public float Progress => CurrentSum * 100 / GoalSum;
        public string? ImageUrl { get; set; }
    }
}
