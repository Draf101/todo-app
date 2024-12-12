# TODO App

A simple TODO application built with the following stack:

- **Frontend**: Vue 3 with Vite and SCSS
- **Backend**: .NET 9/ASP.NET Core (C#)
- **Database**: SQLite
- **Containerization**: Docker & Docker Compose

## Features

- Add, delete, and update TODO items
- Persistent storage using SQLite
- Full-stack integration with REST API

## Prerequisites

Ensure you have the following installed:

1. [Docker](https://www.docker.com/get-started)
2. [Docker Compose](https://docs.docker.com/compose/install/)



### 1. Clone the Repository

```bash
git clone https://github.com/Draf101/todo-app.git
cd todo-app
```


### 2. Build and Run the Application

Run the following command to build and start both the frontend and backend services:

```bash
docker-compose up --build
```

This will:
- Build the frontend and backend Docker images
- Start the services defined in the `docker-compose.yml` file


### 3. Access the Application

- **Frontend**: Open your browser and navigate to [http://localhost:5173](http://localhost:5173).
- **Backend API**: The backend API is available at [http://localhost:7277](http://localhost:7277).

## API Endpoints

### Base URL
http://localhost:7277

| Method | Endpoint      | Description          |
|--------|---------------|----------------------|
| GET    | /todo         | Get all TODO items  |
| GET    | /todo/&lt;id&gt;    | Get one TODO item  |
| POST   | /todo         | Add a new TODO item |
| PUT    | /todo/&lt;id&gt;    | Update a TODO item  |
| DELETE | /todo/&lt;id&gt;    | Delete a TODO item  |
