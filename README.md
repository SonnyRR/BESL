# ![BESL](https://res.cloudinary.com/vasil-kotsev/image/upload/v1565288701/BESL/besl-logo.png)

Bulgarian eSports League /pronounced: Be·es·el·/. My defense project for ASP.NET Core MVC course at SoftUni (June-August 2019). 

🏆 Awarded 1st place in top 5 best projects (June-August 2019).

[![Build status](https://ci.appveyor.com/api/projects/status/a8x6minra5yhem07?svg=true)](https://ci.appveyor.com/project/SonnyRR/besl)
[![Build status](https://dev.azure.com/VasilKotsev/BESL/_apis/build/status/BESL-Azure%20Web%20App%20for%20ASP.NET-CI)](https://dev.azure.com/VasilKotsev/BESL/_build/latest?definitionId=2)
[![codecov](https://codecov.io/gh/SonnyRR/BESL/branch/master/graph/badge.svg)](https://codecov.io/gh/SonnyRR/BESL)

BESL is an online eSports league for competitive tournaments on various games and formats. Everyone with a Steam account can create a team with their fellow friends and sign up for the current season tournament. Skill levels are represented by tiers and range from Open, Mid and Premiership. Match fixtures are scheduled in play weeks where teams face each other every week in order to reach the top skill table rankings.

Written entirely in ASP.NET Core MVC 2.2 with CQRS architecture + Mediator pattern for the business layer, focusing on separation of concerns, testability, scalability and code quality.

# 🛠 Built with:
* Domain-driven design
* CQRS & MediatR
* SignalR
* Fluent validation
* Custom exception-based notifications with Redis*, MediatR & ASP.NET middleware pipelines.
* Hangfire
* EF Core 2.2
* CloudinaryDotNet
* SteamWebApi2, Steam.Models, OpenId.Steam
* Shouldy, Moq, MockQueryable
