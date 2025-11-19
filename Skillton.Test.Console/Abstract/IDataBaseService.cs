using System.Collections.Generic;

namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс контроллера БД
    /// </summary>
    internal interface IDataBaseService
    {
        /// <summary>
        /// Создать БД, если не существует
        /// </summary>
        void EnsureCreated();           

        /// <summary>
        /// Возвращает массив пар (ключ, объект), представляющих записи из запроса
        /// </summary>
        /// <param name="cmdParams">The command parameters.</param>
        IList<IDictionary<string, object>> ExecuteSelect(IDictionary<string, object> cmdParams = null);

        /// <summary>
        /// Executes the update.
        /// </summary>
        /// <param name="cmdParams">The command parameters.</param>
        /// <returns></returns>
        int ExecuteUpdate(IDictionary<string, object> cmdParams);

        /// <summary>
        /// Executes the insert.
        /// </summary>
        /// <param name="cmdParams">The command parameters.</param>
        /// <returns></returns>
        int ExecuteInsert(IDictionary<string, object> cmdParams);

        /// <summary>
        /// Удаляет запись из БД
        /// </summary>
        /// <param name="cmdParams">The command parameters.</param>
        /// <returns></returns>
        int ExecuteDelete(IDictionary<string, object> cmdParams);

        /// <summary>
        /// Возвращает среднюю з/п по всем записям
        /// </summary>
        /// <returns></returns>
        decimal GetAvgSalary();

        /// <summary>
        /// Взозвращает кол-во записей с з/п выше средней
        /// </summary>
        /// <param name="avgSalary">The average salary.</param>
        /// <returns></returns>
        int GetCountWithSalaryAboveAverage(decimal avgSalary);
    }
}
