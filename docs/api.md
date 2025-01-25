# API Documentation

## Base URL

- https://localhost:5043/api/v1

## Endpoints

### AuthController

#### **POST /login**

Autentica o usuário e retorna um token de acesso.

- **Request Body:**

```json
{
  "email": "user@example.com",
  "password": "password123"
}
```

- **Responses:**

| Código de Status | Descrição                  | Corpo                                                               |
| ---------------- | -------------------------- | ------------------------------------------------------------------- |
| 200 OK           | Autenticação bem-sucedida. | `{ "access_token": "token" }`                                       |
| 401 Unauthorized | Credenciais inválidas.     | `{ "message": "Email address or password provided is incorrect." }` |

---

### UserController

#### **POST /users**

Cria um novo usuário.

- **Request Body:**

```json
{
  "fullName": "Carlos Souza",
  "email": "carlos.souza@example.com",
  "password": "senha123"
}
```

- **Responses:**

| Código de Status | Descrição                   | Corpo                                                                                               |
| ---------------- | --------------------------- | --------------------------------------------------------------------------------------------------- |
| 201 Created      | Usuário criado com sucesso. | `{ "userId": 1, "fullName": "Carlos Souza", "email": "carlos.souza@example.com","avatarUrl": null}` |
| 409 Conflict     | E-mail já existe.           | `{ "message": "Usuário com email carlos.souza@example.com já existe." }`                            |

---

#### **PUT /users/upload-avatar**

Atualiza o avatar do usuário.

- **Request Body:**

  - Formulário: Arquivo de imagem.

- **Responses:**

| Código de Status | Descrição                      | Corpo                                                                          |
| ---------------- | ------------------------------ | ------------------------------------------------------------------------------ |
| 200 OK           | Avatar atualizado com sucesso. | `{ "message": "Avatar updated successfully.", "avatar_url": "url_do_avatar" }` |
| 401 Unauthorized | Não autenticado.               | `{ "message": "You need to provide a valid JWT token in the header." }`        |

---

#### **PUT /users/me/update**

Atualiza os dados do usuário autenticado.

- **Request Body:**

```json
{
  "name": "Carlos Souza Atualizado",
  "email": "carlos.souza@novoemail.com"
}
```

- **Responses:**

| Código de Status | Descrição                       | Corpo                                                                                                                                                                   |
| ---------------- | ------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 200 OK           | Usuário atualizado com sucesso. | `{ "userId": 1, "fullName": "Carlos Souza Atualizado", "email": "carlos.souza@novoemail.com", "avatarUrl": "/upload/avatar/dc6cbaa6-6398-4947-b01d-9515f1228933.jpg" }` |
| 401 Unauthorized | Não autenticado.                | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                                 |

---

#### **GET /users/me**

Retorna os dados do usuário autenticado.

- **Responses:**

| Código de Status | Descrição           | Corpo                                                                                                                                                      |
| ---------------- | ------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 200 OK           | Usuário encontrado. | `{ "userId": 1, "fullName": "Carlos Souza", "email": "carlos.souza@example.com", "avatarUrl": "/upload/avatar/dc6cbaa6-6398-4947-b01d-9515f1228933.jpg" }` |
| 401 Unauthorized | Não autenticado.    | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                    |

---

### JobController

#### **POST /jobs**

Cria um novo projeto.

- **Request Body:**

```json
{
  "name": "Editar um vídeo para o youtube",
  "dailyHours": 2,
  "totalHours": 10
}
```

- **Responses:**

| Código de Status | Descrição                | Corpo                                                                                                                                                                                              |
| ---------------- | ------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 201 Created      | Vaga criada com sucesso. | `{ "jobId": "53e1d53e-055f-429f-9d98-5114c587803c", "name": "Projeto compnent em react", "dailyHours": 1, "totalHours": 1, "remainingDays": 10, "valueJob: 29.50, "status": true, "userId": 35  }` |
| 401 Unauthorized | Não autenticado.         | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                                                            |

---

#### **GET /jobs**

Retorna todas as vagas de emprego do usuário autenticado.

- **Responses:**

| Código de Status | Descrição                | Corpo                                                                                                                                                                                                |
| ---------------- | ------------------------ | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 200 OK           | Vagas encontradas.       | `[ { "jobId": "53e1d53e-055f-429f-9d98-5114c587803c", "name": "Projeto compnent em react", "dailyHours": 1, "totalHours": 1, "remainingDays": 10, "valueJob: 29.50, "status": true, "userId": 35} ]` |
| 401 Unauthorized | Não autenticado.         | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                                                              |
| 404 Not Found    | Nenhuma vaga encontrada. | `{ "message": "No jobs found." }`                                                                                                                                                                    |

