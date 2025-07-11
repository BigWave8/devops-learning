# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OrderService/OrderService.csproj", "OrderService/"]
RUN dotnet restore "OrderService/OrderService.csproj"

COPY . .
WORKDIR "/src/OrderService"
RUN dotnet publish "OrderService.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Port 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "OrderService.dll"]