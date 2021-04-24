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
