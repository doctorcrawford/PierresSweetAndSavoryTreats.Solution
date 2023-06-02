# Pierre's Sweet and Savory Treats

#### An application that allows users to create and view flavors and treats.

#### By Kyle Crawford

## Technologies Used

* C#
* .NET
* SQL
* MySQL
* MySQL Workbench
* Entity Framework Core
* Microsoft AspNetCore
* Microsoft Identity

## Description

This application gives the user the ability to keep see of all of the flavors and treats that have been created, including which flavors belong to which treats and vice versa. The app also has log in user functionality and only allows full CRUD to logged in users.

## How To Run This Project

### Install Tools

Install the tools that are introduced in [this series of lessons on LearnHowToProgram.com](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c).

### Set Up and Run Project

1. Clone this repo.
2. Open the terminal and navigate to this project's production directory called "Factory".
3. Within the production directory "Factory", create a new file called `appsettings.json`.
4. Within `appsettings.json`, put in the following code, replacing the `uid` and `pwd` values with your own username and password for MySQL.

```json
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=kyle_crawford;uid=[YOUR-USERNAME];pwd=[YOUR-PASSWORD];"
  }
}
```

5. Create the database using the migrations in the To Do List project. Open your shell (e.g., Terminal or GitBash) to the production directory "ToDoList", and run `dotnet ef database update`. 
    - To optionally create a migration, run the command `dotnet ef migrations add MigrationName` where `MigrationName` is your custom name for the migration in UpperCamelCase. To learn more about migrations, visit the LHTP lesson [Code First Development and Migrations](https://www.learnhowtoprogram.com/c-and-net-part-time/many-to-many-relationships/code-first-development-and-migrations).
6. Within the production directory "ToDoList", run `dotnet watch run` in the command line to start the project in development mode with a watcher.
7. Open the browser to _https://localhost:5001_. If you cannot access localhost:5001 it is likely because you have not configured a .NET developer security certificate for HTTPS. To learn about this, review this lesson: [Redirecting to HTTPS and Issuing a Security Certificate](https://www.learnhowtoprogram.com/lessons/redirecting-to-https-and-issuing-a-security-certificate).

## Known Bugs

* _Any known issues_
None

## License
[MIT](https://opensource.org/license/mit)

Copyright (c) 2023 Kyle Crawford