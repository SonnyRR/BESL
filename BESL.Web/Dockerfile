#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["BESL.Web/BESL.Web.csproj", "BESL.Web/"]
RUN dotnet restore "BESL.Web/BESL.Web.csproj"
COPY . .
WORKDIR "/src/BESL.Web"
RUN dotnet build "BESL.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BESL.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BESL.Web.dll"]