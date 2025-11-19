using Skillton.Test.Console_Net48.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;

namespace Skillton.Test.Console_Net48.Controllers
{
    internal class SqlCEDatabaseController : ControllerBase, IDataBaseController
    {        
        private readonly IDatabaseConfigParams _dataBaseConfigParams;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCEDatabaseController"/> class.
        /// </summary>
        /// <param name="dataBaseConfigParams">The data base configuration parameters.</param>
        /// <param name="writeLogAction">The write log action.</param>
        /// <exception cref="System.ArgumentNullException">dataBaseConfigParams</exception>
        public SqlCEDatabaseController(IDatabaseConfigParams dataBaseConfigParams,
                                  Action<string> writeLogAction = null)
        {
            _dataBaseConfigParams = dataBaseConfigParams 
                ?? throw new ArgumentNullException(Constants.NULLABLE_ARGUMENT_NOT_ALLOWED, nameof(dataBaseConfigParams));
            
            WriteLogAction = writeLogAction;
        }

        #region IDataBaseController имплементация        

        public void EnsureCreated()
        {
            if (!File.Exists(_dataBaseConfigParams.DatabaseFileName))
            {
                WriteLog("База данных отсутствует. Приступаем к созданию...");
                CreateDatabase();
                ExecuteNonQuery(_createEmployeesTableQuery); //Создаем таблицу
                ExecuteNonQuery(_createEmployeesTableNamesIndex); //Создаем индекс на FirstName+LastName
                ExecuteNonQuery(_createEmployeesTableEmailIndex); //Создаем индекс на Email
                //тут бы загнать пару тестовых строк -> Сделано, только из корня
                WriteLog("База данных успешно создана.");
            }
            else
            {
                WriteLog("База данных присутсвует. Проверяем доступность данных...");
                ExecuteNonQuery(_selectCountFromEmployees); //выполняем тестовый запрос
                //тут бы вернуть кол-во строк
                WriteLog("База данных доступна для выполнения запросов.");
            }
        }        

