# Desafio - O Boticário

## “Eu revendedor ‘O Boticário’ quero ter benefícios de acordo com o meu volume de vendas”. 
Camila Martins Dutra

Foi criada uma solução backend utilizando:
* Framework Microsoft .NET Core 3.1 (VS 2019)
* Banco de dados MySQL (Workbench 8.0)
* Ferramenta de testes Postman

## Instruções iniciais:
Executar o script presente dentro na pasta DB para criar a database que será utilizada.
Ao abrir a solution no Visual Studio 2019, no arquivo appsettings.json, inserir a ConnectionString que será utilizada para a conexão.


Ainda no Visual Studio 2019, no Packege Manager Console, selecionar no Default Project o projeto 4- Infra\ 4.1- Data\Cashback.Data e rodar os comandos:
* add-migration "adding migrations " 
* update-database 


Para realizar a autenticação e poder acessar os endpoints pelo Swagger:
* Cadastrar novo usuário (POST api/v1/Reseller)
* Fazer o login e obter o token JWT (POST api/v1/Auth/authenticate)
* No botão "Authorize" no Swagger inserir o token adicionando a flag Bearer (Bearer -token-)


Para auxiliar nos testes foi criada uma collection na pasta Postman_Collection.


## Foram realizados:
* Testes automatizados de unidade e integração utilizando XUnit e Moq
* Autenticação por JWT
* Logs da aplicação utilizando ILogger
