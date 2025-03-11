# Aplicativo Todo List (WPF)

Um aplicativo simples para gerenciar tarefas, desenvolvido em WPF com arquitetura de **Vertical Slices**.

## Estrutura do Projeto

- **Application**: Camada de front-end (WPF) com servi�os para comunica��o com a API.
- **Infrastructure**: Reposit�rios e `DatabaseContext` para acesso a dados (Entity Framework).
- **Domain**: Modelos de dados e valida��es (FluentValidation).
- **Presentation**: API para expor endpoints e integrar com o front-end.

## Bibliotecas Utilizadas

- **HandyControls**: Componentes UI modernos e personaliz�veis para WPF.
- **CommunityToolkit.MVVM**: Simplifica a implementa��o do padr�o MVVM.
- **Refit**: Cliente HTTP para comunica��o com a API de forma declarativa.
- **FluentValidation**: Valida��es robustas no modelo de dom�nio.
- **EntityFramework**: Mapeamento objeto-relacional (ORM) para banco de dados.
- **Microsoft.Extensions.DependencyInjection**: Framework para inje��o de depend�ncia, permitindo gerenciar as depend�ncias da aplica��o de forma desacoplada e configur�vel. Suporta diferentes escopos de vida �til dos servi�os, como Singleton, Scoped e Transient.

## Configura��o  (Escolha um dos m�todos)
### M�todo 1: Docker (n�o necessita de banco de dados instalado)

A API ser� criada em um container no Docker utilizando Sql Server. 

```#!/bin/bash
git clone https://github.com/Alvarez-T/YourTask.git
cd YourTask
docker-compose build
docker-compose up migrations
docker-compose up
```

### M�todo 2: copiar e colar no Prompt de comando (Necess�rio possuir SQL Server instalado)

```#!/bin/bash
git clone https://github.com/Alvarez-T/YourTask.git
cd YourTask/YourTask.Infrastructure
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:DefaultConnection "Server=localhost\SQLEXPRESS;Database=YourTaskDB;User Id=sa;Password=1234Abc!@#;Trusted_Connection=True;"
dotnet tool install --global dotnet-ef
dotnet ef database update --startup-project ../YourTask.Presentation
cd ../YourTask.Presentation
dotnet run
```

### M�todo 3: Passo � passo manual

1. **Clone o reposit�rio**:
  ```bash
  git clone https://github.com/Alvarez-T/YourTask.git
  cd YourTask
  ```
2. **Defina o User Secrets** (configure o ConnectionString e senha com base no seu servidor local)
 ```bash
 cd /YourTask.Infrastructure
 dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:DefaultConnection "Server=localhost\SQLEXPRESS;Database=YourTaskDB;User Id=sa;Password=1234Abc!@#;Trusted_Connection=True;"
 ``` 
3. **Instale EF Core CLI Tools** (caso n�o esteja instalado):
```bash
dotnet tool install --global dotnet-ef
```
4. **Aplique as migra��es do EF Core**
```bash
dotnet ef database update --startup-project ../YourTask.Presentation
```
5. **Execute a API (Presentation)** para persist�ncia de dados.
```bash
cd ../YourTask.Presentation 
dotnet run
```
6. Execute o projeto WPF (Application) para iniciar a interface.
7. Crie e edite novas tarefas.

# Screenshots

### Tela principal

![YourTask](Screenshots/YourTask.png)

### Cadastro de Tarefa

![Task](Screenshots/YourTask_Tarefa.png)