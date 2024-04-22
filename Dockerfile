FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ReadingIsGood/ReadingIsGood.csproj", "ReadingIsGood/"]
RUN dotnet restore "ReadingIsGood/ReadingIsGood.csproj"
COPY . .
WORKDIR "/src/ReadingIsGood"
RUN dotnet build "ReadingIsGood.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReadingIsGood.csproj" -c Release -o /app/publish

FROM postgres:latest AS db
ENV POSTGRES_USER postgres
ENV POSTGRES_PASSWORD Ao2Ru2ae34qlk
ENV POSTGRES_DB ReadingIsGood

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadingIsGood.dll"]
