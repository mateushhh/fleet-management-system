# --- ETAP 1: Budowanie i publikacja aplikacji ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src

# Kopiowanie plików projektów i przywracanie zależności
COPY ["TransitDesk.Core/TransitDesk.Core.csproj", "TransitDesk.Core/"]
COPY ["TransitDesk.Web/TransitDesk.Web.csproj", "TransitDesk.Web/"]
COPY ["TransitDesk.Tests/TransitDesk.Tests.csproj", "TransitDesk.Tests/"]
RUN dotnet restore "TransitDesk.Web/TransitDesk.Web.csproj"

# Kopiowanie całego kodu źródłowego i publikacja
COPY . .
WORKDIR "/src/TransitDesk.Web"
RUN dotnet publish "TransitDesk.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- ETAP 2: Obraz uruchomieniowy (Runtime) ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransitDesk.Web.dll"]