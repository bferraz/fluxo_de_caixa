## Fluxo de Caixa

Esta é uma aplicação simples de fluxo de caixa, porém, que emprega boas práticas de arquitetura de software, nela são utilizados os conceitos de DDD, microsserviços, testes (apenas no contexto de lançamentos pela simplicidade na codificação, não que não seja necessário nos demais serviços), mensageria e API Gateway. Ela é composta por 4 microsserviços, sendo estes: API Gateway, API de Identidade, API de Lançamentos e API de Relatórios, essas APIs foram construídas em .NET Core e rodam em containers Docker. O API Gateway foi desenvolvido utilizando a biblioteca [Ocelot](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html), ela é responsável pelo controle de rota e validação do JWT Token, de forma que caso o usuário não esteja devidamente autenticado, sua requisição nem bata nas APIs internas. Os demais microsserviços possuem cada um seu banco de dados, sendo dois SQL Servers (Databases diferentes rodando em um mesmo servidor containerizado) e um o [MongoDB](https://www.mongodb.com/) para os relatórios. Esses microsserviços comunicam-se entre si utilizando uma fila do [Rabbit MQ](https://www.rabbitmq.com).

#### Desenho da solução:

</br>
<img src="https://github.com/bferraz/fluxo_de_caixa/blob/main/img/DesenhoSolucao.drawio.png" />
</br>

## Como rodar esse projeto

Como pré-requisitos, será necessário possuir o [Docker](https://www.docker.com/) instalado em sua máquina e o [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/) ou [VS Code](https://code.visualstudio.com/) com o [SDK do .NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) ou superior.

### Subindo os containers com Docker-Compose

Primeiramente, para subir as aplicações, através de um prompt de comando, vá até o diretório `Docker` na raíz do projeto, onde se encontra o arquivo `fluxo_de_caixa.yml` e rode o seguinte comando:

```powershell
docker-compose -f fluxo_de_caixa.yml up
```

Com isso todas as aplicações estarão funcionando, porém, ainda falta inicializar os bancos de dados relacionais.

### Inicializando os bancos de dados relacionais

Para iniciar o banco de dados da API de Indentidade no Visual Studio, rode o seguinte comando:

No Package Manager Console, execute

```powershell
Update-Database -StartUpProject FluxoDeCaixa.Identidade.API
```

</br>

Para iniciar o banco de dados da API de Lançamentos no Visual Studio, selecione o projeto **FluxoDeCaixa.Lancamentos.Infra** como **Default Projec** et rode o seguinte comando:

```powershell
Update-Database -StartUpProject FluxoDeCaixa.Lancamentos.API
```

</br>

>Nota: Se você estiver utilizando o VS Code ou outra ferramenta, pode rodar os comandos do Entity Framework através da linha de comando do dotnet ([documentação](https://www.entityframeworktutorial.net/efcore/cli-commands-for-ef-core-migration.aspx))

</br>

## Utilização das APIs

Todas as requisições são feitas através do API Gateway, ele responde no endereço http://localhost:5003. seguem abaixo as os exemplos de requisição

#### Criação de Usuário

Request:

```powershell
POST /auth/api/identidade/new-account HTTP/1.1
Host: localhost:5003
Content-Type: application/json
Content-Length: 166

{
    "Name": "Rafael",
    "Cpf": "21892227029",
    "Email": "teste@teste.com",
    "Password": "Password@123",
    "PasswordConfirm": "Password@123"
}
```

Response:

```powershell
{
    "accessToken": "[TOKEN]",
    "expiresIn": 7200
}
```
#### Login

Request:

```powershell
POST /auth/api/identidade/login HTTP/1.1
Host: localhost:5003
Content-Type: application/json
Content-Length: 76

{
    "Email": "b20-ferraz@hotmail.com",
    "Password": "Password@123"
}
```

Response:

```powershell
{
    "accessToken": "[TOKEN]",
    "expiresIn": 7200
}
```

#### Informações da Conta

Request:

```powershell
GET /cashier/api/account/show-info HTTP/1.1
Host: localhost:5003
Authorization: Bearer [TOKEN]
```

Response:

```powershell
{
    "value": 0,
    "lastUpdate": "2022-10-02T14:04:30.7233618"
}
```

#### Lançamento de Crédito/Débito

Request:

```powershell
POST /cashier/api/account/add-entry HTTP/1.1
Host: localhost:5003
Authorization: Bearer [TOKEN]
Content-Type: application/json

{
    "Type": 1,
    "Value": 300,
    "Description": "Teste"
}
```

Response:

```powershell
{
    "message": "Lançamento criado com sucesso"
}
```

#### Relatório Diário Consolidado

Request:

```powershell
GET /reports/api/report/show-daily-report?month=10&year=2022&day=05 HTTP/1.1
Host: localhost:5003
Authorization: Bearer [TOKEN]
```

Response:

```powershell
[
    {
        "userId": "ad207d72-a2df-4918-86a0-4a9e1546fd68",
        "userName": "Rafael",
        "entryType": 1,
        "entryValue": 400,
        "accountValueAfterEntry": 400,
        "entryDescription": "Teste",
        "entryDate": "2022-10-05T04:02:49.032Z"
    },
    {
        "userId": "ad207d72-a2df-4918-86a0-4a9e1546fd68",
        "userName": "Rafael",
        "entryType": 1,
        "entryValue": 300,
        "accountValueAfterEntry": 700,
        "entryDescription": "Teste",
        "entryDate": "2022-10-05T04:03:50.677Z"
    },
    {
        "userId": "ad207d72-a2df-4918-86a0-4a9e1546fd68",
        "userName": "Rafael",
        "entryType": 1,
        "entryValue": 300,
        "accountValueAfterEntry": 1000,
        "entryDescription": "Teste",
        "entryDate": "2022-10-05T04:14:59.233Z"
    }
]
```
