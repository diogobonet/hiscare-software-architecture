const express = require('express');
const router = express.Router();
const UserService = require('../../domain/services/UserService');
const UpdateUser = require('../../application/usecases/UpdateUser');

// Rota para criar um novo usuário (POST)
// Rota para criar um novo usuário (POST)
router.post('/', async (req, res) => {
    try {
        const { name, email, aniversario, sexo, altura, peso, telefone, Crm_Medico } = req.body;
        const newUser = await UserService.createUser({ name, email, aniversario, sexo, altura, peso, telefone, Crm_Medico });
        res.status(201).json(newUser);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});


// Rota para obter todos os usuários (GET)
router.get('/', async (req, res) => {
    try {
        const users = await UserService.getAllUsers();
        res.json(users);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Rota para obter um usuário por ID (GET)
router.get('/:id', async (req, res) => {
    try {
        const user = await UserService.getUserById(req.params.id);
        if (!user) {
            return res.status(404).json({ message: 'Usuário não encontrado' });
        }
        res.json(user);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Rota para atualizar um usuário (PUT)
router.put('/:id', async (req, res) => {
    try {
        const updatedUser = await UpdateUser.execute(req.params.id, req.body);
        res.json(updatedUser);
    } catch (error) {
        res.status(404).json({ error: error.message });
    }
});

// Rota para deletar um usuário (DELETE)
router.delete('/:id', async (req, res) => {
    try {
        const deletedUser = await UserService.deleteUser(req.params.id);
        if (!deletedUser) {
            return res.status(404).json({ message: 'Usuário não encontrado' });
        }
        res.json({ message: 'Usuário deletado com sucesso' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

module.exports = router;

