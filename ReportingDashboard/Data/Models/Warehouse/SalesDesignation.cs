using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportingDashboard.Data.Models.Warehouse
{
    public class SalesDesignation
    {
        public long Id { get; set; }
        public long SalesPerson_Id { get; set; }
        [Column("SalesDesignation")]
        public string SalesDesignationName { get; set; } = "";
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }

        public required SalesPerson SalesPerson { get; set; }
        public ICollection<SalesPersonState> SalesPersonStates { get; } = [];
    }
}
