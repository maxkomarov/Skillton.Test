using Skillton.Test.Console_Net48.Abstract;
using System;

namespace Skillton.Test.Console_Net48.Presenters
{
    internal class EmployeePresenter
    {
        private readonly IValidationService _validationService;
        private IEmployee _employee;

        public EmployeePresenter(
            IValidationService validationService,
            IEmployee employee)
        {
            if (validationService == null)
                throw new ArgumentNullException(
                    Constants.NULLABLE_ARGUMENT_NOT_ALLOWED,
                    nameof(validationService));

            if (employee == null)
                throw new ArgumentNullException(
                    Constants.NULLABLE_ARGUMENT_NOT_ALLOWED,
                    nameof(employee));

            _validationService = validationService;
            _employee = employee;
        }

        public IEmployee Employee { get => _employee; }

        public void ChangeFirstName(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            try
            {
                _validationService.CheckFirstName(input);
                _validationService.CheckNameMaskValidity(input, "FirstName");
                _employee.FirstName = input;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                Console.WriteLine();
            }
        }

        public void ChangeLastName(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            try
            {
                _validationService.CheckLastName(input);
                _validationService.CheckNameMaskValidity(input, "LastName");
                _employee.LastName = input;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                Console.WriteLine();
            }
        }

        public void ChangeEmail(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            try
            {
                _validationService.CheckEmailMaskValidity(input, "Email");
                _employee.Email = input;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                Console.WriteLine();
            }
        }

        public void ChangeDateOfBirth(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            if (DateTime.TryParse(input, out DateTime date))
            {
                try
                {
                    _validationService.CheckDateOfBirthVilidity(date);
                    _employee.DateOfBirth = date;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"ОШИБКА ВВОДА! Попробуйте еще раз. Формат даты:dd.mm.yyyy");
                Console.WriteLine();
            }
        }

        public void ChangeSalary(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal salary))
            {

                try
                {
                    _validationService.CheckSalaryValidity(salary);
                    _employee.Salary = salary;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"ОШИБКА ВВОДА! Попробуйте еще раз. Формат десятичного числа: xxxxxx.xx");
                Console.WriteLine();
            }
        }        
    }
}
