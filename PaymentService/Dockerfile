# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PaymentService/PaymentService.csproj", "PaymentService/"]
RUN dotnet restore "PaymentService/PaymentService.csproj"

COPY . .
WORKDIR "/src/PaymentService"
RUN dotnet publish "PaymentService.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Port 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "PaymentService.dll"]