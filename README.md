# Token-Based Authentication (Udemy Course by Fatih Çakıroğlu)
Link : https://www.udemy.com/course/aspnet-core-api-token-bazli-kimlik-dogrulama-jwt/

## About

This project teaches how to authenticate endpoints and secure APIs using **Json Web Tokens (JWT)**. The main API, **AuthServer**, uses the **Asp.Net.Identity** library, which requires a login registry-based authentication (user, pass). 

Additionally, tokens can be created for clients using **ClientId** and **ClientSecret**, where registration is not required. The project follows the **N-Layer architecture**, primarily consisting of **Core**, **Repository**, and **Service Layers**.

The end result is that our **AuthenticationServer** creates **Access** and **Refresh Tokens**, which are used to make requests to the server, protecting the API.

## Useful Websites

1. **[jwt.io](https://jwt.io)**: Verify the integrity of your tokens and display the data in the payload.
2. **[dotnettutorials.net](https://dotnettutorials.net)**: Useful for tutorials and reference on .NET technologies.

---

## Steps to Test the API via Postman

### Prerequisite
- **Database**: SQL Server Management Studio 19

After cloning this project, if you cannot make requests using Postman, follow these steps:

### 1. Configure AppSettings to Connect to the Database
- In Visual Studio:
  1. Go to **View** -> **SQL Server Object Explorer**.
  2. Choose a **Database instance**.
  3. Right-click on **Databases** -> Select a Database (If no database is available, proceed to Step 2).
  4. Right-click **Properties** -> Copy the **Connection String** and replace the one in `appsettings.json` of the application.

### 2. Setup SQL Server Management Studio (SSMS)
- Open **SQL Server Management Studio**:
  1. Connect to the server as specified in the connection string.
  2. If no database was selected in Step 1, create a new database.
  
- At this point, your application knows the path to the database, and you can use **EF Migrations** to generate the necessary tables.

### 3. Use Entity Framework Migrations to Create Tables
- In Visual Studio:
  1. Delete the existing **Migrations** folder in the project.
  2. Go to **Tools** -> **Package Manager Console**.
  3. In the pop-up screen, from **Default Project**: select `NLayer-Repository`.
  4. Run the following commands:
     
     ```bash
     Add-Migration InitialMigration
     ```

     - If successful, the **Migrations** folder and Up-Down methods will be recreated.
  
  5. Run the following command to update the database:
  
     ```bash
     Update-Database
     ```

     - This command will apply the migrations to the database executing the Up method accordingly.

---

### Conclusion
If all steps are completed successfully, you will have connected the application to your local database and can now start making requests from Postman.

