using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Controllers;
using Skillton.Test.Console_Net48.Helpers;
using Skillton.Test.Console_Net48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillton.Test.Console_Net48.Presenters
{
    internal class RootPresenter
    {
        private readonly IValidationService _validationService;
        private readonly IEmployeeRepository _repository;

        public RootPresenter(
            IValidationService validationService,
            IEmployeeRepository repository)
        {
            if (validationService == null)
                throw new ArgumentNullException(
                    Constants.NULLABLE_ARGUMENT_NOT_ALLOWED,
                    nameof(validationService));

            if (repository == null)
                throw new ArgumentNullException(
                    Constants.NULLABLE_ARGUMENT_NOT_ALLOWED,
                    nameof(repository));

            _validationService = validationService;
            _repository = repository;
        }


        /// <summary>
        /// 1. Добавить нового сотрудника
        /// </summary>
        public void AddNew()
        {
            Console.WriteLine();
            Console.WriteLine("ДОБАВЛЕНИЕ ЗАПИСИ!");
            Console.WriteLine();

            IEmployee employee = new Employee();
            EmployeeInputController input =
                new EmployeeInputController(_validationService, _repository);
            input.Run(employee);
        }

        /// <summary>
        /// 2. Просмотреть всех сотрудников
        /// </summary>
        public void ShowAll()
        {
            IList<IEmployee> data = _repository.GetEmployees();
            Console.WriteLine();
            Console.WriteLine("\tЗаписи в таблице сотрудников (Employees):");
            Console.WriteLine();
            Console.WriteLine(EmployeeFormatter.GetEmployeeTableHeader());
            data.ToList().ForEach(
                employee => Console.WriteLine(
                    EmployeeFormatter.ToRowString(employee, '|', true)));
            Console.WriteLine();
        }

        public void UpdateEmployee()
        {
            Console.WriteLine();
            Console.WriteLine("\tИЗМЕНЕНИЕ ЗАПИСИ!");
            Console.WriteLine();
            Console.Write("\tВведите идентификатор записи (EmployeeID), которую необходимо редактировать: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                Console.WriteLine();
                {
                    IEmployee employee = _repository.GetEmployee(id);
                    if (employee == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"ОШИБКА! БД вернула null для записи с ID={id}. Такой записи не существует!");
                        return;
                    }

                    EmployeeInputController inputEmployeeController = new EmployeeInputController(_validationService, _repository);
                    inputEmployeeController.Run(employee);
                }
            }
            else
                Console.WriteLine($"Ошибка ввода: введеная строка '{input}' не является целым числом!");
            Console.WriteLine();
        }

        /// <summary>
        /// 4. Удалить сотрудника
        /// </summary>
        public void DeleteEmployee()
        {
            Console.WriteLine();
            Console.WriteLine("\tУДАЛЕНИЕ ЗАПИСИ!");
            Console.WriteLine();
            Console.Write("\tВведите идентификатор записи (EmployeeID), которую необходимо удалить: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                Console.WriteLine();
                if (_repository.DeleteEmployee(id) > 0)
                    Console.WriteLine($"Запись с ИД={id} удалена из БД успешно!");
                else
                    Console.WriteLine($"Записи с ИД={id} в БД не существует. Изменений в БД не зафиксировано!");
            }
            else
                Console.WriteLine($"Ошибка ввода: введеная строка '{input}' не является целым числом!");
            Console.WriteLine();
        }

        public void GetAboveAvgSalaryCount()
        {
            Tuple<int, decimal> res = _repository.GetAboveAvgSalaryCount();
            Console.WriteLine();
            Console.WriteLine($"-> Количество сотрудников, имеющих з/п выше средней ( {res.Item2:C2} ): {res.Item1}");
            Console.WriteLine();
        }

        /// <summary>
        /// 6. Создать записи для тестов
        /// </summary>
        public void AddSamples()
        {
            Console.WriteLine();
            _repository.AddEmployeeRange(Employee.GetSamples());
            Console.WriteLine("Тестовые записи добавлены в БД!");
            Console.WriteLine();
        }
    }
}
