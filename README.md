# EShop Microservices

This project is a study and hands-on implementation of a modern microservices architecture using ASP.NET Core and Docker, developed through a Udemy course and expanded with additional improvements and customizations.

The goal of this project is to deepen knowledge in backend architecture, containerization, distributed systems, and modern software development practices.

## Technologies & Concepts

* ASP.NET Core C#
* Docker & Docker Compose
* PostgreSQL
* Marten
* Redis
* gRPC
* RabbitMQ
* Clean Architecture
* CQRS
* MediatR
* Behaviors (Validation & Logging)
* Entity Framework Core
* Microservices Architecture

## Project Structure

The solution is being organized using a microservices approach, where each service has its own responsibility and database.

Current services:

* Catalog Microservice
* Basket Microservice (in progress)

## BuildingBlocks

One of the most interesting parts of the project is the creation of a shared `BuildingBlocks` project responsible for centralizing common concerns such as:

* CQRS communication
* MediatR Behaviors
* Validation pipeline
* Logging pipeline
* Exception handling

This approach helps keep the microservices cleaner, more organized, reusable, and easier to maintain.

## Architecture Goals

This project focuses on:

* Modernizing backend architecture
* Building scalable services
* Applying clean code principles
* Improving maintainability and separation of concerns
* Understanding container communication and distributed systems

## Running with Docker

The project uses Docker Compose to orchestrate:

* ASP.NET Core APIs
* PostgreSQL containers
* Future integrations with Redis and RabbitMQ

## Learning Journey

This repository is part of my continuous learning journey in:

* Microservices
* Cloud-native applications
* Modern backend development
* Distributed architectures

New features and services are continuously being added as the project evolves.
