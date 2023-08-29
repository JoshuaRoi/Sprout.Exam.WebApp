using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Repositories;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.WebApp.Models;
using static Humanizer.In;
using static Humanizer.On;
using System.Buffers.Text;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using Sprout.Exam.WebApp.Services;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService employeesService;
        public EmployeesController(IEmployeesService _employeesService)
        {
            employeesService = _employeesService;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await employeesService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await employeesService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EditEmployeeDto input, [FromRoute] int id)
        {
            var item = await employeesService.Update(id, input);
            if (item.Id == -1) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeDto input)
        {
            int id = await employeesService.Create(input);
            return Created($"/api/employees/{id}", id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await employeesService.Delete(id);
            if (!result) return NotFound();
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        ///

        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculateDto body, [FromRoute] int id)
        {
            var result = await employeesService.Calculate(body, id);
            if (result == -1M) return NotFound("Employee Type not found");
            return Ok(result);
        }
        /*
                • Your task is to create a web app that computes the salary of an employee.
        There are two types of employee:
        a.Regular employee
        ▪ Per month salary
        ▪ Absences will be deducted to the monthly salary (1 day deduction = monthly
        salary / 22)
        ▪ Has 12% tax deduction
        b.Contractual employee
        ▪ Per day salary
        ▪ Salary is computed daily, and based on the number of days the employee
        reports to work
        ▪ Has no tax deduction

        • Your web app should be able to create a new employee and save it to the Database.Inputs are:

        1. Name
        2. Birthdate
        3. TIN
        4. Employee Type
        • Your web app should also be able to delete and edit the employee details and
        make this reflect in the Database.
        • The app should be able to compute the pay once “Calculate” is clicked.
        • If it's regular, you should be able to input the number of absences in days.
        • If it's contractual, you should be able to input the number of worked days.
        • Absent and worked days can have decimal places.
        • There should be a calculate button or whatever that will show the computed
        salary.
        • The answer should be rounded to 2 decimal places.
        • The answer should always show 2 decimal places (ex. 10,000.00).
        Sample computation:
        a.Regular Employee
        • Has 20,000 basic monthly salary
        • 1 day absent
        • 12% tax
        • = 20,000 - (20,000 / 22)absent - (20,000 * 0.12)tax
        • = 16,690.91
        b.Contractual employee
        • Has 500 per day rate
        • Reported to work for 15.5 days
        • = 500 * 15.5
        • = 7,750.00*/
    }
}
