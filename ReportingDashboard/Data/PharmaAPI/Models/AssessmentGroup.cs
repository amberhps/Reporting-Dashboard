namespace ReportingDashboard.Data.PharmaAPI.Models
{
    public class AssessmentGroup
    {
        public long Id { get; set; }
        public required string TriageName { get; set; }

        public List<AssessmentGroupQuestion> AssessmentGroupQuestions { get; set; } = [];
    }
}
