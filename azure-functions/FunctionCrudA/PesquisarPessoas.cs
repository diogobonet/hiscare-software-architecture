using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Nodes;

namespace FunctionCrudA
{
    public class PesquisarPessoas
    {
        private readonly ILogger<PesquisarPessoas> _logger;

        public PesquisarPessoas(ILogger<PesquisarPessoas> logger)
        {
            _logger = logger;
        }

        [Function("PesquisarPessoas")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {

            // Retrieve connection string from environment variables
            var connectionString = "Server=tcp:hiscaredb.database.windows.net,1433;Initial Catalog=Hiscare;Persist Security Info=False;User ID=gustavo;Password=Guga2406;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            string query = "SELECT * FROM dbo.Medicos";
            var result = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        result += "Id: " + reader[0].ToString() + " Nome: " + reader[1].ToString() + " CRM: " + reader[2].ToString() + " Especialidade:" + reader[3].ToString() + " Telefone:" + reader[4].ToString() + " Email:" + reader[5].ToString() + " Aniversário:" + reader[6].ToString() + " Genero:" + reader[7].ToString() + " Endereço:"+ reader[8].ToString() + " Modificado:" + reader[9].ToString() + "\n"; 
                    }
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);
                //return new BadRequestObjectResult("Error occurred while fetching data.");
            }

            return new OkObjectResult(result);
        }

    }
}
