# Example.MassTransit Solution

This project is a demonstration of using MassTransit in a .NET 8 solution. MassTransit is a free, open-source distributed application framework for .NET that simplifies the development of message-based applications.

## Solution Structure

The solution is composed of the following projects:

- `Example.MassTransit.Producer`: A console application that sends messages to a RabbitMQ exchange using MassTransit;
- `Example.MassTransit.Consumer`: A console application that receives messages from a RabbitMQ queue using MassTransit;
- `Example.MassTransit.Contracts`: A class library that contains the message contracts shared between the producer and consumer projects.

## Dependencies

This project relies on the following dependencies:

- `Serilog`: A logging library for .NET applications.
- `MassTransit`: The core framework for building message-based applications.
- `RabbitMQ`: The message broker used by MassTransit for message transport.

## Running the Solution with Docker Compose

Instead of running the services individually, you can use Docker Compose to manage the containers. Docker Compose allows you to define and run multi-container applications with a single command.

```
docker-compose up
```
