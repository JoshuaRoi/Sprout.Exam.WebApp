using Microsoft.Extensions.Logging;
using Moq;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sprout.Exam.UnitTests
{
    public class RepositoryTest
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployeesRepository _employeesRepository;

        public RepositoryTest()
        {
            var logger = new Mock<ILogger<EmployeesRepository>>().Object;
            _context = ContextGenerator.Generate();
            _employeesRepository = new EmployeesRepository(_context, logger);
        }

        [Fact]
        public async void AddEmployee()
        {
            var newEmp = new Employee { Id = 1, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 1", Tin = "12345" };

            var result = await _employeesRepository.Create(newEmp);

            Assert.NotEqual(-1, result);
            Assert.Equal(newEmp.Id, result);
        }

        [Fact]
        public async void GetById()
        {
            var newEmp = new Employee { Id = 1, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 1", Tin = "12345" };
            await _employeesRepository.Create(newEmp);

            var result = await _employeesRepository.GetById(newEmp.Id);

            Assert.NotNull(result);
            Assert.Equal(newEmp.Id, result.Id);
        }

        [Fact]
        public async void GetAll()
        {
            var newEmps = new List<Employee>
            {
                new Employee  { Id = 2, Birthdate = DateTime.Now, EmployeeTypeId = 2, FullName = "Employee 2", Tin = "24680" },
                new Employee  { Id = 3, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 3", Tin = "13131" }
            };
            newEmps.ForEach(async (x) => await _employeesRepository.Create(x));

            var result = await _employeesRepository.GetAll();

            Assert.NotEmpty(result);
            Assert.Equal(newEmps[0], result.ToList()[0]);
            Assert.Equal(newEmps[1], result.ToList()[1]);
        }

        [Fact]
        public async void DeleteEmployee()
        {
            var newEmp = new Employee { Id = 1, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 1", Tin = "12345" };
            await _employeesRepository.Create(newEmp);

            var result = await _employeesRepository.Delete(newEmp);

            Assert.True(result);
        }

        [Fact]
        public async void UpdateEmployee()
        {
            var newEmp = new Employee { Id = 1, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 1", Tin = "12345" };
            await _employeesRepository.Create(newEmp);
            var updateEmp = new EditEmployeeDto { Id = 1, Birthdate = DateTime.Now, TypeId = 1, FullName = "UpdatedEmployee 1", Tin = "12345" };

            await _employeesRepository.Update(updateEmp, newEmp);
            var getEmployee = await _employeesRepository.GetById(newEmp.Id);

            Assert.NotEqual("Employee 1", getEmployee.FullName);
        }

    }
}
