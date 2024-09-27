FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Surplus.Food.Distribution.Chain/Surplus.Food.Distribution.Chain.csproj", "Surplus.Food.Distribution.Chain/"]
RUN dotnet restore "Surplus.Food.Distribution.Chain/Surplus.Food.Distribution.Chain.csproj"
COPY . .
WORKDIR "/src/Surplus.Food.Distribution.Chain"
RUN dotnet build "Surplus.Food.Distribution.Chain.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Surplus.Food.Distribution.Chain.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Surplus.Food.Distribution.Chain.dll"]