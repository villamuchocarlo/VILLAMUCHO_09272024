# See https://aka.ms/customizecontainer to learn how to customize your debug container
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Correct the path to the csproj file within the FileProcessingAPI directory
COPY ["FileProcessingAPI/FileProcessingAPI.csproj", "FileProcessingAPI/"]
RUN dotnet restore "FileProcessingAPI/FileProcessingAPI.csproj"
# Copy all files from the FileProcessingAPI directory
COPY FileProcessingAPI/ FileProcessingAPI/
WORKDIR "/src/FileProcessingAPI"
RUN dotnet build "FileProcessingAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FileProcessingAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileProcessingAPI.dll"]
