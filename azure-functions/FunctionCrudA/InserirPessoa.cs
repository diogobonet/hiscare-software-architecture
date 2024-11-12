using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace FunctionCrudA
{
    public class InserirPessoa
    {
        private readonly ILogger<InserirPessoa> _logger;

        public InserirPessoa(ILogger<InserirPessoa> logger)
        {
            _logger = logger;
        }

        [Function("InserirPessoa")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            // Read the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            //string name = data?.name;
            //string description = data?.description;
            string nome = data?.nome;
            string crm = data?.crm;
            string especialidade = data?.especialidade;
            string telefone = data?.telefone;
            string email = data?.email;
            DateTime data_nascimento = data?.data_nascimento;
            string sexo = data?.sexo;
            string endereco = data?.endereco;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(crm) || string.IsNullOrEmpty(especialidade) || string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sexo) || string.IsNullOrEmpty(endereco))
            {
                return new BadRequestObjectResult("Please provide name and description in the request body");
            }

            // Retrieve connection string from environment variable
            string connectionString = "Server=tcp:hiscaredb.database.windows.net,1433;Initial Catalog=Hiscare;Persist Security Info=False;User ID=gustavo;Password=Guga2406;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO dbo.Medicos (nome, crm, especialidade, telefone, email, data_nascimento, sexo, endereco) VALUES (@nome, @crm, @especialidade, @telefone, @email, @data_nascimento, @sexo, @endereco)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@crm", crm);
                        command.Parameters.AddWithValue("@especialidade", especialidade);
                        command.Parameters.AddWithValue("@telefone", telefone);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@data_nascimento", data_nascimento);
                        command.Parameters.AddWithValue("@sexo", sexo);
                        command.Parameters.AddWithValue("@endereco", endereco);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        //return new OkObjectResult($"{rowsAffected} row(s) updated.");
                        return new StatusCodeResult(200);
                    }
                }
            }catch (Exception ex)
            {
                return new StatusCodeResult(404);
            }

        }
    }
}
