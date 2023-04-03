# arcade platform

Play here - https://cmarcade.azurewebsites.net

Full-stack project that aims to rekame classic games like snake, space invaders, breakout and others in C# for web from scratch (without a game engine) using Blazor and ASP.NET server.

Also featuring an API allowing users to keep high scores and showing leaderboards for each game, working through Dapper on PostgreSQL.  

The app is hosted on Microsoft Azure Apps through a continuous deployment pipeline using github actions.
Scores are stored on an external Postgresql database, hosted on https://supabase.com/
