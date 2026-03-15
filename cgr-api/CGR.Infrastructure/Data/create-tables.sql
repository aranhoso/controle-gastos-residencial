-- Tabela só para referência, a criação real é feita via migrations do EF

CREATE TABLE "Pessoas" (
    "Id" UUID NOT NULL,
    "Nome" VARCHAR(200) NOT NULL,
    "Idade" INTEGER NOT NULL,

    CONSTRAINT "PK_Pessoas" PRIMARY KEY ("Id")
);

CREATE TABLE "Categorias" (
    "Id" UUID NOT NULL,
    "Descricao" VARCHAR(400) NOT NULL,
    "Finalidade" INTEGER NOT NULL,

    CONSTRAINT "PK_Categorias" PRIMARY KEY ("Id")
);

CREATE TABLE "Transacoes" (
    "Id" UUID NOT NULL,
    "Descricao" VARCHAR(400) NOT NULL,
    "Valor" NUMERIC(18,2) NOT NULL,
    "Tipo" INTEGER NOT NULL,
    "PessoaId" UUID NOT NULL,
    "CategoriaId" UUID NOT NULL,

    CONSTRAINT "PK_Transacoes" PRIMARY KEY ("Id"),

    CONSTRAINT "FK_Transacoes_Pessoas_PessoaId"
        FOREIGN KEY ("PessoaId")
        REFERENCES "Pessoas" ("Id")
        ON DELETE RESTRICT,

    CONSTRAINT "FK_Transacoes_Categorias_CategoriaId"
        FOREIGN KEY ("CategoriaId")
        REFERENCES "Categorias" ("Id")
        ON DELETE RESTRICT
);

CREATE INDEX "IX_Transacoes_PessoaId" ON "Transacoes" ("PessoaId");
CREATE INDEX "IX_Transacoes_CategoriaId" ON "Transacoes" ("CategoriaId");
