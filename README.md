# Athena Union Library

Biblioteca compartilhada entre todos os microsserviços .NET da plataforma Athena Students Union.

## Visão geral

Centraliza contratos, utilitários e a implementação de CQRS que são consumidos por Identity, Institution, Notification e AthenaUnionAI. Qualquer alteração aqui impacta todos os serviços que referenciam a biblioteca como DLL.

## Conteúdo

### AthenaUnionLibrary

Utilitários transversais dos microsserviços:

| Módulo | Responsabilidade |
|---|---|
| `ApiResponse/` | `AthenaApiResponse<T>` — envelope padrão de resposta de todas as APIs |
| `Authorization/` | Políticas e middlewares de autenticação/autorização |
| `Requests/` | Helpers de HTTP client para comunicação entre serviços |
| `Validations/` | Validadores compartilhados |

### Mediator

Implementação customizada de CQRS — **não** é o MediatR de terceiros.

| Tipo | Uso |
|---|---|
| `IRequestMessage<TResponse>` | Contrato de comando ou query |
| `IMessageHandler<TRequest, TResponse>` | Handler que processa o request |
| `IMediator` | Despacha requests para o handler registrado |

## Regras críticas

- `AthenaApiResponse<T>` é o **único** envelope de resposta permitido nas APIs. Nunca retorne objetos crus nos controllers.
- O `Mediator` customizado **não** é intercambiável com MediatR — não misture os dois no mesmo serviço.
- Novos utilitários só entram aqui se forem realmente compartilhados por ≥ 2 serviços.
- Antes de alterar qualquer interface pública, verifique todos os consumidores.

## Repositórios que consomem esta biblioteca

| Serviço | Repositório |
|---|---|
| Identidade | [athena-identity](https://github.com/marceloarauj/athena-identity) |
| Escola / Turmas | [athena-institution-service](https://github.com/marceloarauj/athena-institution-service) |
| Notificações | [athena-union-notification-api](https://github.com/marceloarauj/athena-union-notification-api) |
| Backend de IA | [athena-union-ai](https://github.com/marceloarauj/athena-union-ai) |
