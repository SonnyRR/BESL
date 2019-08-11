#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS build
WORKDIR /src
COPY ["BESL.Web/BESL.Web.csproj", "BESL.Web/"]
COPY ["BESL.Web.Tests/BESL.Web.Tests.csproj", "BESL.Web.Tests/"]
COPY ["BESL.Common/BESL.Common.csproj", "BESL.Common/"]
COPY ["BESL.Application/BESL.Application.csproj", "BESL.Application/"]
COPY ["BESL.Application.Tests/BESL.Application.Tests.csproj", "BESL.Application.Tests/"]
COPY ["BESL.Domain/BESL.Domain.csproj", "BESL.Domain/"]
COPY ["BESL.Infrastructure/BESL.Infrastructure.csproj", "BESL.Infrastructure/"]
COPY ["BESL.Persistence/BESL.Persistence.csproj", "BESL.Persistence/"]
COPY ["entrypoint.sh", "/app/entrypoint.sh"]

RUN dotnet restore "BESL.Web/BESL.Web.csproj"
COPY . .
WORKDIR "/src/BESL.Web"
RUN dotnet build "BESL.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BESL.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN chmod 777 entrypoint.sh \
    && ln -s /entrypoint.sh /
ENTRYPOINT ["./entrypoint.sh"]