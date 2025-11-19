using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Models;
using System;

namespace Skillton.Test.Console_Net48.Controllers
{
    internal class EmployeeBuilder
    {
        private readonly IValidationController _validationController;

        public EmployeeBuilder(IValidationController validationController) 
        {
            _validationController = validationController;
        }
        public Employee CreateNew()
        {
            Employee employee = new Employee();
            return employee;
        }

        public Employee CreateNew(
            int employeeId,
            string firstName,
            string lastName,
            string email,
            DateTime dateOfBirth,
            decimal salary)
        {
            _validationController.CheckFirstName(firstName);
            _validationController.CheckLastName(lastName);
            _validationController.CheckNameMaskValidity(firstName, "Имя");
            _validationController.CheckNameMaskValidity(lastName, "Фамилия");
            _validationController.CheckEmailMaskValidity(email, "Email");
            _validationController.CheckDateOfBirthVilidity(dateOfBirth);
            _validationController.CheckSalaryValidity(salary);


            Employee employee = new Employee()
            {
                EmployeeId = employeeId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DateOfBirth = dateOfBirth,
                Salary = salary
            };           

            return employee;
        }

        
    }
}
