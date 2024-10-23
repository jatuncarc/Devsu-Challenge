FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountMS/AccountMS.csproj", "AccountMS/"]
RUN dotnet restore "AccountMS/AccountMS.csproj"
COPY . .
WORKDIR "/src/AccountMS"
RUN dotnet build "AccountMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountMS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountMS.dll"]