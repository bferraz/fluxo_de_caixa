#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./src/FluxoDeCaixa.Identidade.API/FluxoDeCaixa.Identidade.API.csproj", "FluxoDeCaixa.Identidade.API/"]
COPY ["./src/FluxoDeCaixa.Core/Core.csproj", "FluxoDeCaixa.Core/"]
RUN dotnet restore "./FluxoDeCaixa.Identidade.API/FluxoDeCaixa.Identidade.API.csproj"
COPY . .
WORKDIR "/src/src/FluxoDeCaixa.Identidade.API"
RUN dotnet build "FluxoDeCaixa.Identidade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoDeCaixa.Identidade.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoDeCaixa.Identidade.API.dll"]