using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    public class ManagerController : ControllerBase
    {
        private readonly Conection db;
        public ManagerController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetManager")]
        public async Task<IActionResult> GetManager()
        {
            return await db.TableResult("dbo.JobTime_Get_Manager");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostManager([FromBody] Manager manager)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", manager.Nit));
            Params.Add(new SqlParameter("@Name", manager.Name));
            Params.Add(new SqlParameter("@Email", manager.Email));
            Params.Add(new SqlParameter("@JobTitle", manager.JobTitle));
            return await db.PostResult("dbo.JobTime_Post_Manager", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteManager(string Nit)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", Nit));
            return await db.PostResult("dbo.JobTime_Delete_Manager", Params);
        }
    }
}
