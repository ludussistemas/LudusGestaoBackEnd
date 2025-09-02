# Documentação da API LudusGestao

## Visão Geral

Esta documentação descreve todas as rotas disponíveis na API LudusGestao, incluindo parâmetros de entrada, respostas e exemplos de uso.

**URL Base:** `https://api.ludusgestao.com` (ou sua URL de desenvolvimento)

## Autenticação

A maioria das rotas requer autenticação via JWT Token. O token deve ser incluído no header:
```
Authorization: Bearer {seu_token_jwt}
```

---

## 🔐 Autenticação

### POST /api/autenticacao/entrar
**Descrição:** Realiza login do usuário e retorna token de acesso.

**Parâmetros de Entrada:**
```json
{
  "email": "string",
  "senha": "string"
}
```

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "refresh_token_aqui",
    "expiresIn": 3600
  },
  "message": "Login realizado com sucesso"
}
```

**Resposta de Erro (401):**
```json
{
  "success": false,
  "message": "Credenciais inválidas"
}
```

### POST /api/autenticacao/refresh
**Descrição:** Renova o token de acesso usando refresh token.

**Parâmetros de Entrada:**
```json
{
  "refreshToken": "string"
}
```

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "token": "novo_token_jwt",
    "refreshToken": "novo_refresh_token",
    "expiresIn": 3600
  },
  "message": "Token renovado com sucesso"
}
```

---

## 👥 Usuários

### GET /api/usuarios
**Descrição:** Lista todos os usuários com paginação.

**Parâmetros de Query:**
- `page` (int): Número da página (padrão: 1)
- `limit` (int): Itens por página (padrão: 10)
- `search` (string): Termo de busca
- `fields` (string): Campos específicos a retornar (separados por vírgula)

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": "guid",
        "nome": "string",
        "email": "string",
        "dataCriacao": "2024-01-01T00:00:00Z",
        "situacao": "Ativo"
      }
    ],
    "totalItems": 100,
    "page": 1,
    "limit": 10,
    "totalPages": 10
  },
  "message": "Usuários obtidos com sucesso"
}
```

### GET /api/usuarios/{id}
**Descrição:** Obtém um usuário específico por ID.

**Parâmetros de Rota:**
- `id` (Guid): ID do usuário

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "id": "guid",
    "nome": "string",
    "email": "string",
    "dataCriacao": "2024-01-01T00:00:00Z",
    "situacao": "Ativo"
  },
  "message": "Usuário obtido com sucesso"
}
```

### POST /api/usuarios
**Descrição:** Cria um novo usuário.

**Parâmetros de Entrada:**
```json
{
  "nome": "string",
  "email": "string",
  "senha": "string",
  "situacao": "Ativo"
}
```

**Resposta de Sucesso (201):**
```json
{
  "success": true,
  "data": {
    "id": "guid",
    "nome": "string",
    "email": "string",
    "dataCriacao": "2024-01-01T00:00:00Z",
    "situacao": "Ativo"
  },
  "message": "Usuário criado com sucesso"
}
```

### PUT /api/usuarios/{id}
**Descrição:** Atualiza um usuário existente.

**Parâmetros de Rota:**
- `id` (Guid): ID do usuário

**Parâmetros de Entrada:**
```json
{
  "nome": "string",
  "email": "string",
  "situacao": "Ativo"
}
```

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "id": "guid",
    "nome": "string",
    "email": "string",
    "dataCriacao": "2024-01-01T00:00:00Z",
    "situacao": "Ativo"
  },
  "message": "Usuário atualizado com sucesso"
}
```

### DELETE /api/usuarios/{id}
**Descrição:** Remove um usuário.

**Parâmetros de Rota:**
- `id` (Guid): ID do usuário

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "message": "Usuário removido com sucesso"
}
```

---

## 🏢 Empresas

### GET /api/empresas
**Descrição:** Lista todas as empresas com paginação.

