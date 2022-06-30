FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LeoEcommerce.csproj", "./"]
RUN dotnet restore "LeoEcommerce.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "LeoEcommerce.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeoEcommerce.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeoEcommerce.dll"]
