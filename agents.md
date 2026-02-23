# agents.md

## Objetivo
Manter consistência enterprise no boilerplate.

## Regras
- Não colocar regra de negócio em controllers.
- UseCases em Application são obrigatórios para fluxos de negócio.
- Domain não depende de Application/Infrastructure/Api.
- DTOs apenas fora do Domain.
- Uma classe por arquivo.
- Logging sem dados sensíveis.
- Cobertura mínima alvo: 80%.
