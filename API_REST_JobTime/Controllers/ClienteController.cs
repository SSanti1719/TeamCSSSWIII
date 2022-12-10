using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly Conection db;
        public ClienteController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetClient")]
        public async Task<IActionResult> GetClient()
        {
            return await db.TableResult("dbo.JobTime_Get_Client");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostClient([FromBody] Client client)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", client.Nit));
            Params.Add(new SqlParameter("@Name", client.Name));
            return await db.PostResult("dbo.JobTime_Post_Client", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteClient(string Nit)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", Nit));
            return await db.PostResult("dbo.JobTime_Delete_Client", Params);
        }
    }
}
