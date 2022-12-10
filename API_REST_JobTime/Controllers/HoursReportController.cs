using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    public class HoursReportController : ControllerBase
    {
        private readonly Conection db;
        public HoursReportController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetHoursReport")]
        public async Task<IActionResult> GetReport()
        {
            return await db.TableResult("dbo.JobTime_Get_HoursReport");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostHoursReport([FromBody] HoursReport hoursReport)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Id", hoursReport.Id));
            Params.Add(new SqlParameter("@IdAssign ", hoursReport.IdAssign));
            Params.Add(new SqlParameter("@TimeWorked ", hoursReport.TimeWorked));
            return await db.PostResult("dbo.JobTime_Post_HoursReport", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteHoursReport(string Id)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Id", Id));
            return await db.PostResult("dbo.JobTime_Delete_HoursReport", Params);
        }
    }
}
