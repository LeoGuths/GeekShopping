FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "GeekShopping.CartAPI.csproj" 
RUN dotnet publish "GeekShopping.CartAPI.csproj" -c -release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENV ASPNETORE_URLS=http://*:5010

ENTRYPOINT ["dotnet", "GeekShopping.CartAPI.dll"]