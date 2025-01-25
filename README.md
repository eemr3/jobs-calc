# JobsCalc - Aplicação para Freelancers

JobsCalc é uma aplicação projetada para ajudar freelancers a calcular o custo de seus projetos e gerenciar suas tarefas. O projeto é um mono-repo que contém o frontend, o backend e a configuração necessária para integração total da aplicação.

## Tecnologias Utilizadas

- **Frontend**: [Next.js](https://nextjs.org/)
- **Backend**: [ASP.NET Core SDK 8](https://learn.microsoft.com/en-us/aspnet/core/)
- **Banco de Dados**: PostgreSQL
- **Containerização**: Docker e Docker Compose

---

## Estrutura de Pastas

```
JobsCalc/
  ├── docs/               # Documentação do projeto
  ├── src/
  │   └── JobsCalc/       # Backend da aplicação (ASP.NET Core)
  ├── web/                # Frontend da aplicação (Next.js)
  └── .env                # Variáveis de ambiente gerais
```

---

## Configuração

### Variáveis de Ambiente

#### Arquivo `.env.exemple` na raiz do projeto:

```env
POSTGRES_USER=postgres
POSTGRES_PASSWORD=12345678
POSTGRES_DB=finance_db
POSTGRES_PORT=5433

STRING_CONNECTION=Host=postgres;Port=5432;Database=jobs_db;Username=postgres;Password=12345678

JWT_SECRET_KEY=v9d8SVG1q0W+2ZdKZh2P/h+cbugbBgYHAx/RazclooO3tB56uRp+2lueaCJW9k/qUku0pMGEK9AsUsPoZ16Vjg==

NEXT_PUBLIC_API_URL=http://localhost:5043
```

#### Backend (`src/JobsCalc/.env.example`):

```env
STRING_CONNECTION=Host=postgres;Port=5432;Database=jobs_db;Username=postgres;Password=12345678
JWT_SECRET_KEY=v9d8SVG1q0W+2ZdKZh2P/h+cbugbBgYHAx/RazclooO3tB56uRp+2lueaCJW9k/qUku0pMGEK9AsUsPoZ16Vjg==
```

#### Frontend (`web/.env.example`):

```env
NEXT_PUBLIC_API_URL=http://localhost:5043
```

---

## Pré-requisitos

- [Docker](https://www.docker.com/) e [Docker Compose](https://docs.docker.com/compose/)
- [Node.js](https://nodejs.org/) versão 18 ou superior
- [PostgreSQL](https://www.postgresql.org/)

---

## Como Rodar o Projeto

### Passo 1: Clone o Repositório

```bash
git clone https://github.com/seu-usuario/JobsCalc.git
cd JobsCalc
```

### Passo 2: Configure as Variáveis de Ambiente

Certifique-se de renomear os arquivos `.env.example` para `.env` nas respectivas pastas.

### Passo 3: Inicie os Contêineres na raiz do projeto:

```bash
docker-compose up --build
```

### Passo 4: Acesse o Frontend

Após subir os contêineres, o frontend estará disponível em:

```
http://localhost:3000
```

### Passo 5: Teste o Backend

A API estará disponível em:

```
http://localhost:5043/api/v1
```

---

## Estrutura de Banco de Dados

- **Banco**: jobs_db
- Configurado no PostgreSQL via `docker-compose`
- Tabelas são gerenciadas pelo backend (ASP.NET Core).

#### Backend (`src/JobsCalc/`)

- Para gerar as tabelas

```bash
 dotnet ef database update
```

---

## Documentação

Toda a documentação detalhada da aplicação está disponível na pasta `docs/`.

---

## Contribuição

Sinta-se à vontade para abrir issues e pull requests para melhorias no projeto.

---

## Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

---

## Contato

- Autor: Emerson Moreira
- Email: [seu-email@example.com](mailto:eemr3@yahoo.com.br)
- GitHub: [Seu GitHub](https://github.com/eemr3)
