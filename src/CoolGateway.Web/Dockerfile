#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CoolGateway.Web/CoolGateway.WebApi.csproj", "src/CoolGateway.Web/"]
COPY ["src/CoolGateway.Infrastructure/CoolGateway.Infrastructure.csproj", "src/CoolGateway.Infrastructure/"]
COPY ["src/CoolGateway.Domain/CoolGateway.Domain.csproj", "src/CoolGateway.Domain/"]
COPY ["src/CoolGateway.Application/CoolGateway.Application.csproj", "src/CoolGateway.Application/"]
RUN dotnet restore "src/CoolGateway.Web/CoolGateway.WebApi.csproj"
COPY . .
WORKDIR "/src/src/CoolGateway.Web"
RUN dotnet build "CoolGateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoolGateway.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoolGateway.WebApi.dll"]