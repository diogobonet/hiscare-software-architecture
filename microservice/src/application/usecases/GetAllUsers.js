const UserRepository = require('../repositories/UserRepository');

class GetAllUsers {
    async execute() {
        const users = await UserRepository.findAll();
        return users;
    }
}

module.exports = new GetAllUsers();
