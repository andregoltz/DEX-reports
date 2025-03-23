# DEX-reports

# How to Run the Project

To ensure the project runs correctly, follow the steps below after cloning the repository:

## 1. Clean and Build the Solution
- Open the project in Visual Studio or your preferred editor.
- Go to **Build** and select **Clean Solution** to ensure any old files or cache are removed.
- After cleaning, select **Build Solution** to compile the project from scratch.

## 2. Configure the Database Connection
- Make sure to add the connection string for the database in the **appsettings.json** file or the environment settings for the project.
- The connection string should be in the correct format based on the database type you are using (SQL Server, MySQL, etc.).
- Restore the backupDatabase.bak in your local machine

## 3. Select Multiple Startup Projects
- To ensure the application runs properly, you need to select multiple startup projects.
- In Visual Studio, right-click on the solution name and select **Set Startup Projects**.
- Choose **DEX.API** and **DEX.UI** as startup projects.

## 4. Run the Solution
- After configuring the steps above, press **Ctrl + F5** to run the solution, or use the run button to start both the projects (API and UI).

---

Make sure to follow these steps to avoid any issues while running the project. If you encounter any errors related to database connection or project initialization, please review your configuration and try again.

