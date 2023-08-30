using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public interface IEmployeesRepository
    {
        public Task<IEnumerable<Employee>> GetAll();
        public Task<Employee> GetById(int id);
        public Task<int> Create(Employee employee);
        public Task<EditEmployeeDto> Update( EditEmployeeDto employeeUpdate,Employee employee);
        public Task<bool> Delete(Employee employee);
    }
}
