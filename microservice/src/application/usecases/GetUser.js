const UserRepository = require('../repositories/UserRepository');

class GetUser {
    async execute(id) {
        const user = await UserRepository.findById(id);
        if (!user) {
            throw new Error('Usuário não encontrado');
        }
        return user;
    }
}

module.exports = new GetUser();