---

#### **GET /jobs/{jobId}**

Retorna os detalhes de uma vaga específica.

- **Responses:**

| Código de Status | Descrição            | Corpo                                                                                                                                                                                             |
| ---------------- | -------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 200 OK           | Vaga encontrada.     | `{ "jobId": "53e1d53e-055f-429f-9d98-5114c587803c", "name": "Projeto compnent em react", "dailyHours": 1, "totalHours": 1, "remainingDays": 10, "valueJob: 29.50, "status": true, "userId": 35 }` |
| 401 Unauthorized | Não autenticado.     | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                                                           |
| 404 Not Found    | Vaga não encontrada. | `{ "message": "Job not found." }`                                                                                                                                                                 |

---

### PlanningController

#### **POST /planning**

Cria um novo planejamento para o usuário autenticado.

- **Request Body:**

```json
{
  "monthlyBudget": 7000,
  "daysPerWeek": 5,
  "hoursPerDay": 10,
  "vacationPerYear": 4
}
```

- **Responses:**

| Código de Status | Descrição                        | Corpo                                                                                                                                                                         |
| ---------------- | -------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 201 Created      | Planejamento criado com sucesso. | `{ "planningId": "b22e7c51-fed1-4ac9-b335-56b1b0f2937f", "monthlyBudget": 7000, "daysPerWeek": 5, "hoursPerDay": 10, "vacationPerYear": 4, "valueHour": 35.00, "userId: 35 }` |
| 401 Unauthorized | Não autenticado.                 | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                                       |

---

#### **GET /planning**

Retorna o planejamento do usuário autenticado.

- **Responses:**

| Código de Status | Descrição                | Corpo                                                                                                                                                                          |
| ---------------- | ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| 200 OK           | Planejamento encontrado. | `{ "planningId": "b22e7c51-fed1-4ac9-b335-56b1b0f2937f", "monthlyBudget": 7000, "daysPerWeek": 5, "hoursPerDay": 10, "vacationPerYear": 4, "valueHour": 35.00, "userId: 35  }` |
| 401 Unauthorized | Não autenticado.         | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                                        |

---

#### **PUT /planning**

Atualiza o planejamento do usuário autenticado.

- **Request Body:**

```json
{
  "planningId": "2618e37a-307e-48c9-9a63-1356b8c4d270",
  "monthlyBudget": 7000,
  "daysPerWeek": 5,
  "hoursPerDay": 10,
  "vacationPerYear": 4
}
```

- **Responses:**

| Código de Status | Descrição                            | Corpo                                                                                                                                                                         |
| ---------------- | ------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 200 OK           | Planejamento atualizado com sucesso. | `{ "planningId": "b22e7c51-fed1-4ac9-b335-56b1b0f2937f", "monthlyBudget": 7000, "daysPerWeek": 5, "hoursPerDay": 10, "vacationPerYear": 4, "valueHour": 35.00, "userId: 35 }` |
| 401 Unauthorized | Não autenticado.                     | `{ "message": "You need to provide a valid JWT token in the header." }`                                                                                                       |
| 404 Not Found    | Planejamento não encontrado.         | `{ "message": "Planning not found." }`                                                                                                                                        |

---

## Autenticação

A API usa autenticação JWT. Inclua o token no cabeçalho da requisição:

```http
Authorization: Bearer {token}
```

## Erros Comuns

| Código de Status          | Descrição                                                |
| ------------------------- | -------------------------------------------------------- |
| 400 Bad Request           | A requisição está mal formada ou faltam parâmetros.      |
| 401 Unauthorized          | O token de autenticação não foi fornecido ou é inválido. |
| 403 Forbidden             | O Usuário não tem permisão para acessar a rota/recurso.  |
| 404 Not Found             | O recurso não foi encontrado.                            |
| 409 Conflict              | O recurso já existe (por exemplo, e-mail duplicado).     |
| 500 Internal Server Error | Erro interno do servidor.                                |

---

## Licença

Esta API é fornecida sob a licença MIT.

```

Este arquivo simula o Swagger, oferecendo uma documentação detalhada dos endpoints, parâmetros e respostas da sua API.
```
