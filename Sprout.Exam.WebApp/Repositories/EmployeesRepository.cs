using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Controllers;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<EmployeesRepository> logger;

        public EmployeesRepository(ApplicationDbContext _dbContext, ILogger<EmployeesRepository> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
        }
        public async Task<int> Create(Employee employee)
        {
            try
            {
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Employee {employee.FullName} created");
                return employee.Id;
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                return -1;
            }
        }

        public async Task<bool> Delete(Employee employee)
        {
            try
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Employee with Id={employee.Id} deleted");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                return false;
            }
        }

        public async Task<Employee> GetById(int id)
        {
            return await dbContext.Employees.FindAsync(id);
        }

        public async Task<EditEmployeeDto> Update(EditEmployeeDto employeeUpdate, Employee employee)
        {
            try
            {
                employee.Birthdate = employeeUpdate.Birthdate;
                employee.Tin = employeeUpdate.Tin;
                employee.EmployeeTypeId = employeeUpdate.TypeId;
                employee.FullName = employeeUpdate.FullName;
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Employee with Id={employee.Id} updated");
                return employeeUpdate;
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                employeeUpdate.Id = -1;
                return employeeUpdate;
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            logger.LogInformation("Getting all Employees");
            return await dbContext.Employees.ToListAsync();
        }
    }
}
