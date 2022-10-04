## Fluxo de Caixa

Esta é uma aplicação simples de fluxo de caixa, porém, que emprega boas práticas de arquitetura de software, nela são utilizados os conceitos de DDD, microsserviços, testes de unidade e testes de integração (apenas no contexto de lançamentos pela simplicidade na codificação, não que não fosse necessário nos demais serviços), mensageria e API Gateway. Ela é composta por 4 microsserviços, sendo estes: API Gateway, API de Identidade, API de Lançamentos e API de Relatórios, essas APIs foram construídas em .NET Core e rodam em containers Docker. O API Gateway foi desenvolvido utilizando a biblioteca Ocelot, ela é responsável pelo controle de rota e validação do JWT Token, de forma que caso o usuário não esteja devidamente autenticado, sua requisição nem bata nas APIs internas. Os demais microsserviços possuem cada um seu banco de dados, sendo dois SQL Servers (Databases diferentes rodando em um mesmo servidor containerizado) e um o MongoDB para os relatórios. Esses microsserviços comunicam-se entre si utilizando uma fila do Rabbit MQ.

#### Desenho da solução:

</br>
<img src="https://github.com/bferraz/fluxo_de_caixa/blob/main/img/DesenhoSolucao.drawio.png" />
</br>

## Como rodar esse projeto

Como pré-requisitos, será necessário possuir o Docker instalado em sua máquina e o Visual Studio ou VS Code com o SDK do .NET Core 3.1 ou superior.

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
Update-Database -StartUpProject API.Identidade
```

</br>

Para iniciar o banco de dados da API de Lançamentos no Visual Studio, rode o seguinte comando:

```powershell
Update-Database -StartUpProject API.Caixa
```

</br>

>Nota: Se você estiver utilizando o VS Code ou outra ferramenta, pode rodar os comandos do Entity Framework através da linha de comando do dotnet ([documentação](https://www.entityframeworktutorial.net/efcore/cli-commands-for-ef-core-migration.aspx))
