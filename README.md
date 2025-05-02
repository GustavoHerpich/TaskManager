# Task Manager

Aplicação para controle de tarefas utilizando arquitetura **Clean Architecture** no back-end e **Vue 3** no front-end.

## Tecnologias usadas

- **Back-end**: C# (.NET), Entity Framework, FluentValidation, FluentResults
- **Front-end**: Vue 3 + TypeScript + Vuetify
- **Banco de Dados**: SQL Server
- **Containerização**: Docker
- **Gerenciamento de Secrets**: User-Secrets do .NET

## Como rodar o projeto

### Pré-requisitos

- Docker e Docker Compose instalados
- .NET 7.0 ou superior (caso queira rodar local sem Docker)
- Node.js 18+ (para rodar o front-end)

### Configurar Secrets

No projeto do back-end (`TaskManager.API`), adicione suas configurações sensíveis usando o `dotnet user-secrets`:

```bash
cd src/TaskManager.API
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings__DataBase" "Server=sqlserver,1433;Database=TaskManagerDb;User=sa;Password=task123@;Encrypt=True;TrustServerCertificate=True"
```

## Subir com Docker

Na raiz do projeto, execute:

```bash
docker-compose up --build
```

Isso irá:

- Subir a API (.NET) na porta `5000`
- Subir o banco de dados SQL Server na porta `1433`
- Subir o front-end Vue 3 na porta `3000`

---

### Rodar localmente (sem Docker)

Caso prefira rodar os serviços manualmente:

**Rodar o Back-end (.NET):**

```bash
cd src/TaskManager.API
dotnet run
```

**Rodar o Front-end (Vue 3):**

```bash
cd app
npm install
npm run dev
```


