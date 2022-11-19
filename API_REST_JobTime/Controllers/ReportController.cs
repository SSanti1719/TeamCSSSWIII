using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    public class ReportController : ControllerBase
    {
        private readonly Conection db;
        public ReportController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport()
        {
            return await db.TableResult("dbo.JobTime_Get_Report");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostReport(Report report)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Id", report.Id));
            Params.Add(new SqlParameter("@IdAssign ", report.IdAssign));
            return await db.PostResult("dbo.JobTime_Post_Report", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteReport(string Id)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Id", Id));
            return await db.PostResult("dbo.JobTime_Delete_Report", Params);
        }
    }
}
