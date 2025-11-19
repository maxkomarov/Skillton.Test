using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Models;
using Skillton.Test.Console_Net48.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillton.Test.Console_Net48.Repositories
{
    internal class EmployeeRepository : ServiceBase, IEmployeeRepository
    {
        private readonly IDataBaseService _dataBaseController;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="dataBaseController">The data base controller.</param>
        /// <param name="writeLogAction">The write log action.</param>
        /// <param name="sampleSource">The sample source.</param>
        /// <exception cref="System.ArgumentNullException">
        /// dataBaseController
        /// or
        /// sampleSource
        /// </exception>
        public EmployeeRepository(
            IDataBaseService dataBaseController,
            Action<string> writeLogAction) 
        {
            _dataBaseController = dataBaseController
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, nameof(dataBaseController));

            WriteLogAction = writeLogAction;
        }

        #region IEmployeeRepository имплементация

        /// <summary>
        /// Удалить запись в Employee в БД
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int DeleteEmployee(int id)
        {
            IDictionary<string, object> cmdParams = new Dictionary<string, object>
            {
                { "EmployeeId", id }
            };

            int res = _dataBaseController.ExecuteDelete(cmdParams);

            return res;
        }

        /// <summary>
        /// Получить записи из таблицы Employees
        /// </summary>
        /// <returns></returns>
        public IList<IEmployee> GetEmployees()
        {
            IList<IEmployee> res = new List<IEmployee>();
            IList<IDictionary<string, object>> data = _dataBaseController.ExecuteSelect();
            data.ToList().ForEach(e => res.Add(new Employee(e)));
            return res;
        }

        /// <summary>
        /// Загрузка в таблицу Employees тестовых записей
        /// </summary>
        public void AddEmployeeRange(IList<IEmployee> employees)
        {
            try
            {
                employees.ToList().ForEach(
                        employee => AddEmployee(employee));
            }
            catch (Exception) //Тут срабатывает констрэйн на уникальность имен и добавления дублей нет
            //Это не самый айс, но... типа техдолг )
            {
                WriteLog("SQL CE: Добавление тестовых записей не требуется - они уже существуют");
            }
        }

        /// <summary>
        /// Сохранение в БД записи об Employee
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public int AddEmployee(IEmployee employee)
        {
            IDictionary<string, object> cmdParams = new Dictionary<string, object>
            {
                { "EmployeeId", employee.EmployeeId },
                { "FirstName", employee.FirstName },
                { "LastName", employee.LastName },
                { "Email", employee.Email },
                { "DateOfBirth", employee.DateOfBirth },
                { "Salary", employee.Salary }
            };

            int res = _dataBaseController.ExecuteInsert(cmdParams);

            return res;
        }

        /// <summary>
        /// Получить запись Employee из БД
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Некорректный параметр [{id}] для извлечения записи из БД</exception>
        public IEmployee GetEmployee(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Некорректный параметр [{id}] для извлечения записи из БД");

            IDictionary<string, object> cmdParams = new Dictionary<string, object>()
            {
                { "EmployeeID", id}
            };

            IList<IDictionary<string, object>> data = _dataBaseController.ExecuteSelect(cmdParams);

            if (data.Count > 0) 
                return new Employee(data[0]);
            else
                return null;
        }

        /// <summary>
        /// Обновить запись Employee из БД
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>        
        public int UpdateEmployee(IEmployee employee)
        {
            IDictionary<string, object> cmdParams = new Dictionary<string, object>
            {
                { "EmployeeId", employee.EmployeeId },
                { "FirstName", employee.FirstName },
                { "LastName", employee.LastName },
                { "Email", employee.Email },
                { "DateOfBirth", employee.DateOfBirth },
                { "Salary", employee.Salary }
            };

            int res = _dataBaseController.ExecuteUpdate(cmdParams);

            return res;
        }

        /// <summary>
        /// Получить кол-во сотрудников, зарплата (Salary) которых
        /// выше средней арифметической зарплаты по всем сотрудникам.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Ошибка приведения значения к типу decimal в GetAboveAvgSalaryCount
        /// or
        /// Ошибка приведения значения к типу int в GetAboveAvgSalaryCount
        /// </exception>
        public Tuple<int, decimal> GetAboveAvgSalaryCount()
        {
            //Распилено на две каманды/запроса, поскольку SQL CE не распознает вложенные запросы 
            decimal avg = _dataBaseController.GetAvgSalary();            
            int res = _dataBaseController.GetCountWithSalaryAboveAverage(avg);

            return new Tuple<int, decimal>(res, avg);            
        }


        public void SaveChanges(IEmployee employee)
        {
            try
            {
                if (employee.EmployeeId == 0)
                {
                    if (AddEmployee(employee) > 0)
                        Console.WriteLine($"Запись успешно создана в БД!");
                    else
                        Console.WriteLine($"Запись в БД не добавлена! Инспектируйте файл лога на предмет ошибок!");
                }
                else
                {
                    if (UpdateEmployee(employee) > 0)
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

        #endregion        
    }
}
