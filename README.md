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