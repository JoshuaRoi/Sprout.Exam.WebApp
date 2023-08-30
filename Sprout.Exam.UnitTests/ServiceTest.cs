using Moq;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using Sprout.Exam.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.UnitTests
{
    public class ServiceTest
    {
        private readonly Mock<IEmployeesRepository> _employeeRepositoryMock;
        private readonly IEmployeesService _employeeService;

        public ServiceTest()
        {
            _employeeRepositoryMock = new Mock<IEmployeesRepository>();
        }

        [Fact]
        public async void GetAll()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 1", Tin = "12345" },
                new Employee { Id = 2, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 2", Tin = "12345" },
            };
            _employeeRepositoryMock.Setup(x => x.GetAll().Result).Returns(employees);
            var service = new EmployeesService(_employeeRepositoryMock.Object);

            var getAllEmployees = await service.GetAll();

            Assert.NotNull(getAllEmployees);
        }

        [Fact]
        public async void GetById()
        {
            var emp = new Employee { Id = 1, Birthdate = DateTime.Now, EmployeeTypeId = 1, FullName = "Employee 1", Tin = "12345" };
            _employeeRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(emp);
            var service = new EmployeesService(_employeeRepositoryMock.Object);

            var getEmployeeById = await service.GetById(emp.Id);

            Assert.NotNull(getEmployeeById);
            Assert.Equal(getEmployeeById, emp);
        }

        [Fact]
        public async void Create()
        {
            var employeeToCreate = new CreateEmployeeDto { TypeId = 1, Birthdate = DateTime.Now, FullName = "Employee 1", Tin = "12345" };
            _employeeRepositoryMock.Setup(x => x.Create(It.IsAny<Employee>()).Result).Returns(1);
            var service = new EmployeesService(_employeeRepositoryMock.Object);

            var createEmployee = await service.Create(employeeToCreate);

            Assert.NotEqual(-1, createEmployee);
        }

        [Fact]
        public async void Update()
        {
            var emp = new Employee { Id = 1, EmployeeTypeId = 1, Birthdate = DateTime.Now, FullName = "Employee 1", Tin = "12345" };
            var employeeToUpdate = new EditEmployeeDto { TypeId = 1, Birthdate = DateTime.Now, FullName = "Employee 1", Tin = "12345" };
            _employeeRepositoryMock.Setup(x => x.Update(It.IsAny<EditEmployeeDto>(), It.IsAny<Employee>()).Result).Returns(employeeToUpdate);
            _employeeRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(emp);
            var service = new EmployeesService(_employeeRepositoryMock.Object);

            var createEmployee = await service.Update(employeeToUpdate.Id, employeeToUpdate);

            Assert.NotNull(createEmployee);
            Assert.IsType<EditEmployeeDto>(createEmployee);
        }

        [Fact]
        public async void Delete()
        {
            var emp = new Employee { Id = 1, EmployeeTypeId = 1, Birthdate = DateTime.Now, FullName = "Employee 1", Tin = "12345" };
            _employeeRepositoryMock.Setup(x => x.Delete(It.IsAny<Employee>()).Result).Returns(true);
            _employeeRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(emp);

            var service = new EmployeesService(_employeeRepositoryMock.Object);

            var delete = await service.Delete(emp.Id);

            Assert.IsType<bool>(delete);
            Assert.True(delete);
        }

        [Fact]
        public async void Calculate ()
        {
            var emp = new Employee { Id = 1, EmployeeTypeId = 1, Birthdate = DateTime.Now, FullName = "Employee 1", Tin = "12345" };
            var calcBody = new CalculateDto { absentDays = 1, id = 1, workedDays = 0 };
            _employeeRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()).Result).Returns(emp);

            var service = new EmployeesService(_employeeRepositoryMock.Object);

            var calculate = await service.Calculate(calcBody, emp.Id);

            Assert.IsType<decimal>(calculate);
        }
    }
}
