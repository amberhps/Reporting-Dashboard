namespace ReportingDashboard.Data.Caretend.Models
{
    public class State
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Country_Id { get; set; }
    }
}
