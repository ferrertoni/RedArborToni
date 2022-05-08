using RedArborToni.DataAccess;
using RedArborToni.Models;
using System.Linq;

namespace RedArborToni.Services
{
    public class EmployeeService
    {
        private EmployeeRepo _employeeRepo;
        private IConnectionFactory _connectionFactory;
        private DbContext _context;

        public EmployeeService()
        {            
            _connectionFactory = ConnectionHelperService.GetConnection();
            _context = new DbContext(_connectionFactory);
            _employeeRepo = new EmployeeRepo(_context);
        }

        public EmployeeModel[] Get() => _employeeRepo.GetEmployees().ToArray();

        public EmployeeModel GetByID(int employeeID) => _employeeRepo.GetEmployeeByID(employeeID).First();

        public EmployeeModel Create(EmployeeModel employee) => _employeeRepo.CreateEmployee(employee).First();

        public void Update(int employeeID, EmployeeModel employee) => _employeeRepo.UpdateEmployee(employeeID, employee);

        public void Delete(int employeeID) => _employeeRepo.DeleteEmployee(employeeID);
    }
}
