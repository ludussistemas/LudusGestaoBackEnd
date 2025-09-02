# BaseCrudController - Guia de Uso

## Visão Geral

O `BaseCrudController` foi refatorado para seguir os princípios SOLID, oferecendo múltiplas sobrecargas de construtores e interfaces para diferentes cenários de uso.

## Princípios SOLID Implementados

### 1. Single Responsibility Principle (SRP)
- **IValidationService**: Responsável apenas pela validação
- **ILoggingService**: Responsável apenas pelo logging
- **IAuthorizationService**: Responsável apenas pela autorização
- **IBaseCrudController**: Define apenas os contratos dos métodos CRUD

### 2. Open/Closed Principle (OCP)
- O controller base está aberto para extensão através de herança
- Fechado para modificação através de interfaces

### 3. Liskov Substitution Principle (LSP)
- Implementações padrão podem ser substituídas por implementações customizadas
- Todas as implementações seguem os contratos das interfaces

### 4. Interface Segregation Principle (ISP)
- Interfaces específicas para cada responsabilidade
- Clientes não dependem de interfaces que não usam

### 5. Dependency Inversion Principle (DIP)
- O controller depende de abstrações (interfaces)
- Não depende de implementações concretas

## Estrutura de Arquivos

```
LudusGestao.Core/
├── Controllers/
│   ├── BaseCrudController.cs          # Controller base com todas as funcionalidades
│   ├── SimpleBaseCrudController.cs    # Versão simplificada com implementações padrão
│   └── README.md                      # Esta documentação
├── Interfaces/
│   └── Controllers/
│       ├── IBaseCrudController.cs     # Interface principal do controller
│       ├── IValidationService.cs      # Interface para validação
│       ├── ILoggingService.cs         # Interface para logging
│       └── IAuthorizationService.cs   # Interface para autorização
└── Services/
    ├── DefaultValidationService.cs    # Implementação padrão de validação
    ├── DefaultLoggingService.cs       # Implementação padrão de logging
    ├── DefaultAuthorizationService.cs # Implementação padrão de autorização
    ├── CustomValidationService.cs     # Exemplo de validação customizada
    └── CustomAuthorizationService.cs  # Exemplo de autorização customizada
```

## Como Usar

### 1. Uso Simples (Recomendado para a maioria dos casos)

```csharp
[ApiController]
[Route("api/clientes")]
[Authorize]
public class ClientesController : SimpleBaseCrudController<IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>, ClienteDTO, CreateClienteDTO, UpdateClienteDTO>
{
    public ClientesController(IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO> service) : base(service) { }
}
```

### 2. Uso com Validação Customizada

```csharp
[ApiController]
[Route("api/clientes")]
[Authorize]
public class ClientesController : BaseCrudController<IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>, ClienteDTO, CreateClienteDTO, UpdateClienteDTO>
{
    public ClientesController(IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO> service) 
        : base(service, new CustomValidationService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>()) { }
}
```

### 3. Uso com Todas as Funcionalidades Customizadas

```csharp
[ApiController]
[Route("api/clientes")]
[Authorize]
public class ClientesController : BaseCrudController<IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>, ClienteDTO, CreateClienteDTO, UpdateClienteDTO>
{
    public ClientesController(IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO> service) 
        : base(
            service, 
            new CustomValidationService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>(),
            new CustomLoggingService(),
            new CustomAuthorizationService()) { }
}
```

### 4. Uso com Apenas Logging Customizado

```csharp
[ApiController]
[Route("api/clientes")]
[Authorize]
public class ClientesController : BaseCrudController<IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>, ClienteDTO, CreateClienteDTO, UpdateClienteDTO>
{
    public ClientesController(IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO> service) 
        : base(service, null, new CustomLoggingService()) { }
}
```

## Sobrecargas de Construtor Disponíveis

1. `BaseCrudController(TService service)` - Apenas o service
2. `BaseCrudController(TService service, IValidationService validationService)` - Service + validação
3. `BaseCrudController(TService service, IValidationService validationService, ILoggingService loggingService)` - Service + validação + logging
4. `BaseCrudController(TService service, IValidationService validationService, ILoggingService loggingService, IAuthorizationService authorizationService)` - Todas as dependências

## Funcionalidades Incluídas

### Validação
- Validação automática de IDs nulos ou vazios
- Validação de DTOs nulos
- Suporte a validações customizadas

### Logging
- Log de todas as operações CRUD
- Log de erros com stack trace
- Log de dados de entrada e saída
- Implementação padrão usando Console.WriteLine

### Autorização
- Verificação de permissões antes de cada operação
- Resposta personalizada para acesso negado
- Implementação padrão permite tudo

### Tratamento de Erros
- Try-catch em todos os métodos
- Log de erros automático
- Respostas de erro padronizadas

## Migração dos Controllers Existentes

Para migrar controllers existentes, basta alterar a herança:

**Antes:**
```csharp
public class ClientesController : BaseCrudController<...>
```

**Depois:**
```csharp
public class ClientesController : SimpleBaseCrudController<...>
```

Ou, se quiser manter o nome original:
```csharp
public class ClientesController : BaseCrudController<...>
```

Ambos funcionarão da mesma forma, mas o `SimpleBaseCrudController` oferece uma API mais limpa para a maioria dos casos de uso.

## Benefícios

1. **Flexibilidade**: Múltiplas sobrecargas para diferentes necessidades
2. **Testabilidade**: Interfaces facilitam o mock para testes
3. **Manutenibilidade**: Código organizado por responsabilidades
4. **Extensibilidade**: Fácil adição de novas funcionalidades
5. **Reutilização**: Implementações padrão podem ser reutilizadas
6. **Consistência**: Comportamento padronizado em todos os controllers