        /// <summary>
        /// Возвращает массив пар (ключ, объект), представляющих записи из запроса
        /// </summary>
        /// <param name="command">The command.</param>
        public IList<IDictionary<string, object>> ExecuteSelect(IDictionary<string, object> cmdParams = null) //SqlCeCommand command)
        {
            IList<IDictionary<string, object>> data 
                = new List<IDictionary<string, object>>(); 
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(_dataBaseConfigParams.ConnectionString))
                {
                    connection.Open();
                    SqlCeCommand command = connection.CreateCommand();
                    using (command)
                    {
                        command.Connection = connection;

                        if (cmdParams != null && cmdParams.ContainsKey("EmployeeID"))
                        {
                            command.CommandText = QueryConstants.SELECT_BY_ID_CMD_TEXT;
                            command.Parameters.AddWithValue("EmployeeID", cmdParams["EmployeeID"]);
                        }
                        else
                            command.CommandText = QueryConstants.SELECT_ALL_CMD_TEXT;

                        SqlCeDataReader reader = command.ExecuteReader();
                        
                        while (reader.Read())
                        {
                            IDictionary<string, object> rowData 
                                = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowData.Add(
                                    new KeyValuePair<string, object>(
                                        reader.GetName(i), reader.GetValue(i)));
                            }
                            data.Add(rowData);
                        }
                        WriteLog($"SQL CE: команда выполнена успешно, найдено {data.Count} записей:");
                        WriteLog($"SQL CE: {command.CommandText}:");
                    }
                }
            }
            catch (SqlCeException ex)
            {
                throw new System.Exception("Ошибка выполнения команды для базы данных. См.внутреннее исключение.", ex);
            }
            return data;
        }

        /// <summary>
        /// Возвращает среднюю з/п по всем записям
        /// </summary>
        /// <returns></returns>
        public decimal GetAvgSalary()
        {
            decimal res;
            
            try
            {
                res = (decimal)ExecuteScalar(QueryConstants.SELECT_AVG_SALARY_CMD_TEXT);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка приведения значения к типу decimal в GetAvgSalary", ex);
            }

            return res;
        }

        /// <summary>
        /// Взозвращает кол-во записей с з/п выше средней
        /// </summary>
        /// <param name="avg"></param>
        /// <returns></returns>
        public int GetCountWithSalaryAboveAverage(decimal avg)
        {
            IDictionary<string, object> cmdParams= new Dictionary<string, object>
            {
                { "avg", avg }
            };

            try
            {
                int res = (int)ExecuteScalar(QueryConstants.SELECT_SALARY_ABV_AVG_CMD_TEXT, cmdParams);
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка приведения значения к типу int в GetAboveAvgSalaryCount", ex);
            }
        }
        
        /// <summary>
        /// Сохраняет измененную запись  в БД
        /// </summary>
        /// <param name="cmdParams">The command parameters.</param>
        /// <returns></returns>
        public int ExecuteUpdate(IDictionary<string, object> cmdParams)
        {
            return ExecuteCommand(QueryConstants.UPDATE_CMD_TEXT, cmdParams);
        }

        /// <summary>
        /// Сохраняет новую запись в БД
        /// </summary>
        /// <param name="cmdParams">The command parameters.</param>
        /// <returns></returns>        
        public int ExecuteInsert(IDictionary<string, object> cmdParams)
        {
            return ExecuteCommand(QueryConstants.INSERT_CMD_TEXT, cmdParams);
        }

        /// <summary>
        /// Удаляет запись из БД
        /// </summary>
        /// <param name="cmdParams">The command parameters.</param>
        /// <returns></returns>
        public int ExecuteDelete(IDictionary<string, object> cmdParams)
        {
            return ExecuteCommand(QueryConstants.DELETE_CMD_TEXT, cmdParams);
        }


        #endregion

        #region Приватные методы

        /// <summary>
        /// Сервисный метод для создания БД
        /// </summary>
        /// <exception cref="System.Exception">Ошибка создания базы данных. См.внутреннее исключение.</exception>
        private void CreateDatabase()
        {
            using (SqlCeEngine engine = new SqlCeEngine(_dataBaseConfigParams.ConnectionString))
            {
                try
                {
                    engine.CreateDatabase();
                    WriteLog($"База данных SQL CE database '{_dataBaseConfigParams.ConnectionString}' создана успешно.");
                }
                catch (SqlCeException ex)
                {
                    throw new System.Exception("Ошибка создания базы данных. См.внутреннее исключение.", ex);
                }
            }
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <exception cref="System.Exception">Ошибка выполнения запроса к базе данных. См.внутреннее исключение.</exception>
        private void ExecuteNonQuery(string query)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(_dataBaseConfigParams.ConnectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = new SqlCeCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        WriteLog("SQL CE: запрос выполнен успешно:");
                        WriteLog($"SQL CE: {query}:");
                    }
                }
            }
            catch (SqlCeException ex)
            {
                throw new Exception("Ошибка выполнения запроса к базе данных. См.внутреннее исключение.", ex);
            }
        }        

        private int ExecuteCommand(string cmdText, IDictionary<string, object> cmdParams)
        {
            int res = 0;
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(_dataBaseConfigParams.ConnectionString))
                {
                    SqlCeCommand command = connection.CreateCommand();
                    command.CommandText = cmdText;

                    cmdParams.ToList()
                        .ForEach(x =>
                            command.Parameters.AddWithValue($"@{x.Key}", x.Value));

                    connection.Open();
                    using (command)
                    {
                        command.Connection = connection;
                        res = command.ExecuteNonQuery();
                        WriteLog("SQL CE: команда выполнена успешно:");
                        WriteLog($"SQL CE: {command.CommandText}:");
                    }
                }
            }
            catch (SqlCeException ex)
            {
                throw new System.Exception("Ошибка выполнения команды для базы данных. См.внутреннее исключение.", ex);
            }
            return res;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Ошибка выполнения команды для базы данных. См.внутреннее исключение.</exception>
        private object ExecuteScalar(string cmdText, IDictionary<string, object> cmdParams = null)
        {
            object res;
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(_dataBaseConfigParams.ConnectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = cmdText;

                        if (cmdParams != null && cmdParams.Count > 0)
                            cmdParams.ToList()
                                .ForEach(x =>
                                    command.Parameters.AddWithValue($"@{x.Key}", x.Value));

                        res = command.ExecuteScalar();

                        WriteLog($"SQL CE: команда выполнена успешно, возвращено значение: {res}");
                        WriteLog($"SQL CE: {command.CommandText}:");
                    }
                }
            }
            catch (SqlCeException ex)
            {
                throw new Exception("Ошибка выполнения команды для базы данных. См.внутреннее исключение.", ex);
            }
            return res;
        }

        

        #endregion

        #region Скрипты для базы

        private readonly string _createEmployeesTableQuery =
            "CREATE TABLE Employees " +
            "(EmployeeID INT PRIMARY KEY IDENTITY(1,1)," +
            "FirstName NVARCHAR(50) NOT NULL," +
            "LastName NVARCHAR(50) NOT NULL," +
            "Email NVARCHAR(100) UNIQUE," +
            "DateOfBirth DATETIME," +
            "Salary NUMERIC(18,2))";

        private readonly string _createEmployeesTableNamesIndex =
            "CREATE UNIQUE INDEX IX_Employees_FirstName_LastName ON Employees (FirstName, LastName)";

        private readonly string _createEmployeesTableEmailIndex =
            "CREATE UNIQUE INDEX IX_Employees_Email ON Employees (Email)";

        private readonly string _selectCountFromEmployees =
            "SELECT count(*) FROM Employees";

        #endregion
    }
}
