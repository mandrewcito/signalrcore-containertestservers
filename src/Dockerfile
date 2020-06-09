#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=some
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=keyStore.p12
ENV ENV DOTNET_RUNNING_IN_CONTAINER=true	
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=https://+:443;http://+:80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src.csproj", ""]
RUN dotnet restore "./src.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "src.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY keyStore.p12 keyStore.p12
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "src.dll"]


