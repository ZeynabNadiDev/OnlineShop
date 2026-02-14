# 🛒 OnlineShop – Backend (Work in Progress)

An **Online Shop backend project** built with **.NET**, currently under active development.

At this stage, the project contains **only the Identity service**, which provides authentication and authorization capabilities.
The project is designed as a **microservice-based system** and will be expanded gradually.

---

## 🚧 Project Status

> ⚠️ **Work in Progress**

| Feature | Status |
|------|------|
| Identity Service | ✅ Implemented |
| JWT Authentication | ✅ |
| OTP-based Login | ✅ |
| Redis (OTP Storage) | ✅ |
| SQL Server + EF Core | ✅ |
| Clean Architecture | ✅ |
| RabbitMQ | ✅ |
| MassTransit | ✅ |
| CAP (Outbox / Transactional Messaging) | ✅ |
| Notification Service | 🚧 Planned |
| Business Services (Order, Catalog, Payment) | 🚧 Planned |

---

## 🔐 Identity Service Responsibilities

- User authentication using **phone number + OTP**
- OTP generation and validation
- OTP storage using **Redis**
- JWT access token generation
- Publishing integration events

### Architecture Layers

- Api
- Application
- Domain
- Infrastructure
- IoC

This service acts as the **foundation of the system**.

---

## 📡 Messaging & Communication

RabbitMQ is used as the message broker.
Both **MassTransit** and **CAP** are implemented, each serving a **different architectural purpose**.

### 🟦 MassTransit – Event‑Driven Communication

**Purpose:**
Event publishing for inter‑service communication.

**Usage:**
- Publishing integration events from Identity Service
- Communication with future services such as Notification

**Example Events:**
- `OtpSentEvent`
- `UserRegisteredEvent`
- `LoginSucceededEvent`

```
Identity Service
   |
   |  Publish Event (MassTransit)
   v
RabbitMQ
   |
   v
Notification Service (planned)
```

---

### 🟨 CAP – Transactional Messaging (Outbox Pattern)

**Purpose:**
Ensure **database changes and message publishing** occur atomically.

**Usage:**
- Reliable message publishing tied to database transactions
- Prevent message loss in failure scenarios
- Prepare the system for future business‑critical workflows

> CAP is already implemented and configured.

---

### ✅ Why Both Are Used?

| Tool | Responsibility |
|----|----|
| MassTransit | Event‑driven inter‑service communication |
| CAP | Transactional consistency & reliable messaging |

---

## 🧰 Technologies Used

| Category | Technology |
|------|------|
| Language | C# (.NET) |
| Architecture | Clean Architecture |
| Authentication | JWT |
| Cache | Redis |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Messaging | RabbitMQ |
| Event Bus | MassTransit |
| Transactional Messaging | CAP |
| Containerization | Docker |

---

## 🚀 Running the Project

### Prerequisites

- .NET SDK 8+
- Docker
- SQL Server
- Redis

### Run RabbitMQ

```bash
docker run -d   --name rabbitmq   -p 5672:5672   -p 15672:15672   rabbitmq:4-management
```

RabbitMQ Management UI:

```
http://localhost:15672
username: guest
password: guest
```

### Run Identity Service

```bash
dotnet run
```

---

## 👩‍💻 Author

**Zeynab Nadi**  
Backend Developer (.NET)

GitHub: https://github.com/ZeynabNadiDev

---

## ⭐ Notes

- This project is **not yet a full DDD implementation**
- Focus is on **correct architectural foundations**
- Features are added incrementally
