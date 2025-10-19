FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["ApiConsumerProduct/src/ApiConsumerProduct/ApiConsumerProduct.csproj", "./ApiConsumerProduct/src/ApiConsumerProduct/"]
COPY ["SharedKernel/SharedKernel.csproj", "./SharedKernel/"]
RUN dotnet restore "./ApiConsumerProduct/src/ApiConsumerProduct/ApiConsumerProduct.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet build "ApiConsumerProduct/src/ApiConsumerProduct/ApiConsumerProduct.csproj" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "ApiConsumerProduct/src/ApiConsumerProduct/ApiConsumerProduct.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "/app/ApiConsumerProduct.dll"]
