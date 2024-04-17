# Microsserviços para E-commerce

Este repositório contém a estrutura e os arquivos necessários para criar 3 microsserviços para um sistema de e-commerce, utilizando .NET 8 e Docker para orquestração de contêineres.

## Estrutura de pastas

- **container**: Contém os arquivos Dockerfile e docker-compose.yml para a construção e execução dos contêineres.
- **microsservicos**: Projetos .NET 8 para os microsserviços.
- **data**: Pasta não rastreada pelo git, usada pelos contêineres para armazenamento de dados.

## Microsserviços e Bancos de Dados no Container

Os microsserviços são divididos da seguinte forma:

- **autenticacao-api**:
  - Microsserviço para autenticação de usuários. 
  - Executa na porta 5000.
- **pedidos-api**: 
  - Microsserviço para cadastro de pedidos. 
  - Executa na porta 5001.
- **produtos-api**: 
  - Microsserviço para CRUD de produtos. 
  - Executa na porta 5002.

Cada microsserviço é conectado a um banco de dados MySQL, conforme abaixo:

- **mysql-autenticacao**: 
  - Banco de dados para o microsserviço de autenticação. 
  - Executa na porta externa 3316 e interna 3306.
- **mysql-pedidos**: 
  - Banco de dados para o microsserviço de pedidos. 
  - Executa na porta externa 3326 e interna 3306.
- **mysql-produtos**: 
  - Banco de dados para o microsserviço de produtos. 
  - Executa na porta externa 3336 e interna 3306.

Além dos microsserviços e bancos de dados, há outros serviços necessários para o funcionamento do sistema:

- **rabbitmq-ecommerce**: 
  - Fila RabbitMQ para processamento de pedidos. 
  - Interface web na porta 15672 e acesso CLI na porta 25672.
- **redis-ecommerce**: 
  - Redis para cache. 
  - Executa na porta 6379.
- **redisinsight-ecommerce**: 
  - Não é necessária sua execução para a aplicação 
  - Interface web para visualizar registros no Redis. 
  - Executa na porta externa 8010 e interna 8001.

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
- **Swagger**: Usada para definir, documentar e consumir APIs REST. 
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


## Instruções para Execução

- Entre na pasta container:
```
cd container
```
- Execute o comando para subir os containers:
```
docker-compose up -d
```
- No docker-compose.yml e no Dockerfile já tem as instruções que sobem a aplicação e executam as migrations para criação dos bancos de dados.
- Todo microsserviços utilizam o Swagger. Caso tenha mantido as portas do docker-file.yml, segue abaixo as urls:
  - [Autenticação](https://localhost:5000/swagger)
  - [Produtos](https://localhost:5002/swagger)
  - [Pedidos](https://localhost:5001/swagger) 

## Endpoints e Exemplos de Chamadas da API
Todos os microsserviços utilizam do Swagger para documentação e consumo das APIs.

### Autenticação
**Endpoint de login:**

[POST] /v1/autenticacao/login

Exemplo:

```json
{
  "email": "teste@teste.com",
  "senha": "123456"
}
```

### Produto
**Endpoint de login:**

[POST] /v1/autenticacao/login

Exemplo:

```json
{
  "email": "teste@teste.com",
  "senha": "123456"
}
```

### Pedido
**Endpoint de login:**

[POST] /v1/autenticacao/login

Exemplo:

```json
{
  "email": "teste@teste.com",
  "senha": "123456"
}
```

## Futuras Melhorias

### Testes de unidade
- Criação de mais casos de teste nos 3 microsserviços.

### Microsserviço de Autenticação
- Criar para ele ser o responsável por validar os tokens JWT de todos os microsserviços, além da geração de token para o usuário.
- Criar CRUD de usuário

### Microsserviço de Produto
- A rota de listagem de produtos está aberta, pois ainda não foi implementada no microsserviço de Pedido para que o mesmo se autentique antes de chamar este microsserviço.
Aguardar a alteração do microsserviço de Pedidos e deixar a rota privada.
- Aguardar a implementação de validação de token JWT para APIs no microsserviço de Autenticação, e começar a chamar o mesmo, ao invés de validar internamente.
- Criar lógica para abatimento de estoque do produto, lendo uma fila RabbitMQ.

### Microsserviço de Pedido
- Aguardar a implementação de validação de token JWT para APIs no microsserviço de Autenticação, e começar a chamar o mesmo, ao invés de validar internamente.
- Criar lógica para publicar em uma fila RabbitMQ para que o microsserviço de Produto cuide do abatimento de estoque.

## Observações

- Este README fornece uma visão geral do sistema e sua estrutura. Para detalhes sobre a implementação de cada microsserviço, consulte os arquivos de código-fonte.
- Certifique-se de ter o Docker instalado e em execução para criar e executar os contêineres conforme descrito neste README.
