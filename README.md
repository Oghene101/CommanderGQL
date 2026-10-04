# CommanderGQL

A GraphQL API for cataloguing command-line snippets by platform (e.g. `dotnet build` for .NET, `docker ps` for Docker). Built with ASP.NET Core, [Hot Chocolate](https://chillicream.com/docs/hotchocolate), and Entity Framework Core on PostgreSQL.

## Features

- **Queries** for platforms and commands with cursor paging, filtering, sorting, and projections
- **Mutations** to add platforms and commands
- **Subscriptions** over WebSockets when a platform is added (in-memory pub/sub)
- EF Core migrations with snake_case table and column names

## Tech stack

| Component | Version |
| --- | --- |
| .NET | 10.0 |
| Hot Chocolate (`HotChocolate.AspNetCore`, `HotChocolate.Data.EntityFramework`) | 16.5.0 |
| EF Core + Npgsql (`Npgsql.EntityFrameworkCore.PostgreSQL`) | 10.0.x |
| `EFCore.NamingConventions` | 10.0.1 |

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A running PostgreSQL instance
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### 1. Configure the database connection

The `ConnectionStrings:Database` value in `CommanderGQL/appsettings.json` is empty by default. Supply it through an environment variable so credentials stay out of source control:

```bash
export ConnectionStrings__Database="Host=localhost;Port=5432;Database=commander_gql;Username=postgres;Password=<your-password>"
```

### 2. Apply migrations

```bash
dotnet ef database update --project CommanderGQL
```

### 3. Run

```bash
dotnet run --project CommanderGQL
```

The GraphQL endpoint and the Nitro IDE are served at <http://localhost:5126/graphql>. Use `--launch-profile https` to also listen on <https://localhost:7043>.

## Usage

### Add a platform

```graphql
mutation {
  addPlatform(input: { name: "Docker" }) {
    platform {
      id
      name
    }
  }
}
```

### Add a command

```graphql
mutation {
  addCommand(
    input: { howTo: "List running containers", commandLine: "docker ps", platformId: 1 }
  ) {
    command {
      id
      howTo
      commandLine
      platform {
        name
      }
    }
  }
}
```

### Query with paging, filtering, and sorting

```graphql
query {
  platforms(first: 5, where: { name: { contains: "Do" } }, order: [{ name: ASC }]) {
    totalCount
    pageInfo {
      hasNextPage
      endCursor
    }
    nodes {
      id
      name
      commands {
        howTo
        commandLine
      }
    }
  }
}
```

`commands` supports the same arguments. Page size defaults to 10, which is also the maximum.

### Subscribe to new platforms

```graphql
subscription {
  onPlatformAdded {
    id
    name
  }
}
```

Run this in one Nitro tab, then execute `addPlatform` in another to see the event arrive.

## Data model

| Entity | Fields | Notes |
| --- | --- | --- |
| `Platform` | `id`, `name`, `commands` | `LicenseKey` is stored in the database but hidden from the GraphQL schema |
| `Command` | `id`, `howTo`, `commandLine`, `platformId`, `platform` | Belongs to one platform; deleted with it (cascade) |

## Project structure

```
CommanderGQL/
├── Program.cs              # Service registration and GraphQL endpoint
├── Query.cs                # Root types, extended per feature
├── Mutation.cs
├── Subscription.cs
├── Extensions/Type.cs      # AddTypes() — registers all GraphQL types and extensions
├── Platforms/              # Entity, object type, queries, mutations, subscriptions
├── Commands/               # Entity, object type, queries, mutations
└── Data/
    ├── AppDbContext.cs
    └── Migrations/
```

Each feature folder extends the root `Query`, `Mutation`, and `Subscription` types with `[ExtendObjectType]`. To add a new feature, create its folder and register the new extensions in `Extensions/Type.cs`.

## Adding a migration

```bash
dotnet ef migrations add <name> --project CommanderGQL --output-dir Data/Migrations
```
