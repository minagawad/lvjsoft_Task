# d:\Practices\lvjsoft\lvjsoft_Task\GuessGame.WebAPI\Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY GuessGame.Domain ./GuessGame.Domain
COPY GuessGame.Application ./GuessGame.Application
COPY GuessGame.Infrastructure ./GuessGame.Infrastructure
COPY GuessGame.WebAPI ./GuessGame.WebAPI
RUN dotnet restore ./GuessGame.WebAPI/GuessGame.WebAPI.csproj
RUN dotnet publish ./GuessGame.WebAPI/GuessGame.WebAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5048
EXPOSE 5048
ENTRYPOINT ["dotnet", "GuessGame.WebAPI.dll"]