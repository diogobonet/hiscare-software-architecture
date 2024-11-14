// Arquivo principal: app.js
const express = require('express');
const mongoose = require('mongoose');
const userRoutes = require('./interfaces/controllers/UserController');

require('dotenv').config();

const app = express();

app.use(express.json());

// Conectar ao MongoDB
mongoose.connect(process.env.MONGO_URI, { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => console.log('Conectado ao MongoDB'))
    .catch(err => console.error('Erro ao conectar ao MongoDB:', err));

// Usar as rotas de usuários
app.use('/users', userRoutes);
app.use('/sendCrm', userRoutes);

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
    console.log(`Servidor rodando na porta ${PORT}`);
});

module.exports = app;
