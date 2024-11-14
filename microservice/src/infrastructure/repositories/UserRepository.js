const User = require('../models/User');

// Função para criar um novo usuário
const create = async (userData) => {
    const user = new User(userData);
    return await user.save();
};

// Função para buscar todos os usuários
const findAll = async () => {
    return await User.find();
};

// Função para buscar um usuário por ID
const findById = async (id) => {
    return await User.findById(id);
};

// Função para atualizar um usuário
const update = async (id, updateData) => {
    return await User.findByIdAndUpdate(id, updateData, { new: true });
};

// Função para deletar um usuário
const deleteUser = async (id) => {
    // Aqui estamos chamando o método correto do mongoose
    return await User.findByIdAndDelete(id);
};

module.exports = {
    create,
    findAll,
    findById,
    update,
    deleteUser
};
