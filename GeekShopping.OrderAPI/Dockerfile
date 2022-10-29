FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GeekShopping.OrderAPI/GeekShopping.OrderAPI.csproj", "GeekShopping.OrderAPI/"]
RUN dotnet restore "GeekShopping.OrderAPI/GeekShopping.OrderAPI.csproj"
COPY . .
WORKDIR "/src/GeekShopping.OrderAPI"
RUN dotnet build "GeekShopping.OrderAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekShopping.OrderAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekShopping.OrderAPI.dll"]
