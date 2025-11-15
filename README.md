# GuessGame App

A simple number guessing game with a .NET 8 Web API backend and an Angular 17 frontend. Includes JWT authentication, FluentValidation, a consistent `ApiResult<T>` response envelope, Swagger documentation, and Docker Compose for local orchestration (API, PostgreSQL, and web).

## Tech Stack

- .NET 8 Minimal API (`GuessGame.WebAPI`)
- Angular 17 (`Web`)
- PostgreSQL 16
- MediatR, Entity Framework Core, ASP.NET Identity
- JWT Auth, FluentValidation, Swagger (Swashbuckle)
- Docker Compose

## Project Structure

```
GuessGameApp.sln
├─ GuessGame.Domain
├─ GuessGame.Application
├─ GuessGame.Infrastructure
├─ GuessGame.WebAPI
│  ├─ Endpoints
│  ├─ Validators
│  ├─ Results (ApiResult)
│  └─ Program.cs
├─ Web
│  ├─ src/app/services
│  ├─ src/app/components
│  ├─ src/app/shared/types.ts
│  └─ src/environments/environment.ts
├─ docker-compose.yml
└─ .gitignore
```

## Prerequisites

- .NET SDK 8.0+
- Node.js 18+
- npm 9+
- Docker Desktop (optional, for Compose)

## Configuration

- API settings: `GuessGame.WebAPI/appsettings.json` (connection string and JWT)
- Angular API base URL: `Web/src/environments/environment.ts`

```
export const environment = { apiBaseUrl: 'http://localhost:5048' };
```

- Do not commit secrets. For Docker Compose, env vars are provided via `docker-compose.yml`.

## Local Development

### Run API

```
dotnet build GuessGame.WebAPI/GuessGame.WebAPI.csproj
dotnet run --project GuessGame.WebAPI/GuessGame.WebAPI.csproj
```

- API listens on `http://localhost:5048`
- Swagger UI: `http://localhost:5048/swagger/index.html`

### Run Web

```
cd Web
npm install
npm start
```

- Angular dev server listens on `http://localhost:4200`
- Environment expects API at `http://localhost:5048`

### Swagger

- Enabled in Development: `http://localhost:5048/swagger/index.html`
- Click “Authorize” and enter `Bearer <JWT>` to access protected endpoints

## Docker Compose

Brings up PostgreSQL, API, and web (Nginx).

```
docker compose up --build
```

- API: `http://localhost:5048`
- Web: `http://localhost:4200`
- Database: `postgres://postgres:postgres@localhost:5432/guessgame`

## API Overview

Responses use the envelope:

```
{
  "success": boolean,
  "data": T,
  "error": string | null
}
```

### Auth

- POST `/api/auth/register`
  - Body: `{ "userName": string, "password": string }`
  - Returns: `ApiResultNoData`

- POST `/api/auth/login`
  - Body: `{ "userName": string, "password": string }`
  - Returns: `ApiResult<string>` where `data` is the JWT

- POST `/api/auth/logout`
  - Returns: `ApiResultNoData`

### Game

- POST `/api/game/start` (auth required)
  - Returns: `ApiResult<{ sessionId: string }>`

- POST `/api/game/guess` (auth required)
  - Body: `{ "guess": number }`
  - Returns: `ApiResult<{ result: "higher" | "lower" | "correct"; guessCount: number; bestGuessCount?: number }>`

### Users

- GET `/api/users/me` (auth required)
  - Returns: `ApiResult<{ bestGuessCount: number | null }>`

- GET `/api/users/leaderboard?top=N`
  - Returns: `ApiResult<Array<{ userName: string; bestGuessCount: number }>>`

## Frontend Notes

- Shared API models live in `Web/src/app/shared/types.ts`
- `token.interceptor.ts` attaches `Authorization: Bearer <token>` for protected endpoints
- Auth token is stored in `localStorage` under `auth_token`

## Development Tips

- Initialize DB locally: the API will create required tables automatically on first run
- Validate inputs: FluentValidation is wired for DTOs used in endpoints
- CORS policy allows `http://localhost:4200`

## Scripts

- Angular: `npm start`, `npm run build`
- .NET API: `dotnet build`, `dotnet run`
- Docker: `docker compose up --build`

## License

MIT (or your preferred license). Update as needed.
