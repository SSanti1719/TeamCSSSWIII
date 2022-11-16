using API_REST_JobTime.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace API_REST_JobTime.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("GetEmployee")]
        public Collection<Employee> GetEmployee()
        {
            var lista = new Collection<Employee>();
            using (var conection = new SqlConnection(Conection.CadenaSQL))
            {
                using (var dbemployee = new SqlDataAdapter("Select * From JobTime.dbo.Employee", conection))
                {
                    conection.Open();
                    var reader = dbemployee.SelectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var employee = new Employee();
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.Nombre = reader.GetString(1);
                        employee.Apellido = reader.GetString(2);
                        employee.Direccion = reader.GetString(3);
                        employee.Ciudad = reader.GetString(4);
                        lista.Add(employee);
                    }
                }
            }
            return lista;
            
        }

        [HttpPost("PostEmployee")]
        public Collection<Employee> PostEmployee()
        {
            return null;
        }


    }
}
