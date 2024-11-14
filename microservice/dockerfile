# Usando a imagem oficial do Node.js versão 20
FROM node:20

# Definindo o diretório de trabalho dentro do container
WORKDIR /app

# Copiando os arquivos para usar eles no dockerhub
COPY package*.json ./
COPY src ./
COPY .env ./

# Instalando as dependências do Node.js
RUN npm install

# Copiando o restante do código para o container
COPY . .

# Expondo a porta que a aplicação irá rodar (por exemplo, 3000)
EXPOSE 3000

# Comando para rodar a aplicação
CMD ["node", "./src/server.js"]