**Parâmetros de Query:**
- `page` (int): Número da página
- `limit` (int): Itens por página
- `search` (string): Termo de busca

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": "guid",
        "nome": "string",
        "cnpj": "string",
        "dataCriacao": "2024-01-01T00:00:00Z",
        "situacao": "Ativo"
      }
    ],
    "totalItems": 50,
    "page": 1,
    "limit": 10,
    "totalPages": 5
  }
}
```

### GET /api/empresas/{id}
**Descrição:** Obtém uma empresa específica por ID.

### POST /api/empresas
**Descrição:** Cria uma nova empresa.

### PUT /api/empresas/{id}
**Descrição:** Atualiza uma empresa existente.

### DELETE /api/empresas/{id}
**Descrição:** Remove uma empresa.

---

## 🏪 Filiais

### GET /api/filiais
**Descrição:** Lista todas as filiais com paginação.

**Parâmetros de Query:**
- `page` (int): Número da página
- `limit` (int): Itens por página
- `search` (string): Termo de busca

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": "guid",
        "nome": "string",
        "codigo": "string",
        "endereco": "string",
        "cidade": "string",
        "estado": "string",
        "cep": "string",
        "telefone": "string",
        "email": "string",
        "cnpj": "string",
        "responsavel": "string",
        "situacao": "Ativo",
        "dataAbertura": "2024-01-01T00:00:00Z",
        "empresaId": "guid",
        "empresa": {
          "id": "guid",
          "nome": "string"
        }
      }
    ],
    "totalItems": 30,
    "page": 1,
    "limit": 10,
    "totalPages": 3
  }
}
```

### GET /api/filiais/{id}
**Descrição:** Obtém uma filial específica por ID.

### POST /api/filiais
**Descrição:** Cria uma nova filial.

### PUT /api/filiais/{id}
**Descrição:** Atualiza uma filial existente.

### DELETE /api/filiais/{id}
**Descrição:** Remove uma filial.

---

## 🔐 Permissões

### GET /api/permissoes
**Descrição:** Lista todas as permissões.

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": [
    {
      "id": "guid",
      "nome": "string",
      "moduloPai": "string",
      "submodulo": "string",
      "descricao": "string"
    }
  ],
  "message": "Permissões obtidas com sucesso"
}
```

### GET /api/permissoes/{id}
**Descrição:** Obtém uma permissão específica por ID.

### GET /api/permissoes/modulos-pai
**Descrição:** Obtém todos os módulos pai disponíveis.

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": ["Usuarios", "Empresas", "Filiais", "Eventos"],
  "message": "Módulos pai obtidos com sucesso"
}
```

### GET /api/permissoes/submodulos
**Descrição:** Obtém todos os submódulos disponíveis.

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": ["Criar", "Editar", "Excluir", "Visualizar"],
  "message": "Submódulos obtidos com sucesso"
}
```

### GET /api/permissoes/modulo-pai/{moduloPai}
**Descrição:** Obtém permissões por módulo pai.

**Parâmetros de Rota:**
- `moduloPai` (string): Nome do módulo pai

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": [
    {
      "id": "guid",
      "nome": "string",
      "moduloPai": "Usuarios",
      "submodulo": "Criar",
      "descricao": "string"
    }
  ],
  "message": "Permissões do módulo obtidas com sucesso"
}
```

### GET /api/permissoes/submodulo/{submodulo}
**Descrição:** Obtém permissões por submódulo.

**Parâmetros de Rota:**
- `submodulo` (string): Nome do submódulo

---

## 👥 Grupos de Permissões

### GET /api/grupos-permissoes
**Descrição:** Lista todos os grupos de permissões com paginação.

### GET /api/grupos-permissoes/{id}
**Descrição:** Obtém um grupo de permissões específico por ID.

### POST /api/grupos-permissoes
**Descrição:** Cria um novo grupo de permissões.

### PUT /api/grupos-permissoes/{id}
**Descrição:** Atualiza um grupo de permissões existente.

### DELETE /api/grupos-permissoes/{id}
**Descrição:** Remove um grupo de permissões.

### GET /api/grupos-permissoes/{id}/usuarios
**Descrição:** Obtém todos os usuários de um grupo específico.

