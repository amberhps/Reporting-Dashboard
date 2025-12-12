using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Common;
using ReportingDashboard.Data.Jobs.Models;
using System.Data;

namespace ReportingDashboard.Data.Jobs
{
    public class JobContext(IOptionsSnapshot<ConnectionStrings> settingSnapshot) : BaseContext
    {
        public override IDbConnection Connection => new SqlConnection(settingSnapshot.Value.Config);

        public async Task<IEnumerable<MissingSSISRun>> GetMissedJobRuns(CancellationToken token)
        {
            var command = new CommandDefinition(
                "MissedSSISRuns",
                cancellationToken: token,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 0
            );

            return await Connection.QueryAsync<MissingSSISRun>(command);
        }
    }
}
