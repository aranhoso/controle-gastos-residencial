### Diagrama de Banco de Dados

```mermaid
erDiagram
    PESSOA ||--o{ TRANSACAO : "possui"
    CATEGORIA ||--o{ TRANSACAO : "classifica"

    PESSOA {
        Guid Id PK
        string Nome "Max 200 chars"
        int Idade
    }

    CATEGORIA {
        Guid Id PK
        string Descricao "Max 400 chars"
        int Finalidade "1-Receita, 2-Despesa, 3-Ambas"
    }

    TRANSACAO {
        Guid Id PK
        string Descricao "Max 400 chars"
        decimal Valor "Positivo"
        int Tipo "1-Receita, 2-Despesa"
        Guid PessoaId FK
        Guid CategoriaId FK
    }
```

### Subindo o PostgreSQL com Docker

1. Entre na pasta da API:

```bash
cd cgr-api
```

2. Suba o container do PostgreSQL:

```bash
docker compose up -d
```

3. (Opcional) Acompanhe os logs do banco:

```bash
docker compose logs -f postgres
```

4. (Quando precisar recriar o volume do banco local):

```bash
docker compose down -v
docker compose up -d
```

### Comandos de Migrations (Entity Framework Core)

1. Entre no projeto de startup (`CGR.Api`):

```bash
cd cgr-api/CGR.Api
```

2. Restaure os pacotes:

```bash
dotnet restore
```

3. Crie a migration inicial:

```bash
dotnet ef migrations add InitialCreate --project ../CGR.Infrastructure --startup-project . --context AppDbContext
```

4. Aplique as migrations no banco:

```bash
dotnet ef database update --project ../CGR.Infrastructure --startup-project . --context AppDbContext
```

5. (Opcional) Gere o script SQL da migration:

```bash
dotnet ef migrations script --project ../CGR.Infrastructure --startup-project . --context AppDbContext
```