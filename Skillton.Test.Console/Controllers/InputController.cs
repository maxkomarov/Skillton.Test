using Newtonsoft.Json.Linq;
using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Controllers;
using Skillton.Test.Console_Net48.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Skillton.Test.Console_Net48
{
    internal class InputController : ControllerBase, IInputController
    {
        private readonly IDataController _dataController;
        private readonly IValidationController _validationController;
        public InputController(
            IValidationController validationController,
            IDataController dataController,
            Action<string> writeLogAction) 
        {
            _validationController = validationController 
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, nameof(validationController));

            _dataController = dataController
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, nameof(dataController));

            WriteLogAction = writeLogAction;
        }

        #region IValidationController имплементация

        public IValidationController ValidationController { get; private set; }

        public void Run()
        {
            WriteLog("Рабочий процесс запущен...");

            while (RunRootSelector()) ;

            WriteLog("Рабочий процесс завершен пользователем...");
        }

        #endregion

        private bool RunRootSelector()
        {
            //Console.Clear(); 
            Console.WriteLine();
            Console.WriteLine("--- Главное меню---");
            Console.WriteLine();
            Console.WriteLine("\t1. Добавить нового сотрудника");
            Console.WriteLine("\t2. Просмотреть всех сотрудников");
            Console.WriteLine("\t3. Обновить информацию о сотруднике");
            Console.WriteLine("\t4. Удалить сотрудника");
            Console.WriteLine("\t5. Вывести кол-во сотрудников, зарплата (Salary) которых выше средней арифметической зарплаты по всем сотрудникам");
            Console.WriteLine("\t6. Создать записи для тестов");
            Console.WriteLine("\t9. Выйти из приложения");
            Console.WriteLine();
            Console.Write("Введите команду: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNew();
                    return true; 
                case "2":
                    ShowAll();
                    return true;
                case "3":
                    UpdateEmployee();
                    return true;
                case "4":
                    DeleteEmployee();
                    return true;
                case "5":
                    GetAboveAvgSalaryCount();
                    return true;
                case "6":
                    AddSamples();
                    return true;
                case "9":
                    return false; 
                default:
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу и попробуйте снова.");
                    Console.ReadKey(); 
                    return true; 
            }
        }

        /// <summary>
        /// 1. Добавить нового сотрудника
        /// </summary>
        void AddNew()
        {
            Console.WriteLine();
            Console.WriteLine("ДОБАВЛЕНИЕ ЗАПИСИ!");
            Console.WriteLine();

            IEmployee employee = new Employee();
            InputEmployeeController input = new InputEmployeeController(_validationController, _dataController);
            input.Run(employee);            
        }

        /// <summary>
        /// 2. Просмотреть всех сотрудников
        /// </summary>
        void ShowAll()
        {
            IList<IEmployee> data = _dataController.GetEmployees();
            Console.WriteLine();
            Console.WriteLine("\tЗаписи в таблице сотрудников (Employees):");
            Console.WriteLine();
            Console.WriteLine(Employee.GetTableHeader());
            data.ToList().ForEach(
                employee => Console.WriteLine(
                    employee.ToConsoleString('|', true)));
            Console.WriteLine();
        }

        void UpdateEmployee()
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
                    IEmployee employee = _dataController.GetEmployee(id);
                    if (employee == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"ОШИБКА! БД вернула null для записи с ID={id}. Такой записи не существует!");
                        return;
                    }

                    InputEmployeeController inputEmployeeController = new InputEmployeeController(_validationController, _dataController);
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
        void DeleteEmployee()
        {
            Console.WriteLine();
            Console.WriteLine("\tУДАЛЕНИЕ ЗАПИСИ!");
            Console.WriteLine();
            Console.Write("\tВведите идентификатор записи (EmployeeID), которую необходимо удалить: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                Console.WriteLine();
                if (_dataController.DeleteEmployee(id) > 0)
                    Console.WriteLine($"Запись с ИД={id} удалена из БД успешно!");
                else
                    Console.WriteLine($"Записи с ИД={id} в БД не существует. Изменений в БД не зафиксировано!");
            }
            else
                Console.WriteLine($"Ошибка ввода: введеная строка '{input}' не является целым числом!");
            Console.WriteLine();
        }

        void GetAboveAvgSalaryCount()
        {            
            Tuple<int, decimal> res = _dataController.GetAboveAvgSalaryCount();
            Console.WriteLine();
            Console.WriteLine($"-> Количество сотрудников, имеющих з/п выше средней ( {res.Item2:C2} ): {res.Item1}");
            Console.WriteLine();
        }

        /// <summary>
        /// 6. Создать записи для тестов
        /// </summary>
        void AddSamples()
        {
            Console.WriteLine();
            _dataController.AddEmployeeRange(Employee.GetSamples());
            Console.WriteLine("Тестовые записи добавлены в БД!");
            Console.WriteLine();
        }        
    }
}
