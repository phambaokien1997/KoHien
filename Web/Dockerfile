#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web/BookStore.Web.csproj", "Web/"]
COPY ["Core/BookStore.Core/BookStore.Core.csproj", "Core/BookStore.Core/"]
RUN dotnet restore "./Web/BookStore.Web.csproj"
COPY . .
WORKDIR "/src/Web"
RUN dotnet build "./BookStore.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookStore.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN echo "Current directory is: $(pwd)"
ENTRYPOINT ["dotnet", "BookStore.Web.dll"]