#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["API.Caixa/API.Caixa.csproj", "API.Caixa/"]
RUN dotnet restore "API.Caixa/API.Caixa.csproj"
COPY . .
WORKDIR "/src/API.Caixa"
RUN dotnet build "API.Caixa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.Caixa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Caixa.dll"]