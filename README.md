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

## Padrões e Práticas

Foram empregados diversos padrões de projeto para manter a estrutura organizada e escalável:

- **Repository**: Padrão para isolamento da lógica de acesso a dados.
- **Service**: Lógica de negócios e regras de negócios.
- **IoC**: Injeção de dependência para facilitar a troca de implementações.
- **Arquitetura em camadas**: Divisão clara entre a lógica de negócios, a interface do usuário e a infraestrutura.
- **Conceitos do SOLID**: Princípios de design para facilitar a manutenção e extensibilidade do código.
- **Soft delete**: Prática de marcar registros como "excluídos" em vez de removê-los permanentemente do banco de dados

## Camadas
Foram empregadas camadas para manter a estrutura organizada:

 - **Service**: Esta camada abriga o projeto da web que oferece a interface de programação de aplicativos (API), responsável por fornecer serviços para os clientes.
 - **Domain**: Aqui são definidas as entidades principais da aplicação e os contratos que regem seu comportamento. É o coração da lógica de negócios, onde as regras essenciais são encapsuladas.
 - **Data**: Esta camada cuida da interação com o banco de dados e sua implementação, garantindo a persistência dos dados. É responsável pela conexão com o banco de dados e pelas operações de leitura e gravação.
 - **CrossCutting**: Responsável por registrar as dependências entre os diferentes componentes do sistema, garantindo que as instâncias necessárias estejam disponíveis quando solicitadas. Isso promove a modularidade e a manutenibilidade do código.
 - **Application**: Aqui são implementadas as classes que fornecem os serviços específicos da aplicação. Esta camada contém as Services, que encapsulam a lógica de negócios, os Data Transfer Objects (DTOs), que são objetos utilizados para transferir dados entre as diferentes camadas, e os mapeamentos, que são responsáveis por transformar os objetos do domínio em DTOs e vice-versa.


## Instruções para Execução

- Caso esteja em um sistema operacional windows, e por ter um arquivo .sh em 3 containers,
 é necessário executar o comando abaixo antes de clonar o repositório, devido a quebra de linha de linux e windows:
