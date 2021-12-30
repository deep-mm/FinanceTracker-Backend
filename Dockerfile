#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY FinanceTrackerAPI/FinanceTrackerAPI.csproj ./FinanceTrackerAPI/
RUN dotnet restore "FinanceTrackerAPI/FinanceTrackerAPI.csproj"

COPY FinanceTrackerAPI/. ./FinanceTrackerAPI/
COPY FinancialTracker.Common/. ./FinancialTracker.Common/
COPY FinancialTracker.Core.Lib/. ./FinancialTracker.Core.Lib/
COPY FinancialTracker.Models/. ./FinancialTracker.Models/
COPY FinancialTracker.Services/. ./FinancialTracker.Services/
COPY FinancialTracker.Data/. ./FinancialTracker.Data/
WORKDIR /src/FinanceTrackerAPI
RUN dotnet build "FinanceTrackerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinanceTrackerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceTrackerAPI.dll"]