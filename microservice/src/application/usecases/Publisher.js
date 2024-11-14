const { ServiceBusClient } = require('@azure/service-bus');
require('dotenv').config();
const connectionString = process.env.AZURE_SERVICE_BUS_CONNECTION_STRING;
const queueName = process.env.AZURE_QUEUE_NAME;

async function sendCrmMessage(crmData) {
    const sbClient = new ServiceBusClient(connectionString); // Chave Primária
    const sender = sbClient.createSender(queueName); // Sender

    try {
        const message = {
            body: { Crm: crmData },
            contentType: 'application/json',
        };
        await sender.sendMessages(message);
        console.log('Mensagem enviada para o Service Bus:', message.body);
    } catch (error) {
        console.error('Erro ao enviar mensagem para o Service Bus:', error);
    } finally {
        await sender.close();
        await sbClient.close();
    }
}

module.exports = { sendCrmMessage };
