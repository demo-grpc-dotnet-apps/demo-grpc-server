FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DemoComp.DemoGrpcServer/DemoComp.DemoGrpcServer.csproj", "DemoComp.DemoGrpcServer/"]
RUN dotnet restore "DemoComp.DemoGrpcServer/DemoComp.DemoGrpcServer.csproj"
COPY . .
WORKDIR "/src/DemoComp.DemoGrpcServer"
RUN dotnet build "DemoComp.DemoGrpcServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DemoComp.DemoGrpcServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoComp.DemoGrpcServer.dll"]
