﻿using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;

#nullable disable

namespace Sprout.Exam.WebApp.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public Employee()
        {
            
        }
        public Employee(CreateEmployeeDto employee)
        {
            Birthdate = employee.Birthdate;
            EmployeeTypeId = employee.TypeId;
            FullName = employee.FullName;
            Tin = employee.Tin;
        }
    }
}
