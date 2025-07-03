# Project Overview - ASP.NET-Core-Microservices

This project is a comprehensive microservices-based architecture built using .NET, designed to be scalable, modular, and easily maintainable. The system leverages several modern technologies and patterns, including API Gateway, message-based communication, and multiple database types to support a robust e-commerce or enterprise solution.

![architecture-image](https://github.com/thanhnct/ASP.NET-Core-Microservices/blob/main/architecture.png?raw=true)

## Architecture Highlights

- **API Gateway (Ocelot):**  
  All client devices interact with the system through a centralized API Gateway powered by Ocelot, which routes requests to the appropriate backend services, ensuring security, scalability, and simplified client communication.

- **Microservices:**
  - **Identity Service:**  
    Manages authentication and authorization using SQL Server for persistence.
  - **Product API:**  
    Handles product-related operations, backed by MySQL.
  - **Customer API:**  
    Manages customer data stored in PostgreSQL.
  - **Basket API:**  
    Manages shopping baskets with Redis for fast, in-memory data access.
  - **Ordering API:**  
    Processes orders and stores order data in SQL Server.
  - **Inventory API:**  
    Manages inventory data using MongoDB.
  - **Background Job Service:**  
    Handles scheduled and background tasks, leveraging MongoDB for job state management.
  - **SMTP Service:**  
    Integrates with Google SMTP to send email notifications.

- **Event Bus (RabbitMQ):**  
  All services communicate asynchronously over RabbitMQ, enabling decoupled, event-driven interactions.

- **Health Monitoring:**  
  Integrated health checks and web status endpoints provide real-time service monitoring and diagnostics.

- **DevOps & Observability:**  
  The project is Docker-ready for easy deployment, and integrates with ELK (Elasticsearch, Logstash, Kibana) stack and Prometheus for centralized logging and monitoring.

## Key Technologies

- **.NET 8**
- **Ocelot (API Gateway)**
- **RabbitMQ (Event Bus)**
- **Docker**
- **SQL Server, MySQL, PostgreSQL, MongoDB, Redis**
- **Prometheus & ELK Stack for Monitoring**
- **Google SMTP for Email Notifications**

## Use Cases

This architecture is ideal for large-scale applications such as e-commerce platforms, enterprise resource planning (ERP) systems, or any solution requiring:

- Scalable microservices
- Multiple persistent storage technologies
- Asynchronous, event-driven communication
- Centralized API gateway
- Robust monitoring and health checks
