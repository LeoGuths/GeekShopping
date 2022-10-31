FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GeekShopping.PaymentAPI/GeekShopping.PaymentAPI.csproj", "GeekShopping.PaymentAPI/"]
RUN dotnet restore "GeekShopping.PaymentAPI/GeekShopping.PaymentAPI.csproj"
COPY . .
WORKDIR "/src/GeekShopping.PaymentAPI"
RUN dotnet build "GeekShopping.PaymentAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekShopping.PaymentAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekShopping.PaymentAPI.dll"]
