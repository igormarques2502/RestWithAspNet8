#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RestWithAspNetUdemy/RestWithAspNetUdemy.csproj", "RestWithAspNetUdemy/"]
RUN dotnet restore "./RestWithAspNetUdemy/./RestWithAspNetUdemy.csproj"
COPY . .
WORKDIR "/src/RestWithAspNetUdemy"
RUN dotnet build "./RestWithAspNetUdemy.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "RestWithAspNetUdemy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWithAspNetUdemy.dll"]