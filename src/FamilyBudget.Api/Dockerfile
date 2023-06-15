FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FamilyBudget.Api/FamilyBudget.Api.csproj", "src/FamilyBudget.Api/"]
RUN dotnet restore "src/FamilyBudget.Api/FamilyBudget.Api.csproj"
COPY . .
WORKDIR "/src/src/FamilyBudget.Api"
RUN dotnet build "FamilyBudget.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FamilyBudget.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamilyBudget.Api.dll"]
