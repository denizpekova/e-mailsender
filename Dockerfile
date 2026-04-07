# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["e-mailsender/e-mailsender.csproj", "e-mailsender/"]
RUN dotnet restore "e-mailsender/e-mailsender.csproj"

COPY . .
WORKDIR "/src/e-mailsender"
RUN dotnet publish "e-mailsender.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "e-mailsender.dll"]
