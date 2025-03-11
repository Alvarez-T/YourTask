# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["YourTask.sln", "."]
COPY ["YourTask.Domain/YourTask.Domain.csproj", "YourTask.Domain/"]
COPY ["YourTask.Infrastructure/YourTask.Infrastructure.csproj", "YourTask.Infrastructure/"]
COPY ["YourTask.Presentation/YourTask.Presentation.csproj", "YourTask.Presentation/"]

# Restore dependencies
RUN dotnet restore "YourTask.Presentation/YourTask.Presentation.csproj"

# Copy all files

COPY ["YourTask.Domain/", "YourTask.Domain/"]
COPY ["YourTask.Infrastructure/", "YourTask.Infrastructure/"]
COPY ["YourTask.Presentation/", "YourTask.Presentation/"]

WORKDIR "/src/YourTask.Presentation"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "YourTask.Presentation.dll"]
