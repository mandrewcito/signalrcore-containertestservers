#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=some
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=keyStore.p12
ENV ENV DOTNET_RUNNING_IN_CONTAINER=true	
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=https://+:443;http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /SignalRSample
COPY ["SignalRSample.csproj", ""]
RUN dotnet restore "./SignalRSample.csproj"
COPY . .
WORKDIR "/SignalRSample/."
RUN dotnet build "SignalRSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalRSample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY keyStore.p12 keyStore.p12
ENTRYPOINT ["dotnet", "SignalRSample.dll"]