```
  git config --global core.autocrlf false
```
- Entre na pasta container:
```
cd container
```
- Build a imagem:
```
docker-compose build
```
- Execute o comando para subir os containers:
```
docker-compose up -d
```
- As instruções necessárias para iniciar a aplicação e executar as migrações para criar os bancos de dados já estão configuradas tanto no docker-compose.yml quanto no Dockerfile.
- Todos os microsserviços adotam o Swagger para documentação de suas APIs. Se as portas definidas no docker-compose.yml permaneceram inalteradas, abaixo estão as URLs correspondentes:
  - [Autenticação](https://localhost:5000/swagger)
  - [Produtos](https://localhost:5002/swagger)
  - [Pedidos](https://localhost:5001/swagger) 

## Endpoints e Exemplos de Chamadas da API
Todos os microsserviços utilizam do Swagger para documentação e consumo das APIs.

Os microsserviços de Produtos e Pedidos requerem autenticação. Portanto, é crucial primeiro acessar o microsserviço de Autenticação, utilizando as credenciais fornecidas na explicação abaixo.

### Autenticação
#### Endpoint de login (USAR ESSE USUÁRIO PARA AUTENTICAÇÃO)

Primeiro endpoint a ser consumido.

[POST] /v1/autenticacao/login

Exemplo de envio:

```json
{
  "email": "teste@teste.com",
  "senha": "123456"
}
```

Exemplo de retorno:

```json
{
  "token": "string",
  "tokenType": "string",
  "usuario": {
    "id": 0,
    "nome": "string"
  }
}
```


### Produto
#### Endpoint de cadastro de produto

Antes de fazer um pedido, é crucial cadastrar os produtos e observar seus IDs para utilizá-los no microsserviço de Pedidos.

[POST] /v1/produtos

Exemplo de envio:

```json
{
  "nome": "mouse",
  "descricao": "mouse gammer",
  "valor": 100.99,
  "categoria": "periféricos",
  "quantidadeDisponivel": 200,
  "urlImagem": "https://picsum.photos/200/300"
}
```

Exemplo de retorno:

```json
{
  "id": 1,
  "nome": "mouse",
  "descricao": "mouse gammer",
  "valor": 100.99,
  "categoria": "periféricos",
  "quantidadeDisponivel": 200,
  "urlImagem": "https://picsum.photos/200/300"
}
```


#### Endpoint de consulta de produtos

Você pode enviar os IDs dos produtos desejados separados por vírgula através da query string. Além disso, é possível indicar se deseja incluir os produtos excluídos, usando o parâmetro "incluirExcluidos" na query string, com os valores "true" ou "false".

[GET] /v1/produtos

Exemplo de retorno:

```json
[
  {
    "id": 1,
    "nome": "mouse",
    "descricao": "mouse gammer",
    "valor": 100.99,
    "categoria": "periféricos",
    "quantidadeDisponivel": 200,
    "urlImagem": "https://picsum.photos/200/300"
  }
]
```


#### Endpoint de atualização de produtos

[PUT] /v1/produtos/{idProduto}

Exemplo de envio:

```json
{
  "nome": "mouse",
  "descricao": "mouse gammer",
  "valor": 105.99,
  "categoria": "periféricos",
  "quantidadeDisponivel": 200,
  "urlImagem": "https://picsum.photos/200/300"
}
```

Exemplo de retorno:

```json
{
  "id": 1,
  "nome": "mouse",
  "descricao": "mouse gammer",
  "valor": 105.99,
  "categoria": "periféricos",
  "quantidadeDisponivel": 200,
  "urlImagem": "https://picsum.photos/200/300"
}
```


#### Endpoint de consulta de um produto específico

[GET] /v1/produtos/{idProduto}

Exemplo de retorno:

```json
{
  "id": 1,
  "nome": "mouse",
  "descricao": "mouse gammer",
  "valor": 105.99,
  "categoria": "periféricos",
  "quantidadeDisponivel": 200,
  "urlImagem": "https://picsum.photos/200/300"
}
```


#### Endpoint de exclusão de um produto

É aplicado o conceito de soft delete, o que significa que o produto não será removido permanentemente do banco de dados. Em vez disso, ele será marcado como excluído, utilizando a coluna DataHorarioExclusao.

[DELETE] /v1/produtos/{idProduto}


### Pedido
#### Endpoint de cadastro de pedido

Após a solicitação do pedido, apenas o status 200 é recebido, sem informações adicionais, pois o pedido será processado por meio de uma fila de execução. Para visualizar o pedido registrado, é necessário acessar o endpoint de consulta de pedidos.


[POST] /v1/pedidos

Exemplo de envio:

```json
{
  "itensPedido": [
    {
      "quantidade": 2,
      "idProduto": 1
    }
  ]
}
```


#### Endpoint de consulta de pedidos

Apenas os pedidos do usuário autenticado são retornados.

[GET] /v1/pedidos/

Exemplo de retorno:

```json
[
  {
    "id": 1,
    "valorTotal": 201.98,
    "status": "EM_PROCESSAMENTO",
    "dataHorarioCadastro": "2024-04-18T07:10:04.868497"
  }
]
```


#### Endpoint de consulta de um pedido específico

Apenas o pedido do usuário autenticado é retornado.

[GET] /v1/pedidos/{idPedido}

Exemplo de retorno:

```json
{
  "id": 1,
  "valorTotal": 201.98,
  "status": "EM_PROCESSAMENTO",
  "dataHorarioCadastro": "2024-04-18T07:10:04.868497"
}
```


#### Endpoint de consulta de pedidos detalhados

São retornados apenas os pedidos do usuário autenticado, acompanhados dos detalhes dos produtos.

[GET] /v1/pedidos/detalhado

Exemplo de retorno:

```json
[
  {
    "id": 1,
    "valorTotal": 201.98,
    "status": "EM_PROCESSAMENTO",
    "dataHorarioCadastro": "2024-04-18T07:10:04.868497",
    "itensPedido": [
      {
        "quantidade": 2,
        "nome": "mouse",
        "descricao": "mouse gammer",
        "valor": 100.99,
        "categoria": "periféricos",
        "urlImagem": "https://picsum.photos/200/300"
      }
    ]
  },
  {
    "id": 2,
    "valorTotal": 201.98,
    "status": "EM_PROCESSAMENTO",
    "dataHorarioCadastro": "2024-04-18T07:38:43.435065",
    "itensPedido": [
      {
        "quantidade": 2,
        "nome": "mouse",
        "descricao": "mouse gammer",
        "valor": 100.99,
        "categoria": "periféricos",
        "urlImagem": "https://picsum.photos/200/300"
      }
    ]
  }
]
```


#### Endpoint de consulta de um pedido específico detalhado

Apenas o pedido do usuário autenticado é retornado, juntamente com os detalhes dos produtos.

[GET] /v1/pedidos/{idPedido}/detalhado

Exemplo de retorno:

```json
{
  "id": 1,
  "valorTotal": 201.98,
  "status": "EM_PROCESSAMENTO",
  "dataHorarioCadastro": "2024-04-18T07:10:04.868497",
  "itensPedido": [
    {
      "quantidade": 2,
      "nome": "mouse",
      "descricao": "mouse gammer",
      "valor": 100.99,
      "categoria": "periféricos",
      "urlImagem": "https://picsum.photos/200/300"
    }
  ]
}
```

## Futuras Melhorias

### Testes de unidade
- Criação de mais casos de teste para os 3 microsserviços.

### Logs
- Implementar monitoramento de logs.
- Uma abordagem inicial pode envolver a publicação básica dos logs em uma fila e a criação de outro microsserviço para consumi-los e armazená-los em uma estrutura, como o Elasticsearch.

### Microsserviço de Autenticação
- Alterar o serviço para que ele seja o responsável por validar os tokens JWT de todos os microsserviços.
- Modificar o microsserviço para incluir a autenticação de serviços, além de usuários.
- Criar CRUD de usuário.

### Microsserviço de Produto
- Aguardar a implementação da validação de tokens JWT para APIs no microsserviço de Autenticação e iniciar a chamada a ele em vez de realizar a validação internamente.
- Desenvolver uma lógica para redução do estoque do produto, com base na leitura de uma fila RabbitMQ.

### Microsserviço de Pedido
- Aguardar a implementação da validação de tokens JWT para APIs no microsserviço de Autenticação e iniciar a chamada a ele em vez de realizar a validação internamente.
- Implementar a lógica para publicar mensagens em uma fila RabbitMQ, para que o microsserviço de Produto possa gerenciar o abatimento de estoque.

## Observações

- Este README fornece uma visão geral do sistema e sua estrutura. Para detalhes sobre a implementação de cada microsserviço, consulte os arquivos de código-fonte.
- Certifique-se de ter o Docker instalado e em execução para criar e executar os contêineres conforme descrito neste README.
