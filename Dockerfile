#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
EXPOSE 5000/tcp
EXPOSE 5001/tcp

FROM base AS build
LABEL builder=true
WORKDIR /src

COPY ["BESL.Web", "./BESL.Web"]
COPY ["BESL.SharedKernel", "./BESL.SharedKernel"]
COPY ["BESL.Application", "./BESL.Application"]
COPY ["BESL.Application.Tests", "./BESL.Application.Tests"]
COPY ["BESL.Entities", "./BESL.Entities"]
COPY ["BESL.Infrastructure", "./BESL.Infrastructure"]
COPY ["BESL.Persistence", "./BESL.Persistence"]
COPY ["*.sln", "."]

RUN dotnet restore "BESL.Web/BESL.Web.csproj"

COPY . ./

WORKDIR "/src/BESL.Web"
RUN dotnet build "BESL.Web.csproj" -c Debug -o /app

FROM build AS publish
LABEL builder=true
RUN dotnet publish "BESL.Web.csproj" -c Debug -o /app/publish

FROM base AS final  
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "BESL.Web.dll"]