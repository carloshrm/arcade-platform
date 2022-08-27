#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["gamesPlatform/Server/gamesPlatform.Server.csproj", "gamesPlatform/Server/"]
COPY ["gamesPlatform/Client/gamesPlatform.Client.csproj", "gamesPlatform/Client/"]
COPY ["gamesPlatform/Shared/gamesPlatform.Shared.csproj", "gamesPlatform/Shared/"]
RUN dotnet restore "gamesPlatform/Server/gamesPlatform.Server.csproj"
COPY . .
WORKDIR "/src/gamesPlatform/Server"
RUN dotnet build "gamesPlatform.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gamesPlatform.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gamesPlatform.Server.dll"]