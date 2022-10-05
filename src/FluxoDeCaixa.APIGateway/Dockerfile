#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./src/FluxoDeCaixa.APIGateway/FluxoDeCaixa.APIGateway.csproj", "FluxoDeCaixa.APIGateway/"]
RUN dotnet restore "./FluxoDeCaixa.APIGateway/FluxoDeCaixa.APIGateway.csproj"
COPY . .
WORKDIR "/src/src/FluxoDeCaixa.APIGateway"
RUN dotnet build "FluxoDeCaixa.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoDeCaixa.APIGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoDeCaixa.APIGateway.dll"]