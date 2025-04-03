# TaskListSystemMVC

TaskListSystemMVC is a web-based task management system built using ASP.NET Core MVC. It provides an intuitive interface for managing daily tasks, tracking completed tasks, and summarizing task reports. The system supports CRUD operations, sorting, searching, pagination, and exporting task data to Excel.

# Features

- **Task Management:** Create, update, delete, and view tasks.
- **Sorting and Filtering:** Sort tasks by various parameters and apply search filters.
- **Pagination:** Display tasks with paging support.
- **Export to Excel:** Generate an Excel report of tasks using EPPlus.
- **Authentication:** Secure access using ASP.NET Core Identity.
- **Role-Based Access:** Restrict access based on user roles.

# Technologies Used

- **Frontend:** HTML, CSS, JavaScript
- **Backend:** ASP.NET Core MVC 9.0
- **Database:** Entity Framework Core with SQL Server
- **Authentication:** ASP.NET Core Identity
- **Excel Export:** EPPlus

# Installation

1) Clone the Repository
```
git clone https://github.com/StillNOOB-ing/TaskListSystemMVC.git
cd TaskListSystemMVC
```
2) Configure the Database
- Open appsettings.json and update the database connection string.
- Select the latest database in [Database Backup](https://github.com/StillNOOB-ing/TaskListSystemMVC/tree/master/DatabaseBackup) and restore it in your SSMS.

# Contribution

Feel free to fork this repository and contribute to improving the system! Submit pull requests with clear documentation of changes.

# License

This project is licensed under the MIT License.
