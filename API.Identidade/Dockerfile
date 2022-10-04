#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./API.Identidade/API.Identidade.csproj", "API.Identidade/"]
COPY ["./API.Core/Core.csproj", "API.Core/"]
RUN dotnet restore "./API.Identidade/API.Identidade.csproj"
COPY . .
WORKDIR "API.Identidade"
RUN dotnet build "API.Identidade.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.Identidade.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Identidade.dll"]