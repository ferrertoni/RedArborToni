using RedArborToni.Models;
using System.Collections.Generic;

namespace RedArborToni.DataAccess
{
    public class EmployeeRepo : Repository<EmployeeModel>
    {        
        private DbContext _context;

        public EmployeeRepo(DbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeModel> GetEmployees()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Employees";

                return this.ToList(command);
            }
        }

        public IEnumerable<EmployeeModel> GetEmployeeByID(int employeeID)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = $"SELECT TOP (1) * FROM Employees WHERE EmployeeID = {employeeID}";

                return this.ToList(command);
            }
        }

        public IEnumerable<EmployeeModel> CreateEmployee(EmployeeModel employee)
        {           
            using (var command = _context.CreateCommand())
            {
                command.CommandText = $"INSERT INTO Employees OUTPUT INSERTED.* " +
                    $"VALUES ({employee.CompanyId},'{employee.CreatedOn}','{employee.DeletedOn}','{employee.Email}','{employee.Fax}','{employee.Name}','{employee.Lastlogin}'," +
                    $"'{employee.Password}',{employee.PortalId},{employee.RoleId},{employee.StatusId},'{employee.Telephone}','{employee.UpdatedOn}','{employee.Username}')";

                return this.ToList(command);
            }
        }

        public void UpdateEmployee(int employeeID, EmployeeModel employee)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = $"Update Employees SET " +
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

                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int employeeID)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = $"DELETE FROM Employees WHERE EmployeeID = {employeeID}";

                command.ExecuteNonQuery();
            }
        }
    }
}