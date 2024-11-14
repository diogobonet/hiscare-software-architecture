using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;

namespace FunctionCrudA
{
    public class EditarPessoa
    {
        private readonly ILogger<EditarPessoa> _logger;

        public EditarPessoa(ILogger<EditarPessoa> logger)
        {
            _logger = logger;
        }

        [Function("EditarPessoa")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string crm = data?.crm;
            string nome = data?.nome;
            string especialidade = data?.especialidade;
            string telefone = data?.telefone;
            string email = data?.email;
            DateTime? data_nascimento = data?.data_nascimento != null ? (DateTime?)data?.data_nascimento : null;
            string sexo = data?.sexo;
            string endereco = data?.endereco;
            DateTime? modificado = data?.modificado != null ? (DateTime?)data?.modificado : null;

            if (string.IsNullOrEmpty(crm))
            {
                return new BadRequestObjectResult("Por favor, forneça o CRM para identificar o médico a ser editado.");
            }

            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "UPDATE dbo.Medicos SET ";
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(nome))
                {
                    query += "Nome = @Nome, ";
                    parameters.Add(new SqlParameter("@Nome", nome));
                }

                if (!string.IsNullOrEmpty(especialidade))
                {
                    query += "Especialidade = @Especialidade, ";
                    parameters.Add(new SqlParameter("@Especialidade", especialidade));
                }

                if (!string.IsNullOrEmpty(telefone))
                {
                    query += "Telefone = @Telefone, ";
                    parameters.Add(new SqlParameter("@Telefone", telefone));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query += "Email = @Email, ";
                    parameters.Add(new SqlParameter("@Email", email));
                }

                if (data_nascimento.HasValue)
                {
                    query += "data_nascimento = @data_nascimento, ";
                    parameters.Add(new SqlParameter("@data_nascimento", data_nascimento));
                }

                if (!string.IsNullOrEmpty(sexo))
                {
                    query += "Sexo = @Sexo, ";
                    parameters.Add(new SqlParameter("@Sexo", sexo));
                }

                if (!string.IsNullOrEmpty(endereco))
                {
                    query += "Endereco = @Endereco, ";
                    parameters.Add(new SqlParameter("@Endereco", endereco));
                }

                if (modificado.HasValue)
                {
                    query += "modificado  = @modificado, ";
                    parameters.Add(new SqlParameter("@modificado", modificado));
                }

                query = query.TrimEnd(',', ' ');

                query += " WHERE CRM = @CRM";
                parameters.Add(new SqlParameter("@CRM", crm));

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        //return new OkObjectResult($"{rowsAffected} registro(s) atualizado(s) com sucesso.");
                        return new StatusCodeResult(200);
                    }
                    else
                    {
                        //return new NotFoundObjectResult("Médico não encontrado com o CRM fornecido.");
                        return new StatusCodeResult(404);
                    }
                }
            }
        }
    }
}
