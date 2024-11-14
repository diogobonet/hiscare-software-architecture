const UserRepository = require('../repositories/UserRepository');

class CreateUser {
    async execute(userData) {
        const user = await UserRepository.create(userData);
        return user;
    }
}

module.exports = new CreateUser();
