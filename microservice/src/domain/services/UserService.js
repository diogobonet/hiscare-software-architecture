const UserRepository = require('../../infrastructure/repositories/UserRepository');

// Função para criar um novo usuário
const createUser = async (userData) => {
    try {
        const user = await UserRepository.create(userData);
        return user;
    } catch (error) {
        throw new Error(error);
    }
};

// Função para obter todos os usuários
const getAllUsers = async () => {
    try {
        const users = await UserRepository.findAll();
        return users;
    } catch (error) {
        throw new Error('Erro ao buscar todos os usuários');
    }
};

// Função para obter um usuário por ID
const getUserById = async (id) => {
    try {
        const user = await UserRepository.findById(id);
        if (!user) {
            throw new Error('Usuário não encontrado');
        }
        return user;
    } catch (error) {
        throw new Error('Erro ao buscar o usuário por ID');
    }
};

// Função para atualizar um usuário
const updateUser = async (id, updateData) => {
    try {
        const updatedUser = await UserRepository.update(id, updateData);
        return updatedUser;
    } catch (error) {
        throw new Error('Erro ao atualizar o usuário');
    }
};

// Função para deletar um usuário
const deleteUser = async (id) => {
    try {
        const deletedUser = await UserRepository.deleteUser(id); // Use deleteUser que foi definido no repositório
        return deletedUser;
    } catch (error) {
        throw new Error('Erro ao deletar o usuário');
    }
};

module.exports = {
    createUser,
    getAllUsers,
    getUserById,
    updateUser,
    deleteUser
};
