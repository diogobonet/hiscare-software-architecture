# HisCare - Software Architecture
### Choose a language | Escolha uma linguagem
- PT-BR
- EN

## O que é o HisCare?
O HISCare é uma solução abrangente de gestão de saúde que visa fortalecer a conexão entre pacientes e médicos, além de capacitar os pacientes a assumirem um papel mais ativo no monitoramento de sua própria saúde. A plataforma oferece um conjunto de funcionalidades que abrangem desde o registro detalhado de informações de saúde até a comunicação direta entre pacientes e médicos, tudo isso com foco na segurança e privacidade dos dados.

## Ferramentas e Linguagens de Programação utilizadas

Para o desenvolvimento do sistema usaremos o Node.JS e Azure Functions. Já para o banco de dados usaremos o MongoDB Atlas (não relacional) e Microsoft SQL Server (relacional);


## Arquiteturas e Padrões Arquiteturais utilizados
1. Arquitetura Micro-Front End (MFE);
2. Clean Architecture;
3. Vertical Slice;

## Artefatos Gerados
1. *Microserviço (Node.js & MongoDB)* - responsável pelo cadastro do usuário do sistema que é o paciente;
2. *Azure Functions (C# & Microsoft SQL Server)* - responsável pelo cadastro do usuário do sistema que é o médico/doutor;
3. *BFF (Node.JS)* -  no BFF colocamos os links gerados do Microseviço (MS) e do Azure Functions para realizar as requisições;
4. *Docker & Docker HUB* - para containerizar o Microserviço e o BFF, subimos tanto o MS e o BFF no DockerHUB e utilizamos o Azure Container Apps para criar containers virtuais para realizar as requisições do MS e da Azure Functions dentro do BFF
5. *AWS API Gateway* - utilizamos esse artefato para centralizar as rotas do BFF, então fizemos os apontamentos das rotas do nosso BFF no API Gateway do AWS;
6. Arc42 - Documentação
7. ATAM - Documentação

## Como funciona a arquitetura?
Utilizando a estrutura de arquitetura MFE:

