FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GeekShopping.Email/GeekShopping.Email.csproj", "GeekShopping.Email/"]
RUN dotnet restore "GeekShopping.Email/GeekShopping.Email.csproj"
COPY . .
WORKDIR "/src/GeekShopping.Email"
RUN dotnet build "GeekShopping.Email.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeekShopping.Email.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekShopping.Email.dll"]
