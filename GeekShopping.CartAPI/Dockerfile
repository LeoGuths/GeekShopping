FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "GeekShopping.CartAPI.csproj" 
RUN dotnet publish "GeekShopping.CartAPI.csproj" -c -release --property WarningLevel=0 -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5010
COPY --from=build /app ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "GeekShopping.CartAPI.dll"]