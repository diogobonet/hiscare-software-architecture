const mongoose = require('mongoose');

const UserSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    email: {
        type: String,
        required: true,
        unique: true
    },
    aniversario: {
        type: Date,
        required: true
    },
    sexo: {
        type: String,
        required: true
    },
    altura: {
        type: Number,
        required: true
    },
    peso: {
        type: Number,
        required: true
    },
    telefone: {
        type: String,
        required: true
    },
    Crm_Medico: {
        type: String,
        required: true
    }
});

module.exports = mongoose.model('User', UserSchema);
