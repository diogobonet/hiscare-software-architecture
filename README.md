# HisCare - Software Architecture
### Choose a language | Escolha uma linguagem
- [PT-BR](#pt-br)
- [EN](#en)

# PT-BR

## Índice
1. [O que é o HisCare?](#o-que-é-o-hiscare)
2. [Arquiteturas e Padrões Arquiteturais Utilizados](#arquiteturas-e-padrões-arquiteturais-utilizados)
3. [Artefatos Gerados](#artefatos-gerados)
4. [Como Funciona a Arquitetura](#como-funciona-a-arquitetura)
5. [Tecnologias Utilizadas](#tecnologias-utilizadas)
6. [Testes](#testes)


## O que é o HisCare?
O HISCare é uma solução abrangente de gestão de saúde que visa fortalecer a conexão entre pacientes e médicos, além de capacitar os pacientes a assumirem um papel mais ativo no monitoramento de sua própria saúde. A plataforma oferece um conjunto de funcionalidades que abrangem desde o registro detalhado de informações de saúde até a comunicação direta entre pacientes e médicos, tudo isso com foco na segurança e privacidade dos dados.

## Arquiteturas e Padrões Arquiteturais utilizados
**HisCare** utiliza padrões modernos de arquitetura, incluindo:

- **Micro-Front End (MFE)**: Modulariza interfaces em microcomponentes para facilitar a escalabilidade.

- **Clean Architecture**: Separa responsabilidades entre camadas para maior clareza e manutenção.

- **Vertical Slice**: Organiza o código por funcionalidades, melhorando o isolamento e a testabilidade.

## Artefatos Gerados
1. *Microserviço (Node.js & MongoDB)* - responsável pelo cadastro do usuário do sistema que é o paciente;
2. *Azure Functions (C# & Microsoft SQL Server)* - responsável pelo cadastro do usuário do sistema que é o médico/doutor;
3. *BFF (Node.JS)* -  no BFF colocamos os links gerados do Microseviço (MS) e do Azure Functions para realizar as requisições;
4. *Docker & Docker HUB* - para containerizar o Microserviço e o BFF, subimos tanto o MS e o BFF no DockerHUB e utilizamos o Azure Container Apps para criar containers virtuais para realizar as requisições do MS e da Azure Functions dentro do BFF
5. *AWS API Gateway* - utilizamos esse artefato para centralizar as rotas do BFF, então fizemos os apontamentos das rotas do nosso BFF no API Gateway do AWS;
6. Arc42 - Documentação
7. ATAM - Documentação

## Como funciona a arquitetura?
O HisCare utiliza o padrão MFE para dividir a interface entre as funcionalidades de médicos e pacientes. No backend, uma combinação de microserviços e Azure Functions garante alta escalabilidade e integração com diferentes bancos de dados. As requisições dos usuários passam pelo AWS API Gateway, que direciona as chamadas ao BFF para orquestração e envio aos serviços relevantes.

![Arquitetura do HisCare](https://github.com/diogobonet/hiscare-software-architecture/blob/main/docs/images/diagram.jpg)


## Tecnologias Utilizadas

### Linguagens e Frameworks:

- **Backend**: Node.js, Azure Functions (C#)

- **Banco de Dados**: MongoDB Atlas (não relacional) e Microsoft SQL Server (relacional);

### Infraestrutura:

- **Docker & Docker Hub** para containerização.
- **Azure Container Apps** para orquestração.
- **AWS API Gateway** para centralização de rotas.

### Documentação e Modelagem:

- **Arc42** para documentação arquitetural.
- **ATAM** para análise de trade-offs arquiteturais.

## Testes
Os testes da arquitetura foram desenvolvidos usando Jest, garantindo que todas as camadas e interações da aplicação sigam os princípios definidos pela Clean Architecture. Utilizamos esse teste arquitetural apenas no Microserviço de paciente (Node.js). 
O foco está em validar a correta separação de responsabilidades e a interação entre as camadas.
Para rodar o teste, executar o seguinte comando:

```npm run test -- --coverage```

---

# EN

## Table of Contents
1. [What is HisCare?](#what-is-hiscare)
2. [Architectures and Design Patterns](#architectures-and-design-patterns)
3. [Generated Artifacts](#generated-artifacts)
4. [How the Architecture Works](#how-the-architecture-works)
5. [Technologies Used](#technologies-used)
6. [Tests](#tests)

## What is HisCare?
HisCare is a comprehensive health management solution designed to strengthen the connection between patients and doctors, empowering patients to take a more active role in monitoring their own health. The platform provides a wide range of functionalities, from detailed health information recording to direct communication between patients and doctors, all with a focus on data security and privacy.

## Architectures and Design Patterns
**HisCare** leverages modern architecture patterns, including:

- **Micro-Front End (MFE):** Modularizes the interface into microcomponents, enabling scalability and easier maintenance.

- **Clean Architecture:** Ensures clear separation of concerns across layers for better readability and maintainability.

- **Vertical Slice:** Structures code by features, improving isolation and testability.

## Generated Artifacts
1. *Microservice (Node.js & MongoDB)* - Responsible for handling patient user registration;
2. *Azure Functions (C# & Microsoft SQL Server)* - Handles doctor user registration;
3. *BFF (Node.js)* - Acts as a middleware, linking microservices (MS) and Azure Functions to handle requests;
4. *Docker & Docker Hub* - Containers both the Microservice and the BFF. They are hosted on Docker Hub, with virtual containers managed by Azure Container Apps to handle MS and Azure Functions requests through the BFF;
5. *AWS API Gateway* - Centralizes BFF routes by mapping its endpoints to the API Gateway;
6. Arc42 - Architectural documentation;
7. ATAM - Documentation for architectural trade-off analysis.

## How the Architecture Works
HisCare uses the MFE pattern to divide the user interface into modules for doctors and patients. On the backend, a combination of microservices and Azure Functions ensures scalability and integration with both relational and non-relational databases. User requests are routed through the **AWS API Gateway**, which forwards them to the **BFF** for orchestration and dispatch to the appropriate services.

![HisCare Architecture](https://github.com/diogobonet/hiscare-software-architecture/blob/main/docs/images/diagram.jpg)

## Technologies Used

### Languages and Frameworks:

- **Backend:** Node.js, Azure Functions (C#)

- **Databases:** MongoDB Atlas (non-relational) and Microsoft SQL Server (relational);

### Infrastructure:

- **Docker & Docker Hub** for containerization.
- **Azure Container Apps** for orchestration.
- **AWS API Gateway** for route centralization.

### Documentation and Modeling:

- **Arc42** for architectural documentation.
- **ATAM** for analyzing architectural trade-offs.

## Tests
The architecture tests were developed using **Jest**, ensuring that all application layers and interactions adhere to the principles defined by Clean Architecture. These tests focus on the patient microservice (Node.js).

The goal is to validate the correct separation of responsibilities and interaction between layers.  
To run the tests, use the following command:

```
npm run test -- --coverage
```