using API_REST_JobTime.AppCode.Converters;
using API_REST_JobTime.Controllers;
using API_REST_JobTime.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Transactions;

namespace API_REST_JobTime.Utilities
{
    public class Conection
    {
        public string CadenaSQL { get; set; } = string.Empty;

        public Conection(string cadenaSQL)
        {
            CadenaSQL = cadenaSQL;
        }

        public async Task<DataTable> Table(string query, IEnumerable<SqlParameter>? Params = null)
        {
            var dt = new DataTable();
            using var conection = new SqlConnection(CadenaSQL);
            using var adaptador = new SqlDataAdapter(query, conection);
            adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Params != null)
            {
                foreach (var item in Params)
                {
                    adaptador.SelectCommand.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
            }
            await conection.OpenAsync();
            adaptador.Fill(dt);
            return dt;
        }

        public async Task<ContentResult> TableResult(string SP, IEnumerable<SqlParameter>? Params = null)
        {
            var dt = await Table(SP, Params);
            var options = new JsonSerializerOptions()
            {
                Converters = { new DataTableConverter() }
            };
            string jsonDataTable = JsonSerializer.Serialize(dt, options);
            var content = new ContentResult();
            content.Content = jsonDataTable;
            content.ContentType = "application/json";
            return content;
        }

        public async Task<Response> Execute(string query, IEnumerable<SqlParameter>? Params = null)
        {
            var result = new Response();
            try
            {
                using var conection = new SqlConnection(CadenaSQL);
                var comando = new SqlCommand(query, conection);
                comando.CommandType = CommandType.StoredProcedure;
                if (Params != null)
                {
                    foreach (var item in Params)
                    {
                        comando.Parameters.AddWithValue(item.ParameterName, item.Value);
                    }
                }
                await conection.OpenAsync();
                await comando.ExecuteNonQueryAsync();
                result.success = 1;
                result.mensaje = "Ok";
            }
            catch (Exception ex)
            {
                result.mensaje = ex.Message;
            }
            return result;
        }

        public async Task<ContentResult> PostResult(string query, IEnumerable<SqlParameter>? Params = null)
        {
            var result = await Execute(query, Params);
            string jsonDataTable = JsonSerializer.Serialize(result);
            var content = new ContentResult();
            content.Content = jsonDataTable;
            content.ContentType = "application/json";
            return content;
        }
    }
}
