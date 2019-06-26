FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim
WORKDIR /app
COPY ./bin/Release/netcoreapp2.2 .
ENV ASPNETCORE_URLS http://*:80
#ENV ASPNETCORE_URLS https://*:443
#ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 80
EXPOSE 443
ENTRYPOINT dotnet S3.ApiGateway.dll


#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
#WORKDIR /src
#
#COPY S3.ApiGateway.csproj /src/S3.ApiGateway/
##COPY ["src/S3.ApiGateway/S3.ApiGateway.csproj", "src/S3.ApiGateway/"]
#
#RUN dotnet restore "/src/S3.ApiGateway/S3.ApiGateway.csproj"
##RUN dotnet restore "src/S3.ApiGateway/S3.ApiGateway.csproj"
#
#COPY . .
#WORKDIR /src/S3.ApiGateway
##WORKDIR "/src/src/S3.ApiGateway"
#
#RUN dotnet build "S3.ApiGateway.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "S3.ApiGateway.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "S3.ApiGateway.dll"]