const UserService = require('../../domain/services/UserService');

class UpdateUser {
    async execute(id, userData) {
        try {
            const updatedUser = await UserService.updateUser(id, userData);
            return updatedUser;
        } catch (error) {
            throw new Error(error.message);
        }
    }
}

module.exports = new UpdateUser();
