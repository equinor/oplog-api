# Use the .NET 8 SDK image based on Alpine
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

# Install additional packages
RUN apk add --no-cache libc6-compat icu

# Copy csproj and restore as distinct layers
COPY ./Oplog.Api/Oplog.Api.csproj ./Oplog.Api/Oplog.Api.csproj
COPY ./Oplog.Core/Oplog.Core.csproj ./Oplog.Core/Oplog.Core.csproj
COPY ./Oplog.Persistence/Oplog.Persistence.csproj ./Oplog.Persistence/Oplog.Persistence.csproj

RUN dotnet restore Oplog.Api/Oplog.Api.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish Oplog.Api -c Release -o Oplog.Api/out

# Build runtime image based on Alpine
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ="Europe/Oslo"
RUN apk -U add --no-cache icu tzdata

# Copy the published output from the build stage
COPY --from=build /app/Oplog.Api/out .

# Create a non-root user and group with numeric UID/GID
RUN addgroup -g 1001 non-root-group && \
    adduser -u 1001 -G non-root-group -s /bin/sh -D non-root-user

# Switch to the non-root user
USER 1001:1001

EXPOSE 5000

ENTRYPOINT ["dotnet", "Oplog.Api.dll"]