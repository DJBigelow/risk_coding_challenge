#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Maksad_Client/Maksad_Client.csproj", "Maksad_Client/"]
COPY ["Risk.Shared/Risk.Shared.csproj", "Risk.Shared/"]
RUN dotnet restore "Maksad_Client/Maksad_Client.csproj"
COPY . .
WORKDIR "/src/Maksad_Client"
RUN dotnet build "Maksad_Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maksad_Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maksad_Client.dll"]
