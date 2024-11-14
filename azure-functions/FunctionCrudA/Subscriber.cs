using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Text.Json;

namespace FunctionCrudA
{
    public static class Subscriber
    {
        [Function("Subscriber")]
        public static void Run(
            [ServiceBusTrigger("cadastrarppacientev1", Connection = "ServiceBusConnectionString")] string myQueueItem,
            FunctionContext context)
        {
            var logger = context.GetLogger("Subscriber");
            logger.LogInformation("Service Bus queue trigger function started.");

            if (string.IsNullOrEmpty(myQueueItem))
            {
                logger.LogWarning("Received an empty message.");
                return;
            }
            try
            {
                // Desserializa a mensagem JSON para extrair o campo "crm"
                var message = JsonSerializer.Deserialize<SubscriberMessage>(myQueueItem);
                if (message == null || string.IsNullOrEmpty(message.Crm))
                {
                    logger.LogWarning("CRM not found in the received message.");
                    return;
                }

                string crm = message.Crm;
                DateTime currentDateTime = DateTime.UtcNow; // Utiliza data/hora UTC para o campo modificado

                // String de conex�o com o banco de dados
                string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

                // Conecta ao banco de dados e atualiza o campo "modificado"
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE dbo.Medicos SET modificado = @modificado WHERE CRM = @CRM";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@modificado", currentDateTime);
                        command.Parameters.AddWithValue("@CRM", crm);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            logger.LogInformation($"Registro atualizado para CRM {crm} com sucesso.");
                        }
                        else
                        {
                            logger.LogWarning($"Nenhum registro encontrado para o CRM {crm}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao processar a mensagem: {ex.Message}");
            }
        }
    }
    // Classe para mapear a estrutura JSON da mensagem recebida
    public class SubscriberMessage
    {
        public string Crm { get; set; }
    }

}
