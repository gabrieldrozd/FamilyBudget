# FamilyBudget

Simple family budget management application built with React, .NET Core, and PostgreSQL.
Application consists of a client application and a server application that communicate via REST API.

## Technologies Used

- React
- TypeScript
- Mantine UI
- .NET Core
- C#
- Entity Framework Core
- MediatR
- PostgreSQL
- Docker

# Running the Application Locally

## Step 1: Start the Database using Docker

Navigate to the root directory of the project, where the ```docker-compose.yml``` file is located, and run:

```cmd
docker-compose up -d
```

## Step 2: Install client dependencies and run application

Navigate to the ```/client``` directory and run:

```cmd
npm install
npm run dev
```

## Step 3: Restore .NET Packages and start the server application

Navigate to the ```/src/FamilyBudget.Api``` directory, and run:

```cmd
dotnet restore
dotnet run
```

## Access the Application

Client application:
```
http://localhost:3000
```

Server application:
```
http://localhost:5000
or
https://localhost:5001
```