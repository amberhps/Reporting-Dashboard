using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Models;
using ReportingDashboard.Data.Models.Caretend;
using ReportingDashboard.Data.Models.PharmaAPI;

namespace ReportingDashboard.Data
{
    public class CaretendContext(IOptionsSnapshot<AppSettings> settingSnapshot) : DbContext
    {
        public DbSet<ServiceAreaDesignation> ServiceAreaDesignations { get; set; }

        public DbSet<TeamDesignation> TeamDesignations { get; set; }

        public DbSet<TherapyTypeDesignation> TherapyTypeDesignations { get; set; }

        public DbSet<SalesPerson> SalesPersons { get; set; }

        public DbSet<State> StateLookup { get; set; }

        public DbSet<PharmaAPIConfiguration> PharmaAPIConfiguration { get; set; }

        public DbSet<AssessmentGroup> AssessmentGroups { get; set; }

        public DbSet<ContainerGroup> ContainerGroups { get; set; }

        public DbSet<Field> ContainerFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceAreaDesignation>()
                .ToTable("ServiceAreaDesignation", "AmberWarehouse")
                .HasKey(x => x.Id);

            modelBuilder.Entity<TeamDesignation>()
                .ToTable("TeamSalesDesignation", "AmberWarehouse")
                .HasKey(x => x.Id);

            modelBuilder.Entity<TherapyTypeDesignation>()
                .ToTable("TherapyTypeSalesDesignation", "AmberWarehouse")
                .HasKey(x => x.Id);

            modelBuilder.Entity<SalesPersonState>()
                .ToTable("SalesPersonState", "AmberWarehouse")
                .HasKey(x => x.Id);

            modelBuilder.Entity<SalesDesignation>()
                .ToTable("SalesPersonSalesDesignation", "AmberWarehouse")
                .HasKey(x => x.Id);

            modelBuilder.Entity<SalesDesignation>()
                .HasMany(x => x.SalesPersonStates)
                .WithOne(x => x.SalesPersonSalesDesignation)
                .HasForeignKey(x => x.SalesPersonSalesDesignation_Id);

            modelBuilder.Entity<SalesPerson>()
                .ToTable("SalesPerson", "AmberWarehouse")
                .HasKey(x => x.Id);

            modelBuilder.Entity<SalesPerson>()
                .HasMany(x => x.SalesDesignations)
                .WithOne(x => x.SalesPerson)
                .HasForeignKey(x => x.SalesPerson_Id);

            modelBuilder.Entity<State>()
                .ToTable("State", "Lookups")
                .HasKey(x => x.Id);

            modelBuilder.Entity<PharmaAPIConfiguration>()
                .ToTable("Configurations", "PharmaAPI")
                .HasKey(x => x.Id);

            modelBuilder.Entity<AssessmentGroup>().HasKey(x => x.Id);
            modelBuilder.Entity<AssessmentGroup>()
                .ToTable("AssessmentGroup", "PharmaAPI")
                .HasMany(x => x.AssessmentGroupQuestions)
                .WithOne(x => x.AssessmentGroup)
                .HasForeignKey(x => x.AssessmentGroup_Id);

            modelBuilder.Entity<AssessmentGroupQuestion>()
                .ToTable("AssessmentGroupQuestions", "PharmaAPI")
                .HasKey(x => x.Id);

            modelBuilder.Entity<ContainerGroup>()
                .ToTable("ContinerGroup", "Common")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Field>()
                .ToView("vField", "Common")
                .HasKey(x => x.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            #if DEBUG
            options.LogTo(Console.WriteLine, LogLevel.Information);
            #endif

            options.UseSqlServer(settingSnapshot.Value.CareTend);
        }

        public bool RevertChanges()
        {
            var changes = ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var entry in changes)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }

            return changes.Count > 0;
        }
    }
}
