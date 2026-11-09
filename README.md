# TreinoFácil

API REST para personal trainers gerenciarem **alunos** e **fichas de treino**, com front-end em Bootstrap + jQuery (e, em breve, React + TypeScript).

Projeto de estudo e portfólio, criado a partir da rotina real de um personal trainer: cadastrar alunos, montar treinos (A, B, C...) e listar os exercícios de cada treino com séries e repetições.

> **Status:** em desenvolvimento. API concluída; front-end em construção.

## Tecnologias

| Camada | Tecnologias |
| --- | --- |
| Back-end | C#, ASP.NET Core (.NET 10), Entity Framework Core, API REST |
| Banco de dados | SQL Server 2022 (em Docker), EF Core Migrations |
| Front-end | HTML5, CSS3, Bootstrap 5, jQuery |
| Ferramentas | Git/GitHub, Docker, DotNetEnv (variáveis de ambiente) |

## Funcionalidades

- Cadastro, edição, listagem, busca por nome e exclusão de alunos
- Criação de treinos por aluno, com lista de exercícios (nome, séries, repetições e ordem)
- Repetições aceitam texto livre, como `10-12` ou `Até a falha`
- Validação de dados de entrada e respostas HTTP adequadas (`201`, `204`, `400`, `404`)
- Exclusão em cascata: ao excluir um aluno, seus treinos e exercícios também são excluídos

## Estrutura do projeto

```
TreinoFacil/
├── api/                  API em C# (ASP.NET Core)
│   ├── Controllers/      AlunosController, TreinosController
│   ├── Data/             AppDbContext (EF Core)
│   ├── Migrations/       Histórico do banco de dados
│   ├── Models/           Entidades e DTOs
│   ├── Api.http          Requisições de teste
│   └── Program.cs
├── web-bootstrap/        Front-end com Bootstrap + jQuery
├── .env.example          Modelo das variáveis de ambiente
└── README.md
```

## Como executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Ferramenta do EF Core: `dotnet tool install --global dotnet-ef`

### 1. Clonar o repositório

```bash
git clone https://github.com/SEU_USUARIO/treinofacil.git
cd treinofacil
```

### 2. Subir o SQL Server no Docker

Troque a senha por uma sua (mínimo de 8 caracteres, com maiúscula, minúscula, número e símbolo):

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SUA_SENHA_FORTE" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

Nas próximas vezes, basta `docker start sqlserver`.

### 3. Configurar as variáveis de ambiente

Copie o `.env.example` para `.env` na raiz do projeto e preencha com a mesma senha do passo anterior:

```
DB_HOST=localhost
DB_PORT=1433
DB_NAME=TreinoFacil
DB_USER=sa
DB_PASSWORD=SUA_SENHA_FORTE
```

O arquivo `.env` está no `.gitignore` e nunca deve ser enviado ao repositório.

### 4. Criar o banco de dados

```bash
cd api
dotnet ef database update
```

### 5. Executar a API

```bash
dotnet run --launch-profile http
```

A API sobe em `http://localhost:5014` (confira a porta no console). A especificação OpenAPI fica em `/openapi/v1.json`.

### 6. Testar

Abra o arquivo `api/Api.http` no Rider, no Visual Studio ou no VS Code (extensão REST Client) e execute as requisições.

### 7. Executar o front-end

Em outro terminal:

```bash
cd web-bootstrap
npx serve -l 5500
```

Acesse `http://localhost:5500`.

## Endpoints

### Alunos

| Método | Rota | Descrição |
| --- | --- | --- |
| GET | `/api/alunos?busca=maria` | Lista alunos (busca opcional por nome) |
| GET | `/api/alunos/{id}` | Obtém um aluno |
| POST | `/api/alunos` | Cria um aluno |
| PUT | `/api/alunos/{id}` | Atualiza um aluno |
| DELETE | `/api/alunos/{id}` | Exclui um aluno e seus treinos |

### Treinos

| Método | Rota | Descrição |
| --- | --- | --- |
| GET | `/api/treinos?alunoId=3` | Lista treinos (filtro opcional por aluno) |
| GET | `/api/treinos/{id}` | Obtém um treino com seus exercícios |
| POST | `/api/treinos` | Cria um treino com exercícios |
| PUT | `/api/treinos/{id}` | Atualiza o treino e substitui a lista de exercícios |
| DELETE | `/api/treinos/{id}` | Exclui um treino |

### Exemplo: criar um treino

```http
POST /api/treinos
Content-Type: application/json

{
  "alunoId": 3,
  "nome": "B - Perna",
  "descricao": "Descanso de 2-3 min entre as séries",
  "exercicios": [
    { "nome": "Stiff", "series": 3, "repeticoes": "Até a falha", "ordem": 1 },
    { "nome": "Cadeira extensora", "series": 3, "repeticoes": "10-12", "ordem": 2 }
  ]
}
```

Resposta `201 Created`:

```json
{
  "id": 6,
  "nome": "B - Perna",
  "alunoId": 3,
  "alunoNome": "Mario Ayala",
  "descricao": "Descanso de 2-3 min entre as séries",
  "exercicios": [
    { "id": 9, "nome": "Stiff", "series": 3, "repeticoes": "Até a falha", "ordem": 1 },
    { "id": 10, "nome": "Cadeira extensora", "series": 3, "repeticoes": "10-12", "ordem": 2 }
  ]
}
```

## Decisões técnicas

- **DTOs separados das entidades:** o cliente não define `Id` nem datas, e as respostas não expõem o modelo do banco.
- **Validação com DataAnnotations:** tamanhos máximos e intervalos (`[Required]`, `[MaxLength]`, `[Range]`) retornam `400` automaticamente.
- **Migrations do EF Core:** o esquema do banco é versionado junto com o código.
- **Segredos fora do Git:** a connection string é montada a partir de variáveis de ambiente carregadas de um `.env`.
- **`AsNoTracking()` nas consultas de leitura**, e `Include` para carregar aluno e exercícios sem consultas extras.
- **Front-end sem injeção de HTML:** os dados da API são inseridos com `.text()` do jQuery, o que evita XSS.

## Roadmap

- [x] CRUD de alunos
- [x] CRUD de treinos e exercícios (relacionamento 1:N)
- [x] Front-end com Bootstrap + jQuery: tela de alunos
- [ ] Front-end com Bootstrap + jQuery: tela de treinos
- [ ] Painel em React + TypeScript
- [ ] Autenticação com JWT
- [ ] Testes automatizados (xUnit)
- [ ] Agenda de aulas

## Capturas de tela

_Adicione aqui prints do Api.http e das telas do front-end (pasta `docs/`)._

## Autor

**Emanoel Henrique**
GitHub: [SEU_USUARIO](https://github.com/SEU_USUARIO) · LinkedIn: [seu-perfil](https://www.linkedin.com/in/seu-perfil)
