namespace ReportingDashboard.Data.Sales.Models
{
    public class SalesRecord
    {
        public long Id { get; set; }
        public DateTime BilledDate { get; set; }
        public string CarrierName { get; set; } = string.Empty;
        public string InventoryItemName { get; set; } = string.Empty;
        public decimal ExpectedPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public decimal GrossProfit { get; set; }

        public bool IsError => ExpectedPrice - Cost + AdjustmentAmount != GrossProfit;

    }
}
