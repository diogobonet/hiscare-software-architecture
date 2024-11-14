const UserRepository = require('../repositories/UserRepository');

class DeleteUser {
    async execute(id) {
        const user = await UserRepository.deleteById(id);
        if (!user) {
            throw new Error('Usuário não encontrado');
        }
        return user;
    }
}

module.exports = new DeleteUser();
