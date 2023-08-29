using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository employeesRepository;
        public EmployeesService(IEmployeesRepository _employeesRepository)
        {
            employeesRepository = _employeesRepository;
        }
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeesRepository.GetAll();
        }
        public async Task<Employee> GetById(int id)
        {
            return await employeesRepository.GetById(id);
        }
        public async Task<int> Create(CreateEmployeeDto employee)
        {
            Employee newEmployee = new(employee);
            return await employeesRepository.Create(newEmployee);
        }
        public async Task<EditEmployeeDto> Update(int id, EditEmployeeDto employeeUpdate)
        {
            var employee = await GetById(id);
            if (employeeUpdate == null) return employeeUpdate;
            if(employee!= null) return await employeesRepository.Update(employeeUpdate, employee);
            return employeeUpdate;
        }
        public async Task<bool> Delete(int id)
        {
            var employeeToDelete = await GetById(id);
            if (employeeToDelete == null) return false;
            return await employeesRepository.Delete(employeeToDelete);
        }
        public async Task<decimal> Calculate(CalculateDto body, int id)
        {
            var result = await employeesRepository.GetById(id);
            if (result != null)
            {
                var type = (EmployeeTypeEnum)result.EmployeeTypeId;
                if (type == EmployeeTypeEnum.Regular)
                {
                    decimal EMPLOYEE_SALARY = 20000;
                    decimal TAX = 0.12M;
                    var salary = (EMPLOYEE_SALARY - ((EMPLOYEE_SALARY / 22) * body.absentDays) - (EMPLOYEE_SALARY * TAX));
                    if (body.absentDays < 0 || body.absentDays >= 22) return 0;
                    return salary;
                }
                else if (type == EmployeeTypeEnum.Contractual)
                {
                    var EMPLOYEE_SALARY = 500;
                    var salary = EMPLOYEE_SALARY * body.workedDays;
                    if (body.workedDays < 0) return 0;
                    return salary;
                }
            }  
            return -1M;
        }

    }
}
