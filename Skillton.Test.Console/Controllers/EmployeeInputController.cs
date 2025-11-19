using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Presenters;
using System;

namespace Skillton.Test.Console_Net48.Controllers
{
    /// <summary>
    /// Контроллер редактора записи
    /// </summary>
    internal class EmployeeInputController
    {
        private EmployeePresenter _employeePresenter;
        private readonly IValidationService _validationService;
        private readonly IEmployeeRepository _repository;
        public EmployeeInputController(IValidationService validationController, IEmployeeRepository dataController) 
        {
            _validationService = validationController
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, 
                    nameof(validationController));

            _repository = dataController
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED,
                    nameof(dataController));
        }

        public void Run(IEmployee employee)
        {
            _employeePresenter = new EmployeePresenter(_validationService,employee);

            while (RunSelector());            
        }

        bool RunSelector()
        {
            Console.WriteLine();
            Console.WriteLine("--- Создание/редактирование полей записи---");
            Console.WriteLine();
            Console.WriteLine($"\t1. Изменить фамилию. Текущее значение: '{_employeePresenter.Employee.LastName}'");
            Console.WriteLine($"\t2. Изменить имя.Текущее значение: '{_employeePresenter.Employee.FirstName}'");
            Console.WriteLine($"\t3. Изменить E-mail. Текущее значение: '{_employeePresenter.Employee.Email}'");
            Console.WriteLine($"\t4. Изменить дату рождения. Текущее значение: '{_employeePresenter.Employee.DateOfBirth.ToShortDateString()}'");
            Console.WriteLine($"\t5. Изменить размер зарплаты. Текущее значение: '{_employeePresenter.Employee.Salary}'");
            Console.WriteLine("\t8. Сохранить изменения и выйти в главное меню");
            Console.WriteLine("\t9. Отменить изменения и выйти в главное меню");
            Console.WriteLine();
            Console.Write("Введите команду: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _employeePresenter.ChangeLastName("Фамилия");
                    return true;
                case "2":
                    _employeePresenter.ChangeFirstName("Имя");
                    return true;
                case "3":
                    _employeePresenter.ChangeEmail("Email");
                    return true;
                case "4":
                    _employeePresenter.ChangeDateOfBirth("Дата рождения");
                    return true;
                case "5":
                    _employeePresenter.ChangeSalary("Зарплата"); ;
                    return true;
                case "8":
                    {
                        _validationService.Validate(_employeePresenter.Employee);
                        _repository.SaveChanges(_employeePresenter.Employee);
                        return false;
                    }
                case "9":
                    return false;
                default:
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу и попробуйте снова.");
                    Console.ReadKey();
                    return true;
            }
        }
    }
}
