#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalaryPayment.API/SalaryPayment.API.csproj", "SalaryPayment.API/"]
COPY ["SalaryPayment.Service/SalaryPayment.Service.csproj", "SalaryPayment.Service/"]
COPY ["OvetimePolicies/OvetimePolicies.csproj", "OvetimePolicies/"]
COPY ["SalaryPayment.Repository/SalaryPayment.Repository.csproj", "SalaryPayment.Repository/"]
COPY ["SalaryPayment.Model/SalaryPayment.Model.csproj", "SalaryPayment.Model/"]
RUN dotnet restore "SalaryPayment.API/SalaryPayment.API.csproj"
COPY . .
WORKDIR "/src/SalaryPayment.API"
RUN dotnet build "SalaryPayment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalaryPayment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalaryPayment.API.dll"]