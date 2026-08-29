# E-Commerce API

A RESTful e-commerce backend built with **ASP.NET Core 8**. The API supports product browsing, shopping baskets, orders, user accounts, and JWT-based authentication.

> This solution follows an **Onion Architecture-inspired** structure: the domain model sits at the core, while infrastructure concerns such as SQL Server, Redis, and HTTP live in outer layers. The current dependency graph is documented transparently in the [Current dependency note](#current-dependency-note) section.

## Contents

- [Features](#features)
- [Architecture](#architecture)
- [Solution structure](#solution-structure)
- [Request flow](#request-flow)
- [Technology stack](#technology-stack)
- [Setup and run](#setup-and-run)
- [API endpoints](#api-endpoints)
- [Data and storage](#data-and-storage)
- [Error handling](#error-handling)
- [Current dependency note](#current-dependency-note)

## Features

- Product listing with filtering, sorting, searching, and pagination.
- Product brands and product types.
- Redis-backed shopping baskets with a 30-day lifetime.
- Order creation, order retrieval, and delivery methods.
- User registration, login, and JWT token generation.
- Retrieve and update the authenticated user's address.
- JWT protection for basket, order, and current-user endpoints.
- Automatic database migration and seed data initialization at startup.
- Consistent error responses through `ProblemDetails`.
- Swagger/OpenAPI support in the Development environment.

## Architecture

The solution uses the core idea of Onion Architecture: business policies belong near the center, while technology-specific details such as ASP.NET Core, SQL Server, and Redis remain at the edges and can be changed with minimal effect on business logic.

```text
┌──────────────────────────────────────────────────────────────────┐
│                         ECommerce.API                             │
│  Program • Middleware • Composition Root • Swagger • JWT pipeline │
├──────────────────────────────────────────────────────────────────┤
│                  ECommerce.ApplicaionLayer                        │
│                       HTTP Controllers                             │
├──────────────────────────────────────────────────────────────────┤
│             Services + ServicesAbstraction + Shared                │
│  Use cases • Service contracts • DTOs • Mapping • Specifications  │
├──────────────────────────────────────────────────────────────────┤
│                       ECommerce.Domain                             │
│ Entities • Domain models • Exceptions • Repository contracts       │
├──────────────────────────────────────────────────────────────────┤
│                   ECommerce.Inferastructure                        │
│ EF Core/SQL Server • Redis • Identity • Repositories • Seeding    │
└──────────────────────────────────────────────────────────────────┘
```

### 1. Domain layer — `ECommerce.Domain`

The domain is the center of the application. It contains the business model and the abstractions required by business logic, without HTTP, SQL Server, or Redis implementation details in the entities.

- **Entities:** `Product`, `ProductBrand`, `ProductType`, and order entities such as `Order`, `OrderItem`, and `DeliveryMethod`.
- **Basket model:** `CustomerBasket` and `BasketItem`.
- **Identity model:** `ApplicationUser` and `Address`.
- **Abstractions:** `IGenaricRepository`, `IUnitOfWork`, `IBasketRepository`, `Ispacefications`, and data-seeding contracts.
- **Domain exceptions:** including `ProductNotFoundException`, `BasketNotFoundException`, and `OrderNotFoundException`.

### 2. Use-case layer — `Services`

This project implements its business use cases in `Services`. Services work through domain abstractions rather than interacting directly with `DbContext` or Redis.

- `ProductsServices`: retrieves products, types, and brands; uses Specifications for filtering and pagination.
- `BasketService`: creates, updates, retrieves, and deletes shopping baskets.
- `OrderService`: converts basket items into an order, validates products and delivery methods, and calculates the subtotal.
- `AuthenticationService`: registers and authenticates users, checks email availability, creates JWTs, and manages user addresses.
- `PaymentService`: registered payment service, prepared for Stripe integration.
- `ServicesManger`: one service access point exposed through `IserviceManger`; services are created lazily when first requested.
- `mapping`: AutoMapper profiles that map entities to DTOs and back.
- `Specfiactions`: query specifications such as `ProductWithTypeAndBrandSpesfication` and `OrderSpecfication`.

### 3. Service contracts and transport models

#### `ServicesAbstraction`

Contains use-case interfaces, including `IProductServices`, `IBasketService`, `IOrderService`, `IAuthenticationService`, `IPaymentService`, and `IserviceManger`.

#### `Shared`

Contains cross-layer transport models without business logic:

- Request DTOs such as `OrderRequest`, `UserRegisterRequest`, and `UserloginRequest`.
- Response DTOs such as `ProductResponse`, `OrderResponse`, `CustomerBasketResponse`, and `PaginateResult`.
- Filtering and sorting models: `ProductFiltiration` and `ProductSortingOptions`.
- Shared error models.

### 4. HTTP presentation layer — `ECommerce.ApplicaionLayer`

This project contains controllers only. A controller receives an HTTP request, delegates work to `IserviceManger`, and returns an HTTP response. It should not contain business rules or direct database queries.

| Controller | Responsibility |
| --- | --- |
| `ProductController` | Products, brands, and types |
| `BasketController` | Protected shopping basket operations |
| `OrderController` | Order creation/retrieval and delivery methods |
| `AuthenticationController` | Registration, login, current user, and address operations |

### 5. Infrastructure layer — `ECommerce.Inferastructure`

Contains the technology-specific implementations of the application contracts:

- `AppDbContext`: the SQL Server store for catalog, orders, and delivery methods.
- `IdentityStoreDbContext`: a separate SQL Server database context for ASP.NET Core Identity.
- `UnitOfWork` and `Genaricrepo`: Repository and Unit of Work implementations using EF Core.
- `BasketRepository`: the Redis implementation of the basket repository.
- `SpecficationEvaluator`: converts Specifications into EF Core queries.
- `Configurations`: Fluent API entity configuration classes.
- `DataSeed` and `SeedIdentityData`: migrate pending migrations, load initial JSON data, and seed identity data.
- `DependacyInjection`: registers SQL Server, Redis, Identity, and repositories in the DI container.

### 6. Host layer — `ECommerce.API`

This is the application entry point and composition root. `Program.cs`:

1. Registers controllers and configures JSON enums as strings.
2. Configures `ProblemDetails` and `GlobalExceptionHanlder`.
3. Registers Infrastructure and Services in the dependency injection container.
4. Configures SQL Server, Redis, and ASP.NET Core Identity.
5. Configures JWT Bearer authentication and authorization.
6. Enables Swagger in Development, serves static product images, and runs startup seeding.
7. Maps controllers to HTTP routes.

## Solution structure

```text
ECommerce/
├── ECommerce.API/                 # Host project and application entry point
│   ├── MiddleWare/                 # GlobalExceptionHanlder
│   └── wwwroot/images/products/    # Static product images
├── ECommerce.ApplicaionLayer/      # HTTP controllers
├── ECommerce.Domain/               # Entities, abstractions, exceptions
├── ECommerce.Inferastructure/      # EF Core, Redis, Identity, repositories, migrations
├── Services/                       # Use cases, AutoMapper, and Specifications
├── ServicesAbstraction/            # Service interfaces
├── Shared/                         # Request and response DTOs
└── ECommerce.slnx                  # Solution file
```

> Folder and project names above intentionally match the current repository names, including spellings such as `ApplicaionLayer` and `Inferastructure`.

## Request flow

Example: creating an order.

```text
Client
  │ POST /api/orders + JWT
  ▼
OrderController
  │ Reads email from claims and passes OrderRequest
  ▼
OrderService
  │ Reads basket through IBasketRepository (Redis)
  │ Validates products and delivery method through IUnitOfWork
  │ Creates Order and OrderItems, then calculates the subtotal
  ▼
UnitOfWork / Generic Repository
  ▼
AppDbContext → SQL Server
  ▼
OrderResponse → Client
```

If an exception occurs in any layer, it reaches `GlobalExceptionHanlder` in the API project, which returns an appropriate `ProblemDetails` response (400, 401, 404, or 500).

## Technology stack

| Technology | Purpose |
| --- | --- |
| .NET 8 / ASP.NET Core | REST API framework |
| Entity Framework Core 8 | SQL Server access and migrations |
| SQL Server | Catalog, order, and identity persistence |
| StackExchange.Redis | Shopping-basket persistence |
| ASP.NET Core Identity | Users, passwords, and roles |
| JWT Bearer | Authentication and route protection |
| AutoMapper | Entity/DTO mapping |
| Swagger / OpenAPI | API exploration and testing in Development |
| Stripe.net | Payment integration in the Services layer |

## Setup and run

### Prerequisites

- .NET SDK 8.0 or a compatible later SDK.
- A running SQL Server instance.
- A running Redis instance (default port: `6379`).

### 1. Configure connections and secrets

Update `ECommerce.API/appsettings.json` or, preferably, use User Secrets/environment variables for:

- `ConnectionStrings:DefaultConnection`: catalog and orders database.
- `ConnectionStrings:IdentityConnection`: Identity database.
- `ConnectionStrings:RedisConnection`: Redis server.
- `JWT:Issuer`, `JWT:Audience`, `JWT:Key`, and `JWT:TokenExpirationInMinutes`.
- `StrpeSettings:SecretKey`: Stripe secret key when payment processing is used.

> Do not commit real JWT or Stripe secrets to a public repository. Keep production values in User Secrets or environment variables, and use placeholders in committed configuration files.

Example User Secrets commands from the repository root:

```powershell
dotnet user-secrets set "JWT:Key" "replace-with-a-long-random-secret" --project .\ECommerce.API
dotnet user-secrets set "StrpeSettings:SecretKey" "replace-with-stripe-secret" --project .\ECommerce.API
```

### 2. Restore and build

```powershell
dotnet restore .\ECommerce.slnx
dotnet build .\ECommerce.slnx
```

### 3. Run the API

```powershell
dotnet run --project .\ECommerce.API
```

In the `Development` environment, Swagger UI is available at the URL printed by the application. Pending migrations are applied and the initial catalog, brand, type, delivery-method, and identity data are seeded during startup.

### Manage migrations manually

```powershell
# Add a migration for the catalog/orders database
dotnet ef migrations add <MigrationName> --project .\ECommerce.Inferastructure --startup-project .\ECommerce.API --context AppDbContext

# Apply catalog/orders migrations
dotnet ef database update --project .\ECommerce.Inferastructure --startup-project .\ECommerce.API --context AppDbContext

# Add a migration for the Identity database
dotnet ef migrations add <MigrationName> --project .\ECommerce.Inferastructure --startup-project .\ECommerce.API --context IdentityStoreDbContext --output-dir Identity\Migrations
```

## API endpoints

The following routes are derived from the current controllers. Endpoints marked with 🔒 require this header:

```http
Authorization: Bearer <access-token>
```

### Products — `/api/Products`

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/Products` | List products with filtering, sorting, searching, and pagination query parameters |
| `GET` | `/api/Products/{id}` | Get one product by ID |
| `GET` | `/api/Products/Brands` | Get all product brands |
| `GET` | `/api/Products/Types` | Get all product types |

### Users — `/api/users`

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/users/register` | Register a user and return user details with a JWT |
| `POST` | `/api/users/login` | Authenticate a user and return a JWT |
| `GET` | `/api/users/EmailExist?Email={email}` | Check whether an email exists |
| `GET` | `/api/users` 🔒 | Get the current user |
| `GET` | `/api/users/Address` 🔒 | Get the current user's address |
| `PUT` | `/api/users/Address` 🔒 | Create or update the current user's address |

### Basket — `/api/Basket`

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/Basket/{id}` 🔒 | Get a basket by ID |
| `POST` | `/api/Basket` 🔒 | Create or update a basket |
| `DELETE` | `/api/Basket/{id}` 🔒 | Delete a basket |

### Orders — `/api/orders`

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/orders` 🔒 | Create an order from a basket, delivery method, and address |
| `GET` | `/api/orders` 🔒 | Get orders belonging to the current user |
| `GET` | `/api/orders/{id}` 🔒 | Get an order by `Guid` |
| `GET` | `/api/orders/deliveryMethod` 🔒 | Get available delivery methods |

## Data and storage

| Data | Storage | Access method |
| --- | --- | --- |
| Products, types, brands, orders, and delivery methods | SQL Server via `AppDbContext` | EF Core + `UnitOfWork` / `Genaricrepo` |
| Users, passwords, roles, and addresses | SQL Server via `IdentityStoreDbContext` | ASP.NET Core Identity |
| Shopping baskets | Redis | `BasketRepository` |
| Initial catalog data | JSON files in `ECommerce.Inferastructure/DataSeed/JsonData` | `DataSeed` at startup |

## Error handling

`GlobalExceptionHanlder` converts exceptions to `ProblemDetails` responses and attaches the request path, method, and `requestId`.

| Error type | HTTP status |
| --- | --- |
| `ValidationException` | `400 Bad Request` |
| `NotFoundException` | `404 Not Found` |
| `AuthorizetionException` | `401 Unauthorized` |
| Unexpected exception | `500 Internal Server Error` |

## Current dependency note

The solution expresses Onion Architecture well through separated responsibilities, but it is not yet a fully strict implementation of the inward-only dependency rule. This is important to state clearly:

- `ECommerce.ApplicaionLayer` currently references `Services` directly because controllers use `IserviceManger`.
- `ECommerce.Inferastructure` currently references `ECommerce.ApplicaionLayer`; in a strict Onion Architecture implementation, an infrastructure project should not depend on the HTTP presentation layer.
- `ECommerce.Domain` references an Identity/EF package, whereas a strictly isolated domain core would avoid such infrastructure-related dependencies.

These details do not prevent the application from running and do not remove the practical separation of responsibilities. They are, however, useful future refactoring targets if the goal is a stricter Onion/Clean Architecture.

1. Move controllers to a dedicated Presentation/API project and let them depend on application interfaces only.
2. Keep use-case contracts in an Application/Core project, with `Services` providing their implementations.
3. Remove the Infrastructure-to-Application dependency and let API remain the only composition root that connects implementations with abstractions.
4. Keep the Domain project independent of EF Core and Identity wherever possible; move persistence and identity-specific models outward.

## Development guidelines

- Use `CancellationToken` for new I/O operations, consistent with existing services.
- Implement complex queries as Specifications rather than adding query logic to controllers.
- Add a DTO in `Shared` before exposing a new domain entity through the API.
- Register new services and repositories in the appropriate dependency-injection extension methods.
- When adding a new business exception, derive from `NotFoundException` where appropriate or handle the exception in `GlobalExceptionHanlder` to keep API errors consistent.

