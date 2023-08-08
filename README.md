# arcade platform

Play here - https://cmarcade.azurewebsites.net

Full-stack project that aims to remake classic old games like snake, space invaders, breakout and others for web, coding everything from the ground up (without a game engine) using C# .NET 6, with Blazor and ASP.NET server.

The project also features an API that allows users to submit their highest scores and view a leaderboard for each game, this API is exposed on the ASP.NET backend, working through Dapper and an external PostgreSQL database.  

The app is hosted on Microsoft Azure Apps (https://azure.microsoft.com/en-us/products/app-service), using a continuous deployment pipeline with github actions.
The external PostgreSQL database is hosted on https://supabase.com/