**Parâmetros de Rota:**
- `id` (Guid): ID do grupo de permissões

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": [
    {
      "id": "guid",
      "nome": "string",
      "email": "string"
    }
  ],
  "message": "Usuários do grupo obtidos com sucesso"
}
```

### POST /api/grupos-permissoes/{id}/usuarios
**Descrição:** Adiciona um usuário a um grupo de permissões.

**Parâmetros de Rota:**
- `id` (Guid): ID do grupo de permissões

**Parâmetros de Entrada:**
```json
{
  "usuarioId": "guid"
}
```

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "message": "Usuário adicionado ao grupo com sucesso"
}
```

### DELETE /api/grupos-permissoes/{id}/usuarios/{usuarioId}
**Descrição:** Remove um usuário de um grupo de permissões.

**Parâmetros de Rota:**
- `id` (Guid): ID do grupo de permissões
- `usuarioId` (Guid): ID do usuário

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "message": "Usuário removido do grupo com sucesso"
}
```

---

## 👤 Permissões de Usuário

### GET /api/usuarios-permissoes
**Descrição:** Lista todas as permissões de usuários com paginação.

### GET /api/usuarios-permissoes/{id}
**Descrição:** Obtém uma permissão de usuário específica por ID.

### POST /api/usuarios-permissoes
**Descrição:** Cria uma nova permissão de usuário.

### PUT /api/usuarios-permissoes/{id}
**Descrição:** Atualiza uma permissão de usuário existente.

### DELETE /api/usuarios-permissoes/{id}
**Descrição:** Remove uma permissão de usuário.

---

## 🎯 Clientes

### GET /api/clientes
**Descrição:** Lista todos os clientes com paginação.

### GET /api/clientes/{id}
**Descrição:** Obtém um cliente específico por ID.

### POST /api/clientes
**Descrição:** Cria um novo cliente.

### PUT /api/clientes/{id}
**Descrição:** Atualiza um cliente existente.

### DELETE /api/clientes/{id}
**Descrição:** Remove um cliente.

### GET /api/clientes/resumo
**Descrição:** Obtém um resumo estatístico dos clientes.

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "totalClientes": 150,
    "ativos": 120,
    "inativos": 30,
    "novosMes": 15,
    "pessoaJuridica": 45
  },
  "message": "Resumo de clientes obtido com sucesso"
}
```

---

## 📍 Locais

### GET /api/locais
**Descrição:** Lista todos os locais com paginação.

### GET /api/locais/{id}
**Descrição:** Obtém um local específico por ID.

### POST /api/locais
**Descrição:** Cria um novo local.

### PUT /api/locais/{id}
**Descrição:** Atualiza um local existente.

### DELETE /api/locais/{id}
**Descrição:** Remove um local.

