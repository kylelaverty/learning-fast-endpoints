#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Build arguments
ARG BUILDCONFIGURATION=Release
ARG BUILDNUMBER=9999

FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-alpine3.19-amd64 AS base
WORKDIR /app

# Set the user to run the app as non-root. This is a best practice for security reasons.
USER app

# Declare ports above 1024 as an unprivileged non-root user cannot bind to ports < 1024
# The base image includes this declaration, but only for HTTP over port 80.
# It is still possible to override ASPNETCORE_URLS at deployment time (ie. in Kubernetes),
# but the ports still need to be over 1024.
EXPOSE 8080
EXPOSE 8081

#ENV ASPNETCORE_URLS=https://+:5001;http://+:5000

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0.203-alpine3.19-amd64 AS build
# Inside the "FROM" we need to bring the build arguments into scope by restating them.
ARG BUILDCONFIGURATION
ARG BUILDNUMBER

WORKDIR /source

# copy csproj and restore as distinct layers
COPY src/learning-fast-endpoints/*.csproj .
RUN dotnet restore "learning-fast-endpoints.csproj"


# copy and publish app and libraries
COPY src/learning-fast-endpoints/. .
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "learning-fast-endpoints.csproj" -c ${BUILDCONFIGURATION} -o /app/publish --no-restore

# Enable globalization and time zones:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "learning-fast-endpoints.dll"]
