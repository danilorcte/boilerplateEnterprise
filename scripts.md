# Scripts úteis

## Backend
- `dotnet restore backend/src/ProjectName.Api/ProjectName.Api.csproj`
- `dotnet test backend/tests/UnitTests/UnitTests.csproj --collect:"XPlat Code Coverage"`
- `dotnet test backend/tests/IntegrationTests/IntegrationTests.csproj --collect:"XPlat Code Coverage"`

## Frontend
- `cd frontend && npm install`
- `cd frontend && npm run dev`
- `cd frontend && npm run build`

## Containers
- `docker-compose up --build`
- `docker-compose down -v`
