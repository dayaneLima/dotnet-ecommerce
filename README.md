# Microsserviços para E-commerce

Este repositório contém a estrutura e os arquivos necessários para criar 3 microsserviços para um sistema de e-commerce, utilizando .NET 8 e Docker para orquestração de contêineres.

## Estrutura de pastas

- **container**: Contém os arquivos Dockerfile e docker-compose.yml para a construção e execução dos contêineres.
- **microsservicos**: Projetos .NET 8 para os microsserviços.
- **data**: Pasta não rastreada pelo git, usada pelos contêineres para armazenamento de dados.

## Microsserviços e Bancos de Dados

Os microsserviços são divididos da seguinte forma:

- **autenticacao-api**: 
-- Microsserviço para autenticação de usuários. 
-- Executa na porta 5000.
- **pedidos-api**: 
-- Microsserviço para cadastro de pedidos. 
-- Executa na porta 5001.
- **produtos-api**: 
-- Microsserviço para CRUD de produtos. 
-- Executa na porta 5002.

Cada microsserviço é conectado a um banco de dados MySQL, conforme abaixo:

- **mysql-autenticacao**: 
-- Banco de dados para o microsserviço de autenticação. 
-- Executa na porta externa 3316 e interna 3306.
- **mysql-pedidos**: 
-- Banco de dados para o microsserviço de pedidos. 
-- Executa na porta externa 3326 e interna 3306.
- **mysql-produtos**: 
-- Banco de dados para o microsserviço de produtos. 
-- Executa na porta externa 3336 e interna 3306.

## Serviços Adicionais

Além dos microsserviços e bancos de dados, há outros serviços necessários para o funcionamento do sistema:

- **rabbitmq-ecommerce**: 
-- Fila RabbitMQ para processamento de pedidos. 
-- Interface web na porta 15672 e acesso CLI na porta 25672.
- **redis-ecommerce**: 
-- Redis para cache. 
-- Executa na porta 6379.
- **redisinsight-ecommerce**: 
-- Não é necessária sua execução para a aplicação 
-- Interface web para visualizar registros no Redis. 
-- Executa na porta externa 8010 e interna 8001.

## Tecnologias Utilizadas

- **.NET 8**: Framework principal para os microsserviços.
- **MySql**: Banco de dados utilizado para persistência de dados.
- **Redis**: Cache para melhoria de performance.
- **RabbitMQ**: Fila de mensagens para comunicação assíncrona.
- **Docker**: Plataforma para desenvolvimento e execução de aplicativos em contêineres.

## Bibliotecas Utilizadas

Além das bibliotecas padrão do .NET 8, foram utilizadas outras para desenvolvimento e teste:

- **EntityFrameworkCore**: ORM para interação com o banco de dados.
- **MySql.EntityFrameworkCore**: Provedor específico para o MySQL.
- **JwtBearer**: Para autenticação baseada em tokens JWT.
- **AutoMapper**: Mapeamento de objetos.
- **FluentValidation**: Validação de entrada.
- **StackExchangeRedis**: Cliente para interagir com o Redis.
- **RabbitMQ.Client**: Cliente para interagir com o RabbitMQ.
- **Refit**: Cliente HTTP declarativo.
- **xunit**: Framework de teste para .NET.
- **Moq**: Biblioteca de mocking para teste.

## Padrões

Foram empregados diversos padrões de projeto para manter a estrutura organizada e escalável:

- **Repository**: Padrão para isolamento da lógica de acesso a dados.
- **Service**: Lógica de negócios e regras de negócios.
- **IoC**: Injeção de dependência para facilitar a troca de implementações.
- **Arquitetura em camadas**: Divisão clara entre a lógica de negócios, a interface do usuário e a infraestrutura.
- **Conceitos do SOLID**: Princípios de design para facilitar a manutenção e extensibilidade do código.

## Camadas
Foram empregadas camadas para manter a estrutura organizada:

 - **Service**: Contém o projeto web que fornece a API
 - **Domain**: Contém as entidades e contratos usadas para a aplicação
 - **Data**: Contém a parte de infraestrutura, que cuida da conexão com o banco de dados e a implementação do mesmo.
 - **CrossCutting**: Responsável por registrar as injeções de dependência
 - **Application**: Contém as classes necessárias que provêm serviço para a aplicação. Contém então as Services, Data Transfer Objects e os Mapeamentos.

## Observações

- Este README fornece uma visão geral do sistema e sua estrutura. Para detalhes sobre a implementação de cada microsserviço, consulte os arquivos de código-fonte.
- Certifique-se de ter o Docker instalado e em execução para criar e executar os contêineres conforme descrito neste README.
