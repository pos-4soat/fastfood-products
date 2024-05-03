FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "src/fastfood-products.csproj"
RUN dotnet build "src/fastfood-products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/fastfood-products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /publish ./

ENTRYPOINT ["dotnet", "fastfood-products.dll"]