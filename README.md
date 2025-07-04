# Friends And Places App - Getting Started

This guide explains how to run the Friends And Places application using either direct .NET execution or Docker.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop/) (if using Docker)
- [Geoapify API Key](https://www.geoapify.com/) (required for location services)

## Running Locally

### 1. Set Environment Variables

You need to set the GEOAPIFY_API_KEY environment variable:

```bash
# For Windows
set GEOAPIFY_API_KEY=your-api-key-here

# For Linux/MacOS
export GEOAPIFY_API_KEY=your-api-key-here
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run the Application

```bash
dotnet run
```

The API will be available at: `http://localhost:5227`

## Running with Docker

### 1. Build the Docker Image

```bash
docker build -t friendsandplaces:latest .
```

### 2. Run the Container

```bash
docker run -p 8080:8080 -e GEOAPIFY_API_KEY=your-api-key-here friendsandplaces:latest
```

The API will be available at: `http://localhost:8080`

### Using Pre-built Image from GitHub Container Registry

If you have access to the GitHub Container Registry, you can pull and run the pre-built image:

```bash
docker pull ghcr.io/jenzmann/friendsandplaces:1.0.0
docker run -p 8080:8080 -e GEOAPIFY_API_KEY=your-api-key-here ghcr.io/jenzmann/friendsandplaces:1.0.0
```

## API Documentation

Once running, the API documentation is available at:
- Local: `http://localhost:5227/swagger`
- Docker: `http://localhost:8080/swagger`

## Available Endpoints

- Authentication: `/login`, `/logout`, `/checkLoginName`, `/addUser`
- Users: `/getBenutzer`, `/getStandort`, `/setStandort`
- Location: `/getOrt`, `/getStandortPerAdresse`