using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace ReportingDashboard.Data.Jobs
{
    public class JobContext(IOptionsSnapshot<AppSettings> settingSnapshot) : BaseContext
    {
        public override IDbConnection Connection => new SqlConnection(settingSnapshot.Value.SSIS);
    }
}
