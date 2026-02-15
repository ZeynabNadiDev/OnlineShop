# 🛒 OnlineShop – Backend (Work in Progress)

An online shop backend built with **.NET**, designed using **Clean Architecture** and **microservice principles**.  
The project is under active development and is being expanded incrementally.

At the current stage, only the **Identity (Authentication) Service** is implemented.  
Other services will be added over time.

---

## 🚧 Project Status

**Work in Progress**

### ✅ Implemented
- Identity / Authentication Service
- JWT-based authentication
- Phone number + OTP login flow
- Redis for OTP storage
- SQL Server + Entity Framework Core
- Clean Architecture
- RabbitMQ
- MassTransit (event publishing)
- CAP (transactional messaging / outbox pattern)

### 🚧 Planned
- Notification Service
- Order Service
- Catalog Service
- Payment Service

---

## 🔐 Identity Service

The Identity Service is responsible for authentication and authorization and acts as the foundation of the system.

### Responsibilities
- User authentication using phone number + OTP
- OTP generation and validation
- OTP storage using Redis
- JWT access token generation
- Publishing integration events for other services

### Architecture Layers
- API  
- Application  
- Domain  
- Infrastructure  
- IoC  

---

## 📡 Messaging & Communication

RabbitMQ is used as the message broker.

Both **MassTransit** and **CAP** are implemented, each serving a different architectural purpose.

---

### MassTransit – Event‑Driven Communication

**Purpose**
- Inter-service communication using integration events

**Usage**
- Publishing events from the Identity Service
- Communication with future services such as Notification

**Example Events**
- OtpSentEvent
- UserRegisteredEvent
- LoginSucceededEvent

**Flow**
- Identity Service publishes integration events using MassTransit
- Events are delivered via RabbitMQ
- Other services (e.g. Notification) will subscribe and react accordingly

---

### CAP – Transactional Messaging (Outbox Pattern)

**Purpose**
- Ensures consistency between database changes and message publishing

**Usage**
- Used when an event must be published atomically with a database transaction
- Prevents message loss in failure scenarios

**Flow**
- Business operation is executed
- Database transaction is committed
- Event is stored and published reliably via CAP

---

## 🧱 Technologies Used

- .NET / ASP.NET Core Web API
- C#
- Clean Architecture
- Microservices Architecture
- SQL Server
- Entity Framework Core
- Redis
- RabbitMQ
- MassTransit
- CAP
- JWT Authentication
- Docker (planned)
- CI/CD (planned)

---

## ▶️ Running the Project

> ⚠️ The project is under active development.  
> Currently, only the **Identity Service** is available.

### Prerequisites
- .NET SDK
- SQL Server
- Redis
- RabbitMQ

### Steps
1. Clone the repository
2. Configure connection strings in `appsettings.json`
3. Run database migrations
4. Start required infrastructure services (SQL Server, Redis, RabbitMQ)
5. Run the Identity Service

---

## 🛣️ Roadmap

- Add Notification Service
- Add Order, Catalog, and Payment services
- Introduce gRPC for internal communication
- Add Docker Compose
- Implement CI/CD pipeline
- Improve observability (logging, tracing, metrics)

---

## 👤 Author

**Zeynab Nadi**  
Backend Developer (.NET)
