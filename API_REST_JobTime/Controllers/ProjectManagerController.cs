using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    [Route("api/ProjectManager")]
    [ApiController]
    public class ProjectManagerController : ControllerBase
    {
        private readonly Conection db;
        public ProjectManagerController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetProjectManager")]
        public async Task<IActionResult> GetProjectManager()
        {
            return await db.TableResult("dbo.JobTime_Get_ProjectManager");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostProjectManager([FromBody] ProjectManager projectManager)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", projectManager.Nit));
            Params.Add(new SqlParameter("@Name", projectManager.Name));
            Params.Add(new SqlParameter("@Email", projectManager.Email));
            Params.Add(new SqlParameter("@JobTitle", projectManager.JobTitle));
            return await db.PostResult("dbo.JobTime_Post_ProjectManager", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProjectManager(string Nit)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", Nit));
            return await db.PostResult("dbo.JobTime_Delete_ProjectManager", Params);
        }
    }
}
