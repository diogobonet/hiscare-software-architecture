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
    public class PesquisarPessoa
    {
        private readonly ILogger<PesquisarPessoa> _logger;

        public PesquisarPessoa(ILogger<PesquisarPessoa> logger)
        {
            _logger = logger;
        }

        [Function("PesquisarPessoa")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            // Get the ID from the query string
            string crm = req.Query["crm"];

            if (string.IsNullOrEmpty(crm))
            {
                return new BadRequestObjectResult("Please pass an crm on the query string.");
            }

            // Retrieve connection string from environment variables
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            string query = "SELECT * FROM dbo.Medicos WHERE crm = @Crm";
            var result = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Use SqlCommand with parameterized query to prevent SQL injection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Crm", crm);

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result += "Id: " + reader[0].ToString() + " Nome: " + reader[1].ToString() + " CRM: " + reader[2].ToString() + " Especialidade:" + reader[3].ToString() + " Telefone:" + reader[4].ToString() + " Email:" + reader[5].ToString() + " Anivers�rio:" + reader[6].ToString() + " Genero:" + reader[7].ToString() + " Endere�o:"+ reader[8].ToString() + "\n";
                            }
                        }
                        else
                        {
                            return new StatusCodeResult(404);
                            //return new NotFoundObjectResult("No record found with the provided ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);
            }

            return new OkObjectResult(result);
        }
    }
}
