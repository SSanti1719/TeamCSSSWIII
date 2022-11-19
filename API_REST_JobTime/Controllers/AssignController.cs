using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    public class AssignController : ControllerBase
    {
        private readonly Conection db;
        public AssignController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetAssign")]
        public async Task<IActionResult> GetAssign()
        {
            return await db.TableResult("dbo.JobTime_Get_Assign");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostAssign(Assign assign)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Id", assign.Id));
            Params.Add(new SqlParameter("@NitProjectManager", assign.NitProjectManager));
            Params.Add(new SqlParameter("@NitEmployee", assign.NitEmployee));
            Params.Add(new SqlParameter("@NitProject", assign.NitProject));
            return await db.PostResult("dbo.JobTime_Post_Assign", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAssign(string Nit)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", Nit));
            return await db.PostResult("dbo.JobTime_Delete_Assign", Params);
        }
    }
}
