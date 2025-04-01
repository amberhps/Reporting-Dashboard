namespace ReportingDashboard.Data.Models.Caretend
{
    public class Field
    {
        public long Id { get; set; }
        public required string ReportingFieldName { get; set; }
        public long FieldType_Id { get; set; }
    }
}
