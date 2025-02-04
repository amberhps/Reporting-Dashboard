using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Models;
using System.Data;

namespace ReportingDashboard.Data
{
    public class SSISContext(IOptions<AppSettings> settingSnapshot) : BaseContext
    {
        private AppSettings _config = settingSnapshot.Value;
        public override IDbConnection Connection => new SqlConnection(_config.SSIS);
    }
}
