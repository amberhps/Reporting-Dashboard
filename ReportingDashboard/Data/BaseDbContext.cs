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

        public async Task EnableIdentityInsert<T>() => await SetIdentityInsert<T>(true);
        public async Task DisableIdentityInsert<T>() => await SetIdentityInsert<T>(false);

        private async Task SetIdentityInsert<T>(bool enable)
        {
            var entityType = Model.FindEntityType(typeof(T));
            var value = enable ? "ON" : "OFF";

            if (entityType == null)
                return;

            var command = $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}";

            await Database.ExecuteSqlRawAsync(command);
        }
    }
}
