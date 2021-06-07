#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# install libgdiplus for System.Drawing
RUN apt-get update && \
    apt-get install -y  libgdiplus libc6-dev
# install x11 for System.Drawing
RUN apt-get update && \
    apt-get install -y  libx11-dev

ENV GOOGLE_APPLICATION_CREDENTIALS=/app/a.json

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["salesNowBackend.csproj", ""]
RUN dotnet restore "./salesNowBackend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "salesNowBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "salesNowBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "salesNowBackend.dll"]