# TruckManager

TruckManager is a basic application to manage trucks. It covers the CRUD operations (**C**reate, **R**ead, **U**pdate, **D**elete), and has it protected by authentication.

### Architecture

It is developed in _.NET 5_ and _ASP .NET MVC_.

Makes use of _ASP .NET Identity_ to deal with authentication functionalities, and _Entity Framework_ for querying and persistence in a _SQL Server LocalDb_.
The database creation and maintenance is made automatically on the application startup, using _EF Migrations_.

It uses the default _.NET dependency injection mechanism_, to reduce coupling and make unit testing easier.

It was also used the _Repository Pattern_ to decouple the data access from the business logic.

## Running the application
In _Visual Studio_, you can run the application using either:
* _IIS Express_: https://localhost:5001/
* _Kestrel_: https://localhost:44330/

After the application starts, the _Welcome_ page will be presented, with a link to send you to the trucks listing.
If you click this link without have previously logged in, you will be redirected to the login page, where you will be able to login with an existing account, or create a new one.

## Tests
The solution contains a _XUnit_ test project, using _Moq_ to mock classes dependencies (eg: repository, to avoid database access), and _Fluent Assertions_ to validate test results.

To run the tests, simply build the solution, open `Test Explorer`, and click `Run all tests`.

To verify code coverage is necessary to execute the following steps in Visual Studio:
* In the Package Manager Console, run the command `dotnet tool install --global dotnet-reportgenerator-globaltool`.
* In Extensions, install `Run Coverlet Report` extension.
* In Tools, select `Run Code Coverage` option.

_Ps: The business logic was all created in the service layer, so the tests were focused on these, causing the code coverage having a high percentage on this one, but resulting in a low percentage among the others layers._