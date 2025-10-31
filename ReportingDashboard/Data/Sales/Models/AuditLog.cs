namespace ReportingDashboard.Data.Sales.Models
{
    public class AuditLog
    {
        public long Id { get; set; }
        public required string EntityName { get; set; }
        public required long EntityId { get; set; }
        public required string PropertyName { get; set; } 
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public required string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public string ChangeReason { get;set; } = string.Empty;
    }
}
