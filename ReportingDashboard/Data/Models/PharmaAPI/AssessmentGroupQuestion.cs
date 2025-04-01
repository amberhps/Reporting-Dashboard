namespace ReportingDashboard.Data.Models.PharmaAPI
{
    public class AssessmentGroupQuestion
    {
        public long Id { get; set; }
        public long AssessmentGroup_Id { get; set; }
        public required string ColumnName { get; set; }
        public required string AssessmentName { get; set; }
        public required string AssessmentQuestionName { get; set; }
        public string? TrueMapping { get; set; }
        public string? FalseMapping { get; set; }
        public string? DateTimeFormat { get; set; }
        public string? Delimiter { get; set; }
        public bool IsConstant { get; set; }
        public byte RecStatus { get; set; }
        public string ColumnSource { get; set; } = "";

        public AssessmentGroup AssessmentGroup { get; set; } = null!;
    }
}
