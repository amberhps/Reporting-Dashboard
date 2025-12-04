using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Common;
using System.Data;

namespace ReportingDashboard.Data.Jobs
{
    public class JobContext(IOptionsSnapshot<ConnectionStrings> settingSnapshot) : BaseContext
    {
        public override IDbConnection Connection => new SqlConnection(settingSnapshot.Value.SSIS);
    }
}
