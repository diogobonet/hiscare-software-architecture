const { sendCrmMessage } = require('./application/usecases/Publisher');
app.post('/sendCrm', async (req, res) => {
    const Crm = req.body.Crm_Medico;

    if (!Crm) {
        return res.status(400).json({ message: 'CRM_Medico is required' });
    }

    try {
        await sendCrmMessage(Crm.trim()); 
        res.status(200).json({ message: 'CRM enviado para a fila com sucesso!' });
    } catch (error) {
        console.error('Erro ao enviar CRM:', error);
        res.status(500).json({ message: 'Erro ao enviar CRM para o Service Bus' });
    }
});