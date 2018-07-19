FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 65260
EXPOSE 44394

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY RTL.TVMaze.API/RTL.TVMaze.API.csproj RTL.TVMaze.API/
RUN dotnet restore RTL.TVMaze.API/RTL.TVMaze.API.csproj
COPY . .
WORKDIR /src/RTL.TVMaze.API
RUN dotnet build RTL.TVMaze.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RTL.TVMaze.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RTL.TVMaze.API.dll"]
