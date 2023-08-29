using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public interface IEmployeesService
    {
        public Task<IEnumerable<Employee>> GetAll();
        public Task<Employee> GetById(int id);
        public Task<int> Create(CreateEmployeeDto employee);
        public Task<EditEmployeeDto> Update(int id, EditEmployeeDto employee);
        public Task<bool> Delete(int id);
        public Task<decimal> Calculate(CalculateDto body, int id);
    }
}
