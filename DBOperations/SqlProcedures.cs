using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;

namespace SimpleProjectManagement
{
    public class SqlProcedures
    {
        private DBConnection dbConnection;

        public SqlProcedures()
        {
            dbConnection = new DBConnection();
        }

        /* ------------------------------------- DEPARTMENTS -------------------------------------------- */

        public string GetAllDepartments()
        {
            string sql = @"
                           SELECT  *
                           FROM    Departments
                           ORDER BY DepartmentID ASC";

            return sql;
        }

        public void LoadDepartmentListView(ListView listView)
        {
            string sql = @"
                           SELECT  *
                           FROM    Departments
                           ORDER BY DepartmentID ASC";

            dbConnection.FillListViewRows(listView, sql, null);
        }

        public void LoadDepartmentTabs(TabControl tab)
        {
            string sql = @"
                           SELECT  DepartmentName
                           FROM    Departments
                           ORDER BY DepartmentName ASC";

            dbConnection.FillTabs(tab, sql);
        }

        public DataTable DepartmentWithEmployees(int id)
        {
            string sql = @"
                            SELECT  *
                            FROM    Departments d
                            JOIN Employees e ON ( d.DepartmentID = e.DepartmentID )
                            WHERE   d.DepartmentID = @DepartmentID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@DepartmentID", SqlDbType.Int) { Value = id }
            };

            DataTable dt = dbConnection.FillDataTable(sql, parameters);

            return dt;
        }

        public void RemoveDepartment(int id)
        {
            string sql = @"
                            DELETE  FROM Departments
                            WHERE   DepartmentID = @DepartmentID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@DepartmentID", SqlDbType.Int) { Value = id }
            };

