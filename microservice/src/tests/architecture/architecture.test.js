const fs = require('fs');
const path = require('path');

describe('Architecture Tests', () => {
    const checkDependency = (folderPath, forbiddenPaths) => {
        const items = fs.readdirSync(folderPath);

        items.forEach(item => {
            const itemPath = path.join(folderPath, item);
            const isFile = fs.statSync(itemPath).isFile();

            if (isFile) {
                const content = fs.readFileSync(itemPath, 'utf-8');
                forbiddenPaths.forEach(forbiddenPath => {
                    if (content.includes(`require('${forbiddenPath}')`)) {
                        throw new Error(`Dependência inválida encontrada: ${itemPath} -> ${forbiddenPath}`);
                    }
                });
            }
        });
    };

    test('Application layer should not depend on Infrastructure layer', () => {
        const applicationPath = path.resolve(__dirname, '../../application');
        checkDependency(applicationPath, ['../infrastructure']);
    });

    test('Domain layer should not depend on Infrastructure or Application layers', () => {
        const domainPath = path.resolve(__dirname, '../../domain');
        checkDependency(domainPath, ['../infrastructure', '../application']);
    });

    test('Interfaces layer should not depend on Domain layer', () => {
        const interfacesPath = path.resolve(__dirname, '../../interfaces');
        checkDependency(interfacesPath, ['../domain']);
    });
});
