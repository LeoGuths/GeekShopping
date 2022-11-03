FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GeekShopping.APIGateway/GeekShopping.APIGateway.csproj", "GeekShopping.APIGateway/"]
RUN dotnet restore "GeekShopping.APIGateway/GeekShopping.APIGateway.csproj"
COPY . .
WORKDIR "/src/GeekShopping.APIGateway"
RUN dotnet build "GeekShopping.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekShopping.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekShopping.APIGateway.dll"]
