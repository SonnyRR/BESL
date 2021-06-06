# ![BESL](https://res.cloudinary.com/vasil-kotsev/image/upload/v1565288701/BESL/besl-logo.png)

Bulgarian eSports League /pronounced: Be·es·el·/. My defense project for ASP.NET Core MVC course at SoftUni (June-August 2019).

🏆 Awarded 1st place in top 5 best projects (June-August 2019).

<!-- [![Build status](https://ci.appveyor.com/api/projects/status/a8x6minra5yhem07?svg=true)](https://ci.appveyor.com/project/SonnyRR/besl) -->

[![ci](https://github.com/SonnyRR/BESL/actions/workflows/ci.yml/badge.svg)](https://github.com/SonnyRR/BESL/actions/workflows/ci.yml)
[![Build Status](https://sonnyrr.visualstudio.com/BESL/_apis/build/status/SonnyRR.BESL?branchName=master)](https://sonnyrr.visualstudio.com/BESL/_build/latest?definitionId=1&branchName=master)
[![codecov](https://codecov.io/gh/SonnyRR/BESL/branch/master/graph/badge.svg)](https://codecov.io/gh/SonnyRR/BESL)
![](http://estruyf-github.azurewebsites.net/api/VisitorHit?user=SonnyRR&repo=BESL&countColorcountColor&countColor=%237B1E7A)

BESL is an online eSports league for competitive tournaments on various games and formats. Everyone with a Steam account can create a team with their fellow friends and sign up for the current season tournament. Skill levels are represented by tiers and range from Open, Mid and Premiership. Match fixtures are scheduled in play weeks where teams face each other every week in order to reach the top skill table rankings.

# 🛠 Built with:

-   CQRS & MediatR
-   ASP.NET Core MVC
-   NUKE Build System
-   EF Core 2.2
-   SignalR
-   Fluent validation
-   Custom exception-based notifications with Redis\*, MediatR & ASP.NET middleware pipelines.
-   Hangfire
-   Sendgrid
-   CloudinaryDotNet
-   SteamWebApi2
-   Steam.Models
-   OpenId.Steam
-   Shouldly
-   Moq
-   MockQueryable
-   Coverlet

# ⚙️ Local setup using Docker
The following instructions are tested on `macOS` & `Linux` if you're running a `Windows` machine, you'll have to change certain directory routes in both the `docker-compose` files and `Kestrel` user secrets.
### BESL Application Secrets

1. Make sure you have the following app secrets set in either the 'Secret Manager' or 'Azure KeyVault' if you decide to deploy this application:
    - Cloudinary:
        - cloudinary-cloud
        - cloudinary-apiKey
        - cloudinary-apiSecret
    - Steam
        - steam-api-key
    - SendGrid
        - sendgrid-api-key

### Creating a development HTTPS certificate

1. Clean development certificates:

```sh
  dotnet dev-certs https --clean
```

2. Export the HTTPS certificate using the dev-certs global tool and a password of your choice:

```ps
# Windows
dotnet dev-certs https -v ep ${USERPROFILE}/.aspnet/https/cert.pfx -p aR@ndomPassw0rd
```

```sh
# MacOS/Linux
dotnet dev-certs https -v -ep ~/.aspnet/https/cert.pfx -p aR@ndomPassw0rd
```

3. Trust the new development certificate **[OPTIONAL]**

```sh
dotnet dev-certs https -t #The certificate is trusted automatically after generating it.
```

### Adding the required 'user secrets' for the development HTTPS certificate

\*The user secrets will not be available under 'Production' environment so keep that in mind if you want to run the container with `docker compose` you'll need to temporarily change the `ASPNETCORE_ENVIRONMENT` variable value to `Development` for example.

1. **[Optional]** Initialize the user secrets if you haven't already.

```sh
dotnet user-secrets init
```

2. Run the following commands in the project folder in order to set the certificate path and password:

```sh
dotnet user-secrets remove "Kestrel:Certificates:Default:Password"
dotnet user-secrets remove "Kestrel:Certificates:Default:Path"
dotnet user-secrets set "Kestrel:Certificates:Default:Password" "{your password here}"
dotnet user-secrets set "Kestrel:Certificates:Default:Path" "/root/.aspnet/https/cert.pfx"
```

### Starting the containers

\* The volumes mounted in the `docker-compose` files are Linux/MacOS paths, if you're running Docker + WSL2 make sure that the volumes are mounted correctly:

```yml
volumes:
    - ${APPDATA}/Microsoft/UserSecrets:/root/.microsoft/usersecrets:ro
    - ${USERPROFILE}/.aspnet/https:/root/.aspnet/https:ro
```


1. Run the following docker command from the root directory of the repository to start up the necessary containers:

```sh
#Development
docker-compose -f docker-compose.yml -f docker-compose.development.yml up
```

📌 **Before running the application under the 'Production' environment ensure that the necessary credentials are added in the appsettings.production.json file. e.g. Azure KeyVault, Redis credentials, etc.**

```sh
#Production
docker-compose -f docker-compose.yml -f docker-compose.production.yml up
```

2. To stop the containers press `CTRL+C` in your terminal client or use the Docker desktop utility.

### Removing the containers

2. To remove all containers used by the application run the following command from the root directory of the repository:

```sh
#Development
docker-compose -f docker-compose.yml -f docker-compose.development.yml down
```

```sh
#Production
docker-compose -f docker-compose.yml -f docker-compose.production.yml down
```