### GET /api/locais/resumo
**Descrição:** Obtém um resumo estatístico dos locais.

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "totalLocais": 25,
    "ativos": 20,
    "inativos": 3,
    "manutencao": 2,
    "valorMedioHora": 150.50
  },
  "message": "Resumo de locais obtido com sucesso"
}
```

---

## 📅 Reservas

### GET /api/reservas
**Descrição:** Lista todas as reservas com paginação.

### GET /api/reservas/{id}
**Descrição:** Obtém uma reserva específica por ID.

### POST /api/reservas
**Descrição:** Cria uma nova reserva.

### PUT /api/reservas/{id}
**Descrição:** Atualiza uma reserva existente.

### DELETE /api/reservas/{id}
**Descrição:** Remove uma reserva.

---

## 💰 Recebíveis

### GET /api/recebiveis
**Descrição:** Lista todos os recebíveis com paginação.

### GET /api/recebiveis/{id}
**Descrição:** Obtém um recebível específico por ID.

### POST /api/recebiveis
**Descrição:** Cria um novo recebível.

### PUT /api/recebiveis/{id}
**Descrição:** Atualiza um recebível existente.

### DELETE /api/recebiveis/{id}
**Descrição:** Remove um recebível.

---

## 🛠️ Utilitários

### GET /api/utilitarios/cep/{cep}
**Descrição:** Busca endereço por CEP usando a API ViaCEP.

**Parâmetros de Rota:**
- `cep` (string): CEP a ser consultado (formato: 12345-678 ou 12345678)

**Resposta de Sucesso (200):**
```json
{
  "success": true,
  "data": {
    "cep": "12345-678",
    "rua": "Rua Exemplo",
    "bairro": "Bairro Exemplo",
    "cidade": "São Paulo",
    "estado": "SP",
    "numero": ""
  },
  "message": "Endereço encontrado com sucesso"
}
```

**Resposta de Erro (400):**
```json
{
  "success": false,
  "message": "CEP deve conter 8 dígitos"
}
```

**Resposta de Erro (404):**
```json
{
  "success": false,
  "message": "CEP não encontrado"
}
```

### POST /api/utilitarios/seed
**Descrição:** Executa o seed de dados base do sistema.

**Resposta de Sucesso (200):**
```json
{
  "message": "Dados base inseridos com sucesso!"
}
```

**Resposta de Erro (400):**
```json
{
  "message": "Dados já existem ou erro ao inserir dados base."
}
```

---

## 📊 Gerenciamento

### GET /api/gerenciamento
**Descrição:** Lista dados de gerenciamento com paginação.

### GET /api/gerenciamento/{id}
**Descrição:** Obtém um item de gerenciamento específico por ID.

### POST /api/gerenciamento
**Descrição:** Cria um novo item de gerenciamento.

### PUT /api/gerenciamento/{id}
**Descrição:** Atualiza um item de gerenciamento existente.

### DELETE /api/gerenciamento/{id}
**Descrição:** Remove um item de gerenciamento.

---

## 📝 Códigos de Status HTTP

- **200 OK**: Requisição bem-sucedida
- **201 Created**: Recurso criado com sucesso
- **400 Bad Request**: Dados inválidos na requisição
- **401 Unauthorized**: Não autorizado (token inválido ou ausente)
- **403 Forbidden**: Acesso negado
- **404 Not Found**: Recurso não encontrado
- **500 Internal Server Error**: Erro interno do servidor

---

## 🔧 Estrutura de Resposta Padrão

Todas as respostas seguem o padrão:

```json
{
  "success": true|false,
  "data": object|array|null,
  "message": "string"
}
```

### Campos:
- **success**: Indica se a operação foi bem-sucedida
- **data**: Dados retornados (pode ser objeto, array ou null)
- **message**: Mensagem descritiva da operação

### Para Listagens Paginadas:
```json
{
  "success": true,
  "data": {
    "items": [...],
    "totalItems": 100,
    "page": 1,
    "limit": 10,
    "totalPages": 10
  },
  "message": "Dados obtidos com sucesso"
}
```

---

## 📋 Exemplos de Uso

### Exemplo 1: Login e Acesso a Dados Protegidos

```bash
# 1. Fazer login
curl -X POST https://api.ludusgestao.com/api/autenticacao/entrar \
  -H "Content-Type: application/json" \
  -d '{"email": "usuario@exemplo.com", "senha": "senha123"}'

# 2. Usar o token retornado para acessar dados protegidos
curl -X GET https://api.ludusgestao.com/api/usuarios \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"
```

### Exemplo 2: Criar um Cliente

```bash
curl -X POST https://api.ludusgestao.com/api/clientes \
  -H "Authorization: Bearer SEU_TOKEN_AQUI" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@exemplo.com",
    "telefone": "(11) 99999-9999",
    "documento": "123.456.789-00"
  }'
```

### Exemplo 3: Buscar Endereço por CEP

```bash
curl -X GET https://api.ludusgestao.com/api/utilitarios/cep/12345-678
```

---

## 🔒 Segurança

- Todas as rotas (exceto autenticação e utilitários) requerem autenticação JWT
- Tokens expiram em 1 hora por padrão
- Use HTTPS em produção
- Valide sempre os dados de entrada
- Implemente rate limiting em produção

---

## 📞 Suporte

Para dúvidas ou problemas com a API, entre em contato com a equipe de desenvolvimento.
