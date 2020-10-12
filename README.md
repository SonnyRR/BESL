# ![BESL](https://res.cloudinary.com/vasil-kotsev/image/upload/v1565288701/BESL/besl-logo.png)

Bulgarian eSports League /pronounced: Be·es·el·/. My defense project for ASP.NET Core MVC course at SoftUni (June-August 2019). 

🏆 Awarded 1st place in top 5 best projects (June-August 2019).

[![Build status](https://ci.appveyor.com/api/projects/status/a8x6minra5yhem07?svg=true)](https://ci.appveyor.com/project/SonnyRR/besl)
[![Build Status](https://sonnyrr.visualstudio.com/BESL/_apis/build/status/SonnyRR.BESL?branchName=master)](https://sonnyrr.visualstudio.com/BESL/_build/latest?definitionId=1&branchName=master)
[![codecov](https://codecov.io/gh/SonnyRR/BESL/branch/master/graph/badge.svg)](https://codecov.io/gh/SonnyRR/BESL)

BESL is an online eSports league for competitive tournaments on various games and formats. Everyone with a Steam account can create a team with their fellow friends and sign up for the current season tournament. Skill levels are represented by tiers and range from Open, Mid and Premiership. Match fixtures are scheduled in play weeks where teams face each other every week in order to reach the top skill table rankings.

# 🛠 Built with:
* CQRS & MediatR
* ASP.NET Core MVC
* EF Core 2.2
* SignalR
* Fluent validation
* Custom exception-based notifications with Redis*, MediatR & ASP.NET middleware pipelines.
* Hangfire
* Sendgrid
* CloudinaryDotNet
* SteamWebApi2
* Steam.Models 
* OpenId.Steam
* Shouldly
* Moq
* MockQueryable
* OpenCover

&nbsp;

# ⚙️ Local setup
1. Make sure you have the following app secrets set in either the 'Secret Manager' or 'Azure KeyVault' if you decide to deploy this application:
    * Cloudinary:
        - cloudinary-cloud
        - cloudinary-apiKey
        - cloudinary-apiSecret
    * Steam
        - steam-api-key
    * SendGrid
        - sendgrid-api-key

2. Run the following docker command from the root directory of the repository to set up the necessary containers (you could also start the containers in detached mode if you want):
``` 
docker-compose -f docker-compose.Development.yml up
```

📌 __The Web App container is build under the "Development" environment and the developer exception page is enabled. If you manage to get a HTTP 500 response you will be greeted with the stack-trace of the exception.__

3. To shutdown all containers used by the application run the following command from the root directory of the repository:
```
docker-compose -f docker-compose.Development.yml down
```