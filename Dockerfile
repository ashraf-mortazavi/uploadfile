FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CsvFileUploadApp.csproj", "./"]
RUN dotnet restore "CsvFileUploadApp.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "CsvFileUploadApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CsvFileUploadApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CsvFileUploadApp.dll"]


FROM mcr.microsoft.com/mssql/server:2022-latest
EXPOSE 1433

FROM rabbitmq:3-management
EXPOSE 5672,15672

