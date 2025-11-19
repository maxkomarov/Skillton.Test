using Skillton.Test.Console_Net48.Abstract;
using System;

namespace Skillton.Test.Console_Net48.Controllers
{
    /// <summary>
    /// Контроллер редактора записи
    /// </summary>
    internal class InputEmployeeController
    {
        IEmployee _employee;
        private readonly IValidationController _validationController;
        private readonly IDataController _dataController;
        public InputEmployeeController(IValidationController validationController, IDataController dataController) 
        {
            _validationController = validationController
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, 
                    nameof(validationController));

            _dataController = dataController
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED,
                    nameof(dataController));
        }

        public IEmployee Run(IEmployee employee)
        {
            _employee = employee;
            while (RunSelector());
            return _employee;
        }

        bool RunSelector()
        {
            Console.WriteLine();
            Console.WriteLine("--- Создание/редактирование полей записи---");
            Console.WriteLine();
            Console.WriteLine($"\t1. Изменить фамилию. Текущее значение: '{_employee.LastName}'");
            Console.WriteLine($"\t2. Изменить имя.Текущее значение: '{_employee.FirstName}'");
            Console.WriteLine($"\t3. Изменить E-mail. Текущее значение: '{_employee.Email}'");
            Console.WriteLine($"\t4. Изменить дату рождения. Текущее значение: '{_employee.DateOfBirth.ToShortDateString()}'");
            Console.WriteLine($"\t5. Изменить размер зарплаты. Текущее значение: '{_employee.Salary}'");
            Console.WriteLine("\t8. Сохранить изменения и выйти в главное меню");
            Console.WriteLine("\t9. Отменить изменения и выйти в главное меню");
            Console.WriteLine();
            Console.Write("Введите команду: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ChangeLastName("Фамилия");
                    return true;
                case "2":
                    ChangeFirstName("Имя");
                    return true;
                case "3":
                    ChangeEmail("Email");
                    return true;
                case "4":
                    ChangeDateOfBirth("Дата рождения");
                    return true;
                case "5":
                    ChangeSalary("Зарплата"); ;
                    return true;
                case "8":
                    SaveChanges();
                    return false;
                case "9":
                    return false;
                default:
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу и попробуйте снова.");
                    Console.ReadKey();
                    return true;
            }
        }

        void ChangeFirstName(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            try
            {
                _validationController.CheckFirstName(input);
                _validationController.CheckNameMaskValidity(input, "FirstName");
                _employee.FirstName = input;
            }
            catch (Exception e)
            { 
                Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                Console.WriteLine();
            }
        }

        void ChangeLastName(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            try
            {
                _validationController.CheckLastName(input);
                _validationController.CheckNameMaskValidity(input, "LastName");
                _employee.LastName = input;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                Console.WriteLine();
            }
        }

        void ChangeEmail(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            try
            {
                _validationController.CheckEmailMaskValidity(input, "Email");
                _employee.Email = input;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ОШИБКА! Попробуйте еще раз. Подробно:{e.Message}");
                Console.WriteLine();
            }
        }

        void ChangeDateOfBirth(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            if (DateTime.TryParse(input, out DateTime date))
            {
                try
                {
                    _validationController.CheckDateOfBirthVilidity(date);
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

        void ChangeSalary(string fieldName)
        {
            Console.WriteLine();
            Console.Write($"Введите новое значение поля [{fieldName}]: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal salary))
            {

                try
                {
                    _validationController.CheckSalaryValidity(salary);
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

        void SaveChanges()
        {
            try
            {
                _validationController.Validate(_employee);

                if (_employee.EmployeeId == 0)
                {
                    if (_dataController.AddEmployee(_employee) > 0)
                        Console.WriteLine($"Запись успешно создана в БД!");
                    else
                        Console.WriteLine($"Запись в БД не добавлена! Инспектируйте файл лога на предмет ошибок!");
                }
                else
                {
                    if (_dataController.UpdateEmployee(_employee) > 0)
                        Console.WriteLine($"Запись успешно изменена в БД!");
                    else
                        Console.WriteLine($"Запись в БД не изменена! Инспектируйте файл лога на предмет ошибок!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\r\n{e.Message}\r\n{e.InnerException?.Message}\r\n"); 
            }
        }
    }
}
