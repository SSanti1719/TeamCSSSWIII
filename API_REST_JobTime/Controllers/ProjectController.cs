using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly Conection db;
        public ProjectController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetProject")]
        public async Task<IActionResult> GetProject()
        {
            return await db.TableResult("dbo.JobTime_Get_Project");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostProject(Project project)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Id", project.Id));
            Params.Add(new SqlParameter("@NitClient", project.NitClient));
            Params.Add(new SqlParameter("@Name", project.Name));
            Params.Add(new SqlParameter("@SalesHours", project.SalesHours));
            return await db.PostResult("dbo.JobTime_Post_Project", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProject(string Nit)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", Nit));
            return await db.PostResult("dbo.JobTime_Delete_Project", Params);
        }
    }
}
