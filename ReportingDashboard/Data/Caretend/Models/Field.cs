namespace ReportingDashboard.Data.Caretend.Models
{
    public class Field
    {
        public long Id { get; set; }
        public required string ReportingFieldName { get; set; }
        public long FieldType_Id { get; set; }
    }
}
