using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Sales.Models;

namespace ReportingDashboard.Data.Sales
{
    public class SalesContext(IOptionsSnapshot<AppSettings> settingSnapshot) : BaseDbContext
    {
        public List<AuditLog> AuditLogs { get; set; } = [];
        public List<SalesRecord> FiscalMonthSales { get; set; } = [];
        public List<FiscalMonth> FiscalMonths { get; set; } = [];

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            #if DEBUG
            options.LogTo(Console.WriteLine, LogLevel.Information);
            #endif

            options.UseSqlServer(settingSnapshot.Value.OldWarehouse);
        }

        public async Task<List<FiscalMonth>> GetFiscalMonthsAsync(CancellationToken token)
        {
            using var conn = Database.GetDbConnection();
            return [.. await conn.QueryAsync<FiscalMonth>("""
                SELECT
                	FiscalTimeDim.Fiscal_Month_Id AS Id
                	,FiscalTimeDim.Fiscal_Month_Name AS Name
                	,MIN(FiscalTimeDim.Fiscal_Date) AS StartDate
                	,MAX(FiscalTimeDim.Fiscal_Date) AS EndDate
                FROM
                	dbo.FiscalTimeDim
                	JOIN dbo.FiscalTimeDim CurrentYear ON CurrentYear.Fiscal_Year_Id = FiscalTimeDim.Fiscal_Year_Id
                	AND CurrentYear.Fiscal_Date = CONVERT(DATE, GETDATE())
                GROUP BY
                	FiscalTimeDim.Fiscal_Month_Id
                	,FiscalTimeDim.Fiscal_Month_Name
                ORDER BY
                	FiscalTimeDim.Fiscal_Month_Id
                """)];
        }
    }
}
