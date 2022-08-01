#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["poc-intercepeter-api/poc-intercepeter-api.csproj", "poc-intercepeter-api/"]
RUN dotnet restore "poc-intercepeter-api/poc-intercepeter-api.csproj"
COPY . .
WORKDIR "/src/poc-intercepeter-api"
RUN dotnet build "poc-intercepeter-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "poc-intercepeter-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#COPY localhost.crt /usr/local/share/ca-certificates
#RUN chmod 644 /usr/local/share/ca-certificates/localhost.crt && update-ca-certificates
ENTRYPOINT ["dotnet", "poc-intercepeter-api.dll"]