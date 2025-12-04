namespace ReportingDashboard.Data.Sales.Models
{
    public class FiscalMonth
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Year { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public bool IsClosed { get; set; }
    }
}