            dbConnection.ExecSQL(sql, parameters);
        }

        public void AddDepartment(string departmentName, string departmentFunction)
        {
            string sql = @"
                            INSERT  INTO Departments
                                    (
                                        DepartmentName,
                                        DepartmentFunction
                                    )
                            VALUES  (
                                        @DepartmentName,
                                        @DepartmentFunction
                                    )";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@DepartmentName", SqlDbType.NVarChar, 50) { Value = departmentName },
                new SqlCeParameter("@DepartmentFunction", SqlDbType.NText) { Value = departmentFunction }
            };

            dbConnection.ExecSQL(sql, parameters);
        }

        public void EditDepartment(int id, string departmentName, string departmentFunction)
        {
            string sql = @"
                            UPDATE  Departments
                            SET     DepartmentName = @DepartmentName,
                                    DepartmentFunction = @DepartmentFunction
                            WHERE   DepartmentID = @DepartmentID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@DepartmentName", SqlDbType.NVarChar, 50) { Value = departmentName },
                new SqlCeParameter("@DepartmentFunction", SqlDbType.NText) { Value = departmentFunction },
                new SqlCeParameter("@DepartmentID", SqlDbType.Int) { Value = id }
            };

            dbConnection.ExecSQL(sql, parameters);
        }

        /* ---------------------------------------------------------------------------------------------- */

        /* ------------------------------------- EMPLOYEES ---------------------------------------------- */

        public string GetAllEmployees()
        {
            string sql = @"
                           SELECT  EmployeeID,
                                   FirstName + ' ' + LastName as FullName
                           FROM    Employees
                           ORDER BY EmployeeID ASC";
            return sql;
        }

        public void LoadEmployeesListView(ListView listView, TabControl tab)
        {
            string sql;

            if (tab.SelectedTab.Text == "All")
            {
                sql = @"
                        SELECT  EmployeeID,
                                FirstName + ' ' + LastName,
                                JobTitle,
                                EmailAddress
                        FROM    Employees
                        ORDER BY EmployeeID ASC";

                dbConnection.FillListViewRows(listView, sql, null);
            }
            else
            {
                sql = @"
                        SELECT  EmployeeID,
                                FirstName + ' ' + LastName,
                                JobTitle,
                                EmailAddress
                        FROM    Employees e
                        JOIN Departments d ON ( e.DepartmentID = d.DepartmentID )
                        WHERE   d.DepartmentName = @DepartmentName
                        ORDER BY EmployeeID ASC";

                List<SqlCeParameter> parameters = new List<SqlCeParameter>()
                {
                    new SqlCeParameter("@DepartmentName", SqlDbType.NVarChar, 50) { Value = tab.SelectedTab.Text }
                };

                dbConnection.FillListViewRows(listView, sql, parameters);
            }
        }

        public DataRow GetEmployee(int id)
        {
            string sql = @"
                            SELECT  *
                            FROM    Employees
                            WHERE   EmployeeID = @EmployeeID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value = id }
            };

            DataTable dt = dbConnection.FillDataTable(sql, parameters);
            DataRow dr = dt.Rows[0];

            return dr;
        }

        public void RemoveEmployee(int id)
        {
            string sql = @"
                            DELETE  FROM Employees
                            WHERE   EmployeeID = @EmployeeID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value = id }
            };

            dbConnection.ExecSQL(sql, parameters);
        }

        public void AddEmployee(string firstName, string lastName, DateTime? birthDate, string jobTitle,
            int departmentID, DateTime? dateHired, string email, string bussinesPhone,
            string homePhone, string mobilePhone, string faxNumber, string address, string city,
            string stateProvince, string postalCode, string country, string notes)
        {
            string sql = @"
                            INSERT INTO Employees (
	                            FirstName,
	                            LastName,
	                            JobTitle,
	                            EmailAddress,
	                            BusinessPhone,
	                            HomePhone,
	                            MobilePhone,
	                            FaxNumber,
	                            Address,
	                            City,
	                            StateProvince,
	                            PostalCode,
	                            Country,
	                            Notes,
	                            BirthDate,
	                            DateHired,
	                            DepartmentID
                            ) VALUES ( 
                                @FirstName,	                            
                                @LastName,
	                            @JobTitle,
	                            @EmailAddress,
	                            @BusinessPhone,
	                            @HomePhone,
	                            @MobilePhone,
	                            @FaxNumber,
	                            @Address,
	                            @City,
	                            @StateProvince,
	                            @PostalCode,
	                            @Country,
	                            @Notes,
	                            @BirthDate,
	                            @DateHired,
	                            @DepartmentID)";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@FirstName", SqlDbType.NVarChar, 50) { Value =  firstName },
                new SqlCeParameter("@LastName", SqlDbType.NVarChar, 50) { Value =  lastName },
                new SqlCeParameter("@JobTitle", SqlDbType.NVarChar, 50) { Value =  jobTitle },
                new SqlCeParameter("@EmailAddress", SqlDbType.NVarChar, 50) { Value =  email },
                new SqlCeParameter("@BusinessPhone", SqlDbType.NVarChar, 30) { Value =  bussinesPhone },
                new SqlCeParameter("@HomePhone", SqlDbType.NVarChar, 30) { Value =  homePhone },
                new SqlCeParameter("@MobilePhone", SqlDbType.NVarChar, 30) { Value =  mobilePhone },
                new SqlCeParameter("@FaxNumber", SqlDbType.NVarChar, 30) { Value =  faxNumber },
                new SqlCeParameter("@Address", SqlDbType.NVarChar, 50) { Value =  address },
                new SqlCeParameter("@City", SqlDbType.NVarChar, 50) { Value =  city },
                new SqlCeParameter("@StateProvince", SqlDbType.NVarChar, 50) { Value =  stateProvince },
                new SqlCeParameter("@PostalCode", SqlDbType.NVarChar, 15) { Value =  postalCode },
                new SqlCeParameter("@Country", SqlDbType.NVarChar, 50) { Value =  country },
                new SqlCeParameter("@Notes", SqlDbType.NText) { Value =  notes },
                new SqlCeParameter("@DepartmentID", SqlDbType.Int) { Value = departmentID }
            };

            SqlCeParameter bD = new SqlCeParameter();
            bD.ParameterName = "@BirthDate";
            bD.Value = (object)birthDate ?? DBNull.Value;
            bD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(bD);

            SqlCeParameter dH = new SqlCeParameter();
            dH.ParameterName = "@DateHired";
            dH.Value = (object)dateHired ?? DBNull.Value;
            dH.SqlDbType = SqlDbType.DateTime;
            parameters.Add(dH);

            dbConnection.ExecSQL(sql, parameters);
        }

        public void EditEmployee(int empID, string firstName, string lastName, DateTime? birthDate, string jobTitle,
            int departmentID, DateTime? dateHired, string email, string bussinesPhone,
            string homePhone, string mobilePhone, string faxNumber, string address, string city,
            string stateProvince, string postalCode, string country, string notes)
        {
            string sql = @"
                            UPDATE  Employees
                            SET     FirstName = @FirstName,
                                    LastName = @LastName,
                                    JobTitle = @JobTitle,
                                    EmailAddress = @EmailAddress,
                                    BusinessPhone = @BusinessPhone,
                                    HomePhone = @HomePhone,
                                    MobilePhone = @MobilePhone,
                                    FaxNumber = @FaxNumber,
                                    Address = @Address,
                                    City = @City,
                                    StateProvince = @StateProvince,
                                    PostalCode = @PostalCode,
                                    Country = @Country,
                                    Notes = @Notes,
                                    BirthDate = @BirthDate,
                                    DateHired = @DateHired,
                                    DepartmentID = @DepartmentID
                            WHERE   EmployeeID = @EmployeeID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@FirstName", SqlDbType.NVarChar, 50) { Value =  firstName },
                new SqlCeParameter("@LastName", SqlDbType.NVarChar, 50) { Value =  lastName },
                new SqlCeParameter("@JobTitle", SqlDbType.NVarChar, 50) { Value =  jobTitle },
                new SqlCeParameter("@EmailAddress", SqlDbType.NVarChar, 50) { Value =  email },
                new SqlCeParameter("@BusinessPhone", SqlDbType.NVarChar, 30) { Value =  bussinesPhone },
                new SqlCeParameter("@HomePhone", SqlDbType.NVarChar, 30) { Value =  homePhone },
                new SqlCeParameter("@MobilePhone", SqlDbType.NVarChar, 30) { Value =  mobilePhone },
                new SqlCeParameter("@FaxNumber", SqlDbType.NVarChar, 30) { Value =  faxNumber },
                new SqlCeParameter("@Address", SqlDbType.NVarChar, 50) { Value =  address },
                new SqlCeParameter("@City", SqlDbType.NVarChar, 50) { Value =  city },
                new SqlCeParameter("@StateProvince", SqlDbType.NVarChar, 50) { Value =  stateProvince },
                new SqlCeParameter("@PostalCode", SqlDbType.NVarChar, 15) { Value =  postalCode },
                new SqlCeParameter("@Country", SqlDbType.NVarChar, 50) { Value =  country },
                new SqlCeParameter("@Notes", SqlDbType.NText) { Value =  notes },
                new SqlCeParameter("@DepartmentID", SqlDbType.Int) { Value = departmentID },
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value = empID }
            };

            SqlCeParameter bD = new SqlCeParameter();
            bD.ParameterName = "@BirthDate";
            bD.Value = (object)birthDate ?? DBNull.Value;
            bD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(bD);

            SqlCeParameter dH = new SqlCeParameter();
            dH.ParameterName = "@DateHired";
            dH.Value = (object)dateHired ?? DBNull.Value;
            dH.SqlDbType = SqlDbType.DateTime;
            parameters.Add(dH);

            dbConnection.ExecSQL(sql, parameters);
        }

        /* ---------------------------------------------------------------------------------------------- */


        /* -------------------------------------- PROJECTS ---------------------------------------------- */

        public string GetAllProjects()
        {
            string sql = @"
                           SELECT  ProjectID,
                                   ProjectName
                           FROM    Projects
                           ORDER BY ProjectID ASC";
            return sql;
        }

        public void LoadProjectsListView(ListView listView, TabControl tab)
        {
            string sql;

            if (tab.SelectedTab.Text == "All")
            {
                sql = @"
                        SELECT  p.ProjectID,
                                p.ProjectName,
                                e.FirstName + ' ' + e.LastName,
                                pr.Priority,
                                p.StartDate,
                                p.EndDate
                        FROM    Projects p
                        JOIN Employees e ON ( p.EmployeeID = e.EmployeeID )
                        JOIN Status s ON ( p.StatusID = s.StatusID )
                        JOIN Priority pr ON ( p.PriorityID = pr.PriorityID )
                        ORDER BY p.ProjectID ASC";

                dbConnection.FillListViewRows(listView, sql, null);
            }
            else
            {
                sql = @"
                        SELECT  p.ProjectID,
                                p.ProjectName,
                                e.FirstName + ' ' + e.LastName,
                                pr.Priority,
                                p.StartDate,
                                p.EndDate
                        FROM    Projects p
                        JOIN Employees e ON ( p.EmployeeID = e.EmployeeID )
                        JOIN Status s ON ( p.StatusID = s.StatusID )
                        JOIN Priority pr ON ( p.PriorityID = pr.PriorityID )
                        WHERE s.Status = @Status
                        ORDER BY p.ProjectID ASC";

                List<SqlCeParameter> parameters = new List<SqlCeParameter>()
                {
                    new SqlCeParameter("@Status", SqlDbType.NVarChar, 50) { Value = tab.SelectedTab.Text }
                };

                dbConnection.FillListViewRows(listView, sql, parameters);
            }
        }

        public void AddProject(string projectName, int owner, int priority, int status,
            DateTime? startDate, DateTime? endDate, string notes)
        {
            string sql = @"
                            INSERT INTO Projects (
	                            ProjectName,
	                            EmployeeID,
	                            StatusID,
	                            PriorityID,
	                            StartDate,
	                            EndDate,
	                            Notes
                            ) VALUES ( 
	                            @ProjectName,
	                            @EmployeeID,
	                            @StatusID,
	                            @PriorityID,
	                            @StartDate,
	                            @EndDate,
	                            @Notes)";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@ProjectName", SqlDbType.NVarChar, 50) { Value = projectName },
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value = owner },
                new SqlCeParameter("@StatusID", SqlDbType.Int) { Value = status },
                new SqlCeParameter("@PriorityID", SqlDbType.Int) { Value = priority },
                new SqlCeParameter("@Notes", SqlDbType.NText) { Value = notes }
            };

            SqlCeParameter sD = new SqlCeParameter();
            sD.ParameterName = "@StartDate";
            sD.Value = (object)startDate ?? DBNull.Value;
            sD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(sD);

            SqlCeParameter eD = new SqlCeParameter();
            eD.ParameterName = "@EndDate";
            eD.Value = (object)endDate ?? DBNull.Value;
            eD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(eD);

            dbConnection.ExecSQL(sql, parameters);
        }

        public void EditProject(int projID, string projectName, int owner, int priority, int status,
            DateTime? startDate, DateTime? endDate, string notes)
        {
            string sql = @"
                            UPDATE  Projects
                            SET     ProjectName = @ProjectName,
                                    EmployeeID = @EmployeeID,
                                    StatusID = @StatusID,
                                    PriorityID = @PriorityID,
                                    StartDate = @StartDate,
                                    EndDate = @EndDate,
                                    Notes = @Notes
                            WHERE   ProjectID = @ProjectID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@ProjectName", SqlDbType.NVarChar, 50) { Value =  projectName },
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value =  owner },
                new SqlCeParameter("@StatusID", SqlDbType.Int)  { Value =  status },
                new SqlCeParameter("@PriorityID", SqlDbType.Int)  { Value =  priority },
                new SqlCeParameter("@Notes", SqlDbType.NText) { Value =  notes },
                new SqlCeParameter("@ProjectID", SqlDbType.Int) { Value =  projID },
            };

            SqlCeParameter sD = new SqlCeParameter();
            sD.ParameterName = "@StartDate";
            sD.Value = (object)startDate ?? DBNull.Value;
            sD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(sD);

            SqlCeParameter eD = new SqlCeParameter();
            eD.ParameterName = "@EndDate";
            eD.Value = (object)endDate ?? DBNull.Value;
            eD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(eD);

            dbConnection.ExecSQL(sql, parameters);
        }

        public DataRow GetProject(int id)
        {
            string sql = @"
                            SELECT  *
                            FROM    Projects
                            WHERE   ProjectID = @ProjectID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@ProjectID", SqlDbType.Int) { Value = id }
            };

            DataTable dt = dbConnection.FillDataTable(sql, parameters);
            DataRow dr = dt.Rows[0];

            return dr;
        }

        public DataTable GetProjectsJoinedTasks(int id)
        {
            string sql = @"
                            SELECT  *
                            FROM    Projects p
                            JOIN Tasks t ON ( p.ProjectID = t.ProjectID )
                            WHERE   p.ProjectID = @ProjectID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@ProjectID", SqlDbType.Int) { Value = id }
            };

            DataTable dt = dbConnection.FillDataTable(sql, parameters);

            return dt;
        }

        public void RemoveProject(int id)
        {
            string sql = @"
                            DELETE  FROM Projects
                            WHERE   ProjectID = @ProjectID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@ProjectID", SqlDbType.Int) { Value = id }
            };

            dbConnection.ExecSQL(sql, parameters);
        }

        public DataTable EmployeeOwnerOnProject(int id)
        {
            string sql = @"
                            SELECT  *
                            FROM    Employees e
                            JOIN Projects p ON ( e.EmployeeID = p.EmployeeID )
                            WHERE   e.EmployeeID = @EmployeeID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value = id }
            };

            DataTable dt = dbConnection.FillDataTable(sql, parameters);

            return dt;
        }

        /* ---------------------------------------------------------------------------------------------- */


        /* ----------------------------------------- TASKS ---------------------------------------------- */

        public void LoadTasksListView(ListView listView, TabControl tab)
        {
            string sql;

            if (tab.SelectedTab.Text == "All")
            {
                sql = @"
                        SELECT  t.TaskID,
                                t.TaskName,
                                p.ProjectName,
                                e.FirstName + ' ' + e.LastName AS FullName,
                                pr.Priority,
                                t.StartDate,
                                t.DueDate
                        FROM    Tasks t
                        JOIN Employees e ON ( t.EmployeeID = e.EmployeeID )
                        JOIN Projects p ON ( t.ProjectID = p.ProjectID )
                        JOIN Priority pr ON ( t.PriorityID = pr.PriorityID )
                        ORDER BY t.TaskID ASC";

                dbConnection.FillListViewRows(listView, sql, null);
            }
            else
            {
                sql = @"
                        SELECT  t.TaskID,
                                t.TaskName,
                                p.ProjectName,
                                e.FirstName + ' ' + e.LastName AS FullName,
                                pr.Priority,
                                t.StartDate,
                                t.DueDate
                        FROM    Tasks t
                        JOIN Employees e ON ( t.EmployeeID = e.EmployeeID )
                        JOIN Projects p ON ( t.ProjectID = p.ProjectID )
                        JOIN Priority pr ON ( t.PriorityID = pr.PriorityID )
                        JOIN Status s ON ( t.StatusID = s.StatusID )
                        WHERE s.Status = @Status
                        ORDER BY t.TaskID ASC";

                List<SqlCeParameter> parameters = new List<SqlCeParameter>()
                {
                    new SqlCeParameter("@Status", SqlDbType.NVarChar, 50) { Value = tab.SelectedTab.Text }
                };

                dbConnection.FillListViewRows(listView, sql, parameters);
            }
        }

        public void LoadTasksListView(ListView listView, int id)
        {
            string sql = @"
                            SELECT  TaskID,
                                    TaskName,
                                    e.FirstName + ' ' + e.LastName,
                                    s.Status,
                                    pr.Priority,
                                    StartDate,
                                    DueDate
                            FROM    Tasks t
                            JOIN Status s ON ( t.StatusID = s.StatusID )
                            JOIN Priority pr ON ( t.PriorityID = pr.PriorityID )
                            JOIN Employees e ON ( t.EmployeeID = e.EmployeeID )
                            WHERE   ProjectID = @ProjectID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                 new SqlCeParameter("@ProjectID", SqlDbType.Int) { Value = id }
            };

            dbConnection.FillListViewRows(listView, sql, parameters);
        }

        public void AddTask(string taskName, int project, int assignedTo, int priority, int status,
            DateTime? startDate, DateTime? dueDate, string description)
        {
            string sql = @"
                            INSERT INTO Tasks (
	                            ProjectID,
	                            TaskName,
	                            StatusID,
	                            PriorityID,
                                EmployeeID,
                                Description,
	                            StartDate,
	                            DueDate
                            ) VALUES ( 
	                            @ProjectID,
	                            @TaskName,
	                            @StatusID,
	                            @PriorityID,
	                            @EmployeeID,
	                            @Description,
	                            @StartDate,
                                @DueDate)";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@ProjectID", SqlDbType.Int) { Value =  project },
                new SqlCeParameter("@TaskName", SqlDbType.NVarChar, 50) { Value =  taskName },
                new SqlCeParameter("@StatusID", SqlDbType.Int) { Value =  status },
                new SqlCeParameter("@PriorityID", SqlDbType.Int) { Value =  priority },
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value =  assignedTo },
                new SqlCeParameter("@Description", SqlDbType.NText) { Value =  description }
            };

            SqlCeParameter sD = new SqlCeParameter();
            sD.ParameterName = "@StartDate";
            sD.Value = (object)startDate ?? DBNull.Value;
            sD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(sD);

            SqlCeParameter dD = new SqlCeParameter();
            dD.ParameterName = "@DueDate";
            dD.Value = (object)dueDate ?? DBNull.Value;
            dD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(dD);

            dbConnection.ExecSQL(sql, parameters);
        }

        public void EditTask(int taskID, string taskName, int project, int assignedTo, int priority, int status,
            DateTime? startDate, DateTime? dueDate, string description)
        {
            string sql = @"
                            UPDATE Tasks
                            SET    ProjectID = @ProjectID,
	                               TaskName = @TaskName,
	                               StatusID = @StatusID,
	                               PriorityID = @PriorityID,
                                   EmployeeID = @EmployeeID,
                                   Description = @Description,
	                               StartDate = @StartDate,
	                               DueDate = @DueDate
                            WHERE TaskID = @TaskID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@ProjectID", SqlDbType.Int) { Value =  project },
                new SqlCeParameter("@TaskName", SqlDbType.NVarChar, 50) { Value =  taskName },
                new SqlCeParameter("@StatusID", SqlDbType.Int) { Value =  status },
                new SqlCeParameter("@PriorityID", SqlDbType.Int) { Value =  priority },
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value =  assignedTo },
                new SqlCeParameter("@Description", SqlDbType.NText) { Value =  description },
                new SqlCeParameter("@TaskID", SqlDbType.Int) { Value =  taskID}
            };

            SqlCeParameter sD = new SqlCeParameter();
            sD.ParameterName = "@StartDate";
            sD.Value = (object)startDate ?? DBNull.Value;
            sD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(sD);

            SqlCeParameter dD = new SqlCeParameter();
            dD.ParameterName = "@DueDate";
            dD.Value = (object)dueDate ?? DBNull.Value;
            dD.SqlDbType = SqlDbType.DateTime;
            parameters.Add(dD);

            dbConnection.ExecSQL(sql, parameters);
        }

        public DataRow GetTask(int id)
        {
            string sql = @"
                            SELECT  *
                            FROM    Tasks
                            WHERE   TaskID = @TaskID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@TaskID", SqlDbType.Int) { Value = id }
            };

            DataTable dt = dbConnection.FillDataTable(sql, parameters);
            DataRow dr = dt.Rows[0];

            return dr;
        }

        public void RemoveTask(int id)
        {
            string sql = @"
                            DELETE  FROM Tasks
                            WHERE   TaskID = @TaskID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@TaskID", SqlDbType.Int) { Value = id }
            };

            dbConnection.ExecSQL(sql, parameters);
        }

        public DataTable EmployeeAssignedToTask(int id)
        {
            string sql = @"
                            SELECT  *
                            FROM    Employees e
                            JOIN Tasks t ON ( e.EmployeeID = t.EmployeeID)
                            WHERE   e.EmployeeID = @EmployeeID";

            List<SqlCeParameter> parameters = new List<SqlCeParameter>()
            {
                new SqlCeParameter("@EmployeeID", SqlDbType.Int) { Value = id }
            };

            DataTable dt = dbConnection.FillDataTable(sql, parameters);

            return dt;
        }
        /* ---------------------------------------------------------------------------------------------- */


        /* ----------------------------------------- RAZNO ---------------------------------------------- */

        public void LoadStatusTabs(TabControl tab)
        {
            string sql = @"
                           SELECT  Status
                           FROM    Status
                           ORDER BY StatusID ASC";

            dbConnection.FillTabs(tab, sql);
        }

        public string GetPriority()
        {
            string sql = @"
                           SELECT  *
                           FROM    Priority
                           ORDER BY PriorityID ASC";

            return sql;
        }

        public string GetStatus()
        {
            string sql = @"
                           SELECT  *
                           FROM    Status
                           ORDER BY StatusID ASC";

            return sql;
        }
        /* ---------------------------------------------------------------------------------------------- */
    }
}
