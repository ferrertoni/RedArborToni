using Microsoft.Extensions.Configuration;
using RedArborToni.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RedArborToni.DataAccess
{
    public class EmployeeRepo
    {
        public IConfiguration _configuration { get; }
        string connectionString = string.Empty;

        public EmployeeRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("ConnectionString").Value;
        }

        public IEnumerable<EmployeeModel> GetEmployees()
        {
            string queryString = @"SELECT * FROM Employees";
            List<EmployeeModel> listEmployees = new List<EmployeeModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new EmployeeModel();
                        employee.EmployeeID = (int)reader["EmployeeID"];
                        employee.CompanyId = (int)reader["CompanyId"];
                        employee.CreatedOn = reader["CreatedOn"].ToString();
                        employee.DeletedOn = reader["DeletedOn"].ToString();
                        employee.Email = reader["Email"].ToString();
                        employee.Fax = reader["Fax"].ToString();
                        employee.Name = reader["Name"].ToString();
                        employee.Lastlogin = reader["Lastlogin"].ToString();
                        employee.Password = reader["Password"].ToString();
                        employee.PortalId = (int)reader["PortalId"];
                        employee.RoleId = (int)reader["RoleId"];
                        employee.StatusId = (int)reader["StatusId"];
                        employee.Telephone = reader["Telephone"].ToString();
                        employee.UpdatedOn = reader["UpdatedOn"].ToString();
                        employee.Username = reader["Username"].ToString();

                        listEmployees.Add(employee);
                    }
                }
            }

            return listEmployees;
        }

        public EmployeeModel GetEmployeeByID(int employeeID)
        {
            string queryString = $"SELECT TOP (1) * FROM Employees WHERE EmployeeID = {employeeID}";
            var employee = new EmployeeModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee.EmployeeID = (int)reader["EmployeeID"];
                        employee.CompanyId = (int)reader["CompanyId"];
                        employee.CreatedOn = reader["CreatedOn"].ToString();
                        employee.DeletedOn = reader["DeletedOn"].ToString();
                        employee.Email = reader["Email"].ToString();
                        employee.Fax = reader["Fax"].ToString();
                        employee.Name = reader["Name"].ToString();
                        employee.Lastlogin = reader["Lastlogin"].ToString();
                        employee.Password = reader["Password"].ToString();
                        employee.PortalId = (int)reader["PortalId"];
                        employee.RoleId = (int)reader["RoleId"];
                        employee.StatusId = (int)reader["StatusId"];
                        employee.Telephone = reader["Telephone"].ToString();
                        employee.UpdatedOn = reader["UpdatedOn"].ToString();
                        employee.Username = reader["Username"].ToString();
                    }
                }
            }

            return employee;
        }

        public EmployeeModel CreateEmployee(EmployeeModel employee)
        {
            var employeeResult = new EmployeeModel();
            string queryString = $"INSERT INTO Employees OUTPUT INSERTED.* " +
                $"VALUES ({employee.CompanyId},'{employee.CreatedOn}','{employee.DeletedOn}','{employee.Email}','{employee.Fax}','{employee.Name}','{employee.Lastlogin}'," +
                $"'{employee.Password}',{employee.PortalId},{employee.RoleId},{employee.StatusId},'{employee.Telephone}','{employee.UpdatedOn}','{employee.Username}')";            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeResult.EmployeeID = (int)reader["EmployeeID"];
                        employeeResult.CompanyId = (int)reader["CompanyId"];
                        employeeResult.CreatedOn = reader["CreatedOn"].ToString();
                        employeeResult.DeletedOn = reader["DeletedOn"].ToString();
                        employeeResult.Email = reader["Email"].ToString();
                        employeeResult.Fax = reader["Fax"].ToString();
                        employeeResult.Name = reader["Name"].ToString();
                        employeeResult.Lastlogin = reader["Lastlogin"].ToString();
                        employeeResult.Password = reader["Password"].ToString();
                        employeeResult.PortalId = (int)reader["PortalId"];
                        employeeResult.RoleId = (int)reader["RoleId"];
                        employeeResult.StatusId = (int)reader["StatusId"];
                        employeeResult.Telephone = reader["Telephone"].ToString();
                        employeeResult.UpdatedOn = reader["UpdatedOn"].ToString();
                        employeeResult.Username = reader["Username"].ToString();
                    }
                }
            }

            return employeeResult;
        }

        public void UpdateEmployee(int employeeID, EmployeeModel employee)
        {
            string queryString = $"Update Employees SET " +
                $"CompanyId = {employee.CompanyId}, " +
                $"CreatedOn = '{employee.CreatedOn}', " +
                $"DeletedOn = '{employee.DeletedOn}', " +
                $"Email = '{employee.Email}', " +
                $"Fax = '{employee.Fax}', " +
                $"Name = '{employee.Name}', " +
                $"Lastlogin = '{employee.Lastlogin}', " +
                $"Password = '{employee.Password}', " +
                $"PortalId = {employee.PortalId}, " +
                $"RoleId = {employee.RoleId}, " +
                $"StatusId = {employee.StatusId}, " +
                $"Telephone = '{employee.Telephone}', " +
                $"UpdatedOn = '{employee.UpdatedOn}', " +
                $"Username = '{employee.Username}' " +
                $"WHERE EmployeeID = {employeeID}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();                
            }
        }

        public void DeleteEmployee(int employeeID)
        {
            string queryString = $"DELETE FROM Employees WHERE EmployeeID = {employeeID}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
