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
