
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CustomerMS/CustomerMS.csproj", "CustomerMS/"]
RUN dotnet restore "CustomerMS/CustomerMS.csproj"
COPY . .
WORKDIR "/src/CustomerMS"
RUN dotnet build "CustomerMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerMS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerMS.dll"]