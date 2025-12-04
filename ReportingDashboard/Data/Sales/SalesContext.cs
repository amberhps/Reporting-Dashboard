using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Common;
using ReportingDashboard.Data.Sales.Models;

namespace ReportingDashboard.Data.Sales
{
    public class SalesContext(IOptionsSnapshot<ConnectionStrings> settingSnapshot) : BaseDbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SalesRecord> FiscalMonthSales { get; set; }
        public DbSet<FiscalMonth> FiscalMonths { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ReportingDashboard");

            modelBuilder.Entity<AuditLog>()
                .ToTable("AuditLog")
                .HasKey(x => x.Id);

            modelBuilder.Entity<AuditLog>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SalesRecord>()
                .ToTable("AuditedSales")
                .HasKey(x => x.Id);

            modelBuilder.Entity<SalesRecord>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SalesRecord>()
                .HasMany(x => x.AuditLogs)
                .WithOne(x => x.SalesRecord)
                .HasForeignKey(x => x.EntityId);

            modelBuilder.Entity<FiscalMonth>()
                .ToTable("FiscalMonth")
                .HasKey(x => x.Id);

            modelBuilder.Entity<FiscalMonth>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(settingSnapshot.Value.Sales);
        }

        public async Task<List<FiscalMonth>> GetFiscalMonthsAsync()
        {
            using var conn = new SqlConnection(settingSnapshot.Value.OldWarehouse);
            return [.. await conn.QueryAsync<FiscalMonth>("""
                SELECT
                   	FiscalTimeDim.Fiscal_Month_Id AS Id
                   	,FiscalTimeDim.Fiscal_Month_Name AS Name
                    ,FiscalTimeDim.Fiscal_Year_Id AS Year
                   	,MIN(FiscalTimeDim.Fiscal_Date) AS StartDate
                   	,MAX(FiscalTimeDim.Fiscal_Date) AS EndDate
                FROM
                   	dbo.FiscalTimeDim
                GROUP BY
                   	FiscalTimeDim.Fiscal_Month_Id
                   	,FiscalTimeDim.Fiscal_Month_Name
                	 ,FiscalTimeDim.Fiscal_Year_Id
                ORDER BY
                   	FiscalTimeDim.Fiscal_Month_Id
                """)];
        }

        public async Task<int> GetCurrentFiscalYearAsync()
        {
            using var conn = new SqlConnection(settingSnapshot.Value.OldWarehouse);
            return await conn.QuerySingleAsync<int>("""
                SELECT
                	Fiscal_Year_Id
                FROM
                	dbo.FiscalTimeDim
                WHERE
                	Fiscal_Date = CONVERT(DATE, GETDATE())
                """);
        }
    }
}
