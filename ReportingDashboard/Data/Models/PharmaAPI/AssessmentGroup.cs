namespace ReportingDashboard.Data.Models.PharmaAPI
{
    public class AssessmentGroup
    {
        public long Id { get; set; }
        public required string TriageName { get; set; }

        public List<AssessmentGroupQuestion> AssessmentGroupQuestions { get; set; } = [];
    }
}
