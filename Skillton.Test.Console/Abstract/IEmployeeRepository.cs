using System;
using System.Collections.Generic;

namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс взаимодействия с источником данных, 
    /// изолированный (ну, почти) от конкретной реализации БД (условный репозиторий)
    /// </summary>
    internal interface IEmployeeRepository
    {
        /// <summary>
        /// Создать запись в Employee в БД
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        int AddEmployee(IEmployee employee);

        /// <summary>
        /// Удалить запись в Employee в БД
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        int DeleteEmployee(int id);

        /// <summary>
        /// Получить все записи Employee из БД
        /// </summary>
        /// <returns></returns>
        IList<IEmployee> GetEmployees();

        /// <summary>
        /// Получить запись Employee из БД
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEmployee GetEmployee(int id);

        /// <summary>
        /// Обновить запись Employee из БД
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        int UpdateEmployee(IEmployee employee);

        /// <summary>
        /// Загрузка в таблицу Employees массива записей
        /// </summary>
        void AddEmployeeRange(IList<IEmployee> employees);

        /// <summary>
        /// Получить кол-во сотрудников, зарплата (Salary) которых 
        /// выше средней арифметической зарплаты по всем сотрудникам.
        /// </summary>
        /// <returns>int - кол-во, получающее выше средней, decimal - значение средней з/п</returns>
        Tuple<int, decimal> GetAboveAvgSalaryCount();

        /// <summary>
        /// Сохранить изменения в сущности
        /// </summary>
        /// <param name="employee">The employee.</param>
        void SaveChanges(IEmployee employee);
    }
}
