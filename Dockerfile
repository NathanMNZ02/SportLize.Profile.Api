#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/SportLize.Profile.Api/SportLize.Profile.Api.csproj", "src/SportLize.Profile.Api/"]
RUN dotnet restore "src/SportLize.Profile.Api/SportLize.Profile.Api.csproj"
COPY . .
WORKDIR "/src/src/SportLize.Profile.Api"
RUN dotnet build "SportLize.Profile.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportLize.Profile.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportLize.Profile.Api.dll"]
