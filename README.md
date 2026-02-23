# ProjectName Enterprise Boilerplate

Boilerplate enterprise full-stack com foco em **.NET 10 + React TypeScript**, arquitetura **Hexagonal + Clean Architecture**, segurança robusta e prontidão para ambientes corporativos.

## Arquitetura
- Backend em camadas: Domain, Application, Infrastructure, Api, CrossCutting.
- Casos de uso centralizados em Application (controllers sem regra de negócio).
- DDD tático: entidades ricas no domínio, repositórios por porta.
- Multi-tenant por `TenantId`.

## Segurança
- JWT curto (15 min) + refresh token com rotação.
- Refresh token em cookie HttpOnly/Secure/SameSite Strict.
- HTTPS obrigatório + HSTS.
- Rate limiting global.
- Antiforgery (CSRF) habilitado.
- Validação com FluentValidation.
- Middleware de exceção global e CorrelationId.

## Stack Infra
- PostgreSQL (persistência).
- Redis (refresh token store / blacklist / cache).
- Serilog para logging estruturado.
- Health checks para PostgreSQL e Redis.
- Docker e docker-compose.
- CI GitHub Actions com objetivo de cobertura mínima de 80%.

## Estrutura
Ver diretórios em `backend/src`, `backend/tests` e `frontend/src` organizados por features e responsabilidades.

## Execução local
```bash
docker-compose up --build
```

## Próximos passos recomendados
1. Integrar Identity Provider real (Keycloak/Azure AD/Cognito).
2. Adicionar migrations e seeds de RBAC.
3. Completar CRUD Product (GetById, Update, Delete) com testes.
4. Habilitar gate de cobertura 80% com reportgenerator.
