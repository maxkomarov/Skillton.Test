using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Presenters;
using System;

namespace Skillton.Test.Console_Net48
{
    internal class RootInputController : ServiceBase, IInputController
    {
        private readonly IEmployeeRepository _repository;
        private readonly IValidationService _validationService;
        private readonly RootPresenter _rootPresenter;

        public RootInputController(
            IValidationService validationService,
            IEmployeeRepository dataController,
            Action<string> writeLogAction) 
        {
            _validationService = validationService
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, nameof(_validationService));

            _repository = dataController
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, nameof(dataController));

            WriteLogAction = writeLogAction;

            _rootPresenter = new RootPresenter(
                _validationService,
                _repository);
        }

        #region IValidationController имплементация

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
                    _rootPresenter.AddNew();
                    return true; 
                case "2":
                    _rootPresenter.ShowAll();
                    return true;
                case "3":
                    _rootPresenter.UpdateEmployee();
                    return true;
                case "4":
                    _rootPresenter.DeleteEmployee();
                    return true;
                case "5":
                    _rootPresenter.GetAboveAvgSalaryCount();
                    return true;
                case "6":
                    _rootPresenter.AddSamples();
                    return true;
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
