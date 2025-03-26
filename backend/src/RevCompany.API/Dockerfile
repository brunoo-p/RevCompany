FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5117
ENV ASPNETCORE_ENVIRONMENT=Development

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ["src/RevCompany.Contracts/RevCompany.Contracts.csproj", "RevCompany.Contracts/"]
COPY ["src/RevCompany.Application/RevCompany.Application.csproj", "RevCompany.Application/"]
COPY ["src/RevCompany.Domain/RevCompany.Domain.csproj", "RevCompany.Domain/"]
COPY ["src/RevCompany.Infrastructure/RevCompany.Infrastructure.csproj", "RevCompany.Infrastructure/"]
COPY ["src/RevCompany.API/RevCompany.API.csproj", "RevCompany.API/"]

RUN dotnet restore "RevCompany.API/RevCompany.API.csproj"

COPY . .
RUN ls -la
WORKDIR /src/RevCompany.API
RUN dotnet build "./RevCompany.API.csproj" -c Release -o /app/build


FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./RevCompany.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "RevCompany.API.dll"]
