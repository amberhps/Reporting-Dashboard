using Microsoft.EntityFrameworkCore;

namespace ReportingDashboard.Data
{
    public class BaseDbContext : DbContext
    {
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
