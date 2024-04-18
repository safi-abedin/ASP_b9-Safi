FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["StackOverFlow.Web/*.csproj", "StackOverFlow.Web/"]
COPY ["StackOverFlow.Application/*.csproj", "StackOverFlow.Application/"]
COPY ["StackOverFlow.Domain/*.csproj", "StackOverFlow.Domain/"]
COPY ["StackOverFlow.Infrastructure/*.csproj", "StackOverFlow.Infrastructure/"]
RUN dotnet restore "StackOverFlow.Web/StackOverFlow.Web.csproj"
COPY . .
WORKDIR "/src/StackOverFlow.Web"
RUN dotnet build "StackOverFlow.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StackOverFlow.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "StackOverFlow.Web.dll"]