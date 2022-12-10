using API_REST_JobTime.AppCode.Converters;
using API_REST_JobTime.Entity;
using API_REST_JobTime.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.Json;

namespace API_REST_JobTime.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly Conection db;
        public EmployeeController(Conection db)
        {
            this.db = db;
        }

        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            return await db.TableResult("dbo.JobTime_Get_Employee");            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", employee.Nit));
            Params.Add(new SqlParameter("@Name", employee.Name));
            Params.Add(new SqlParameter("@Email", employee.Email));
            Params.Add(new SqlParameter("@JobTitle", employee.JobTitle));
            return await db.PostResult("dbo.JobTime_Post_Employee", Params);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteEmployee(string Nit)
        {
            var Params = new Collection<SqlParameter>();
            Params.Add(new SqlParameter("@Nit", Nit));
            return await db.PostResult("dbo.JobTime_Delete_Employee", Params);
        }

    }
}
