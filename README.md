# Property Repairs Microservices Project

## Overview
The Property Repairs Microservices Project is designed for PropertyRepairs Inc. to transition from a monolithic management system to a microservices architecture. This project aims to modernize the order management system of the company, enhancing efficiency, scalability, and flexibility.

## Architecture
The solution comprises two main microservices:
- `PublisherAPI`: Handles incoming repair requests from customers and publishes them to a RabbitMQ queue.
- `ConsumerAPI`: Listens to the RabbitMQ queue, processes the messages, and saves them to the database.

## Technologies Used
- .NET Core
- Entity Framework Core
- Microsoft SQL Server
- RabbitMQ
- Docker
