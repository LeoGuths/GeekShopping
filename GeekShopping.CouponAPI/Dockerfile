FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "GeekShopping.CouponAPI.csproj" 
RUN dotnet publish "GeekShopping.CouponAPI.csproj" -c -release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENV ASPNETORE_URLS=http://*:5020

ENTRYPOINT ["dotnet", "GeekShopping.CouponAPI.dll"]