# FootballManager
Welcome to all Happycoders to my small web API to manage football teams!
This readme is designed to give you a basic idea of the project organizanion.

This application, as requested, uses .NET core 3.1 and uses Microsoft technologies for the backend part so the API is written in ASP.net Core and the Data Access Layer is uses EF Core.

For this application I used a Domain Driven Design approach, and I tried to achieve the cleanest and most modular code that was possible without making thing overly complex.

The design idea is to divide everything in the following layers:

1. Application: The actual software the users interact with (like the API in this case). Implements the logic to have users access the domain
2. Domain: The models and business logic that for the specific domain at hand (in this case: football). The code that has to do with football should is here!
3. Infrastructure: Everything that the domain needs to work properly. For instance here we define how data is persisted, or how logs are stored/displayed.

Application and Infrastucture depend on the Domain, but the Domain has no dependency on them. This means that it is possible to change infrastructure (eg. persist to Mongo) or Application (eg. serve over gRPC) without affecting the business logic which is the core of the application.

All source code is contained in the src folder, while tests are contained in the tests folder.

# Packages used
For this project the following packages were used:
1. AutoMapper - This was used to map domain model data to DTOs before serving them, to avoid overexposing the internal data
2. EF Core - For database persistance (as requested by assignement)
3. Swashbuckle - To create a swagger endpoint
4. Bogus - To autogenerate data for seeding the database and during some tests

# Testing
There are two main test ideas followed:

* Domain was unit tested. All the business logic has some kind of unit test attached to it.
* An higher level functional test of the API was done to test some calls against the in-memory seeded database. This is useful to test the behavior of the API return codes.

For this test, as I had not much time unfortunately, the functional tests are only demonstrated briefily and are not extended to every possible case.

# Deployment
The API was deployed on Azure at https://football-heply.azurewebsites.net
The swagger is at https://football-heply.azurewebsites.net/swagger

# Final notes and remarks
I had fun making this demo. I've tried to work towards a clean architecture that was not the obvious auto-scaffolded API from ASP.net core.
My goal was to expore and show Domain Driven Design to make such API in a clean, modular and manageble way.
Unfortunately I was very busy with my job because I'm leaving for China this week, so I had a lot of things to do. This meant that I couldn't do any work on the UI.
Also some things here could be cleaner than they are now:
* Improve incapsulation of Child Entities in the domain model to avoid developers to circumvent business logic
* The API could use some more love for improved usability

If you think this demo is looking ok but you still think it needs some work, or you really want to see some frontend code I can work on that, but starting from half May.