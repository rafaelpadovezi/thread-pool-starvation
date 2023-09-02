FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

COPY . .

RUN apt-get update && apt-get -y install curl
RUN dotnet tool install --global dotnet-stack && dotnet tool install --global dotnet-counters

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet build