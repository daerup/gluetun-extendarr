# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# These ARGs allow for swapping out the base used to make the final image when debugging from VS
ARG LAUNCHING_FROM_VS
# This sets the base image for final, but only if LAUNCHING_FROM_VS has been defined
ARG FINAL_BASE_IMAGE=${LAUNCHING_FROM_VS:+aotdebug}

# Set global ARGs used for environment variables
ARG INPUT_DIR="/input"
ARG OUTPUT_DIR="/output"
ARG CONFIG_NAME="custom.conf"

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base 
USER $APP_UID
WORKDIR /app

ARG INPUT_DIR
ARG OUTPUT_DIR
ARG CONFIG_NAME
ENV INPUT_DIR=$INPUT_DIR
ENV OUTPUT_DIR=$OUTPUT_DIR
ENV CONFIG_NAME=$CONFIG_NAME

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GluetunExtendarr.App/GluetunExtendarr.App.csproj", "GluetunExtendarr.App/"]
RUN dotnet restore "./GluetunExtendarr.App/GluetunExtendarr.App.csproj"
COPY . .
WORKDIR "/src/GluetunExtendarr.App"
RUN dotnet build "./GluetunExtendarr.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GluetunExtendarr.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:DebugType=none /p:DebugSymbols=false /p:PublishTrimmed=true /p:Optimize=true /p:PublishSingleFile=false

# This stage is used as the base for the final stage when launching from VS to support debugging in regular mode (Default when not using the Debug configuration)
FROM base AS aotdebug
USER root
# Install GDB to support native debugging
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    gdb
USER app

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM ${FINAL_BASE_IMAGE:-mcr.microsoft.com/dotnet/nightly/runtime-deps:8.0-noble-chiseled-aot} AS final

WORKDIR /app
COPY --from=publish /app/publish .

ARG INPUT_DIR
ARG OUTPUT_DIR
ARG CONFIG_NAME
ENV INPUT_DIR=$INPUT_DIR
ENV OUTPUT_DIR=$OUTPUT_DIR
ENV CONFIG_NAME=$CONFIG_NAME

ENV DISTROLESS_HEALTHCHECKS_URI="http://localhost/healthz"
COPY --from=ghcr.io/alexaka1/distroless-dotnet-healthchecks:1.0.0 / /healthchecks
HEALTHCHECK --start-period=5s CMD ["/healthchecks/Distroless.HealthChecks"]

EXPOSE 80
VOLUME /input
ENV ASPNETCORE_HTTP_PORTS=80
ENV SERILOG__MINIMUMLEVEL__Default="Information"

ENTRYPOINT ["./GluetunExtendarr.App"]
