#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Foodly_new/Foodly_new.csproj", "Foodly_new/"]
RUN dotnet restore "Foodly_new/Foodly_new.csproj"
COPY . .
WORKDIR "/src/Foodly_new"
RUN dotnet build "Foodly_new.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Foodly_new.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Foodly_New.dll