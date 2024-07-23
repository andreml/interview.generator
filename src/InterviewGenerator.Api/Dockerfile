FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InterviewGenerator.Api/InterviewGenerator.Api.csproj", "src/InterviewGenerator.Api/"]
COPY ["src/InterviewGenerator.Application/InterviewGenerator.Application.csproj", "src/InterviewGenerator.Application/"]
COPY ["src/InterviewGenerator.Domain/InterviewGenerator.Domain.csproj", "src/InterviewGenerator.Domain/"]
COPY ["src/InterviewGenerator.CrossCutting/InterviewGenerator.CrossCutting.csproj", "src/InterviewGenerator.CrossCutting/"]
COPY ["src/InterviewGenerator.Infra/InterviewGenerator.Infra.csproj", "src/InterviewGenerator.Infra/"]
RUN dotnet restore "./src/InterviewGenerator.Api/InterviewGenerator.Api.csproj"
COPY . .
WORKDIR "/src/src/InterviewGenerator.Api"
RUN dotnet build "./InterviewGenerator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InterviewGenerator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterviewGenerator.Api.dll"]