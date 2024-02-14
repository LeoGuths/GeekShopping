FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GeekShopping.CartAPI/GeekShopping.CartAPI.csproj", "GeekShopping.CartAPI/"]
RUN dotnet restore "GeekShopping.CartAPI/GeekShopping.CartAPI.csproj"
COPY . .
WORKDIR "/src/GeekShopping.CartAPI"
RUN dotnet build "GeekShopping.CartAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GeekShopping.CartAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekShopping.CartAPI.dll"]
