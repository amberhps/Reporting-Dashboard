using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Models;
using System.Data;

namespace ReportingDashboard.Data
{
    public class SSISContext(IOptionsSnapshot<AppSettings> settingSnapshot) : BaseContext
    {
        public override IDbConnection Connection => new SqlConnection(settingSnapshot.Value.SSIS);
    }
}
