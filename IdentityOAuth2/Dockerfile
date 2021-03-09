FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:6001 
EXPOSE 6001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IdentityOAuth2.csproj", "./"]
RUN dotnet restore "IdentityOAuth2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "IdentityOAuth2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityOAuth2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityOAuth2.dll"]
