🛒 OnlineShop – Backend (Work in Progress)
An Online Shop backend project built with .NET, currently under active development.

At this stage, the project contains only the Identity service, which provides authentication and authorization capabilities.

The project is designed as a microservice-based system and will be expanded gradually.

🚧 Project Status
⚠️ Work in Progress

✅ Implemented service: Identity Service
✅ JWT Authentication
✅ OTP-based login flow
✅ Redis for OTP storage
✅ SQL Server + EF Core
✅ Clean Architecture
✅ RabbitMQ
✅ MassTransit (Event Publishing)
✅ CAP (Transactional Messaging / Outbox)
🚧 Notification Service (planned)
🚧 Business services (Order, Catalog, Payment)
🔐 Implemented Service
Identity / Authentication Service
Responsibilities:

User authentication using phone number + OTP
OTP generation and validation
OTP storage using Redis
JWT access token generation
Publishing integration events
Clean Architecture layers:
Api
Application
Domain
Infrastructure
IoC
This service acts as the foundation of the system.

📡 Messaging & Communication
RabbitMQ is used as the message broker.

Both MassTransit and CAP are implemented, each serving a different architectural purpose.

🟦 MassTransit (Event-Driven Communication)
Purpose:

Event publishing for inter-service communication.

Usage:

Publishing integration events from Identity Service
Communication with future services such as Notification
Examples:

OtpSentEvent
UserRegisteredEvent
LoginSucceededEvent

content_copy
text

note_add
ویرایش با Canvas
Identity Service
   |
   |  Publish Event (MassTransit)
   v
RabbitMQ
   |
   v
Notification Service (planned)
🟨 CAP (Transactional Messaging / Outbox Pattern)
Purpose:

Ensure database changes and message publishing occur atomically.

Usage:

Reliable message publishing tied to database transactions
Prevent message loss in failure scenarios
Prepare the system for future business-critical workflows
CAP is currently implemented and configured, but its usage will become more significant

when business services (Order, Payment, Inventory) are introduced.

✅ Why Both Are Used?
Tool	Responsibility
MassTransit	Event-driven communication between services
CAP	Transactional messaging & consistency
This hybrid approach allows:

Flexible event-driven design
Safe and reliable message delivery for critical operations
🧱 Architecture (Current)

content_copy
text
Client
   ↓
Identity API
   ↓
JWT Token
Planned Architecture

content_copy
text
API Gateway
   ↓
Microservices
   ├── Identity Service ✅
   ├── Notification Service 🚧
   ├── Catalog Service 🚧
   ├── Order Service 🚧
   └── Payment Service 🚧
🧰 Technologies Used
Category	Technology
Language	C# (.NET)
Architecture	Clean Architecture
Authentication	JWT
Cache	Redis
Database	SQL Server
ORM	Entity Framework Core
Messaging	RabbitMQ
Event Bus	MassTransit
Transactional Messaging	CAP
Containerization	Docker
🚀 Running the Project
Prerequisites
.NET SDK 8+
Docker
SQL Server
Redis
Run RabbitMQ

content_copy
bash

note_add
ویرایش با Canvas
docker run -d \
  --name rabbitmq \
  -p 5672:5672 \
  -p 15672:15672 \
  rabbitmq:4-management
RabbitMQ Management UI:


content_copy
text
http://localhost:15672
username: guest
password: guest
Run Identity Service

content_copy
bash

note_add
ویرایش با Canvas
dotnet run
🎯 Purpose of This Project
Build a scalable authentication system
Practice Clean Architecture in a real-world project
Learn event-driven microservices
Explore MassTransit and CAP side by side
Prepare a strong foundation for future business services
🔮 Planned Improvements
🔲 Implement Notification Service
🔲 Add Order / Catalog / Payment services
🔲 Strengthen transactional event handling
🔲 Improve observability & logging
🔲 CI/CD pipeline
🔲 Docker Compose / Kubernetes
👩‍💻 Author
Zeynab Nadi

Backend Developer (.NET)

🔗 GitHub: ZeynabNadiDev

⭐ Notes
This project is not yet a full DDD implementation
Focus is on correct architectural foundations
Features are added incrementally with clarity over completeness
