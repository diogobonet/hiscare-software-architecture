using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
public class ExcluirPessoa
{
        private readonly ILogger<ExcluirPessoa> _logger;

        public ExcluirPessoa(ILogger<ExcluirPessoa> logger)
        {
            _logger = logger;
        }

        [Function("ExcluirPessoa")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequest req)
        {
            string crm = req.Query["crm"];

            if (string.IsNullOrEmpty(crm))
            {
                return new BadRequestObjectResult("Please pass an crm on the query string.");
            }

            // Retrieve connection string from environment variables
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var query = $"DELETE FROM dbo.Medicos WHERE crm = @Crm";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Crm", crm);
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new StatusCodeResult(200);
                            //return new OkObjectResult($"Record with ID {id} deleted successfully.");
                        }
                        else
                        {
                            return new StatusCodeResult(404);
                            //return new NotFoundObjectResult($"No record found with ID {id}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);
            }

        }
}
