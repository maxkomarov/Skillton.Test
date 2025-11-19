using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Repositories;
using Skillton.Test.Console_Net48.Services;
using System;
using System.Text;

namespace Skillton.Test.Console_Net48
{
    internal class Program
    {
        static LogService _logger = null;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; //Иначе символ рубля не выводится           

            //Цепляемся к необработанным
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += (o,e) =>
            {
                _logger?.Write(e.ToString());
                WriteMessage(_logger, "\r\nОШИБКА!!! Возникло необработанное исключение!");
                if (e.IsTerminating)
                    WriteMessage(_logger, "Выполняется аварийная остановка приложения.\r\n");
                WriteMessage(_logger, e.ExceptionObject?.ToString());
            };

            try
            {
                //Инициализация контроллера конфигурации
                ConfigService configController = new ConfigService();
                if (configController.Config == null)
                    throw new Exception("Конфигурация приложения не инициализирована: " +
                        "продолжение невозможно, приложение остановлено!");

                //Проверяем наличие SQL CE
                Tuple<bool, string> res = configController.IsSqlCeInstalled();
                if (res.Item1)
                    WriteMessage(_logger, $"\r\nMS SQL Compact Edition установлена, версия: [{res.Item2}]");
                else
                    throw new PlatformNotSupportedException(
                        "MS SQL Compact Edition не установлена, продолжение невозможно");

                //Инициализация логгера
                _logger = new LogService(configController.Config.LogConfigParams);
                _logger.Write("Приложение запущено...");

                //Инициализация валидатора сущности
                ValidationService validationController
                    = new ValidationService(configController.Config.EmployeeValidationParams);

                //Инициализация контроллера БД
                SqlCEDatabaseService databaseController
                    = new SqlCEDatabaseService(
                        configController.Config.DataBaseConfigParams,
                        _logger.Write);

                //Инициализация БД (если её нет)
                databaseController.EnsureCreated();

                //Инициализация моста к контроллеру БД
                EmployeeRepository dataController
                    = new EmployeeRepository(
                        databaseController,
                        _logger.Write);

                //Инициализация контроллера ввода
                RootInputController inputController
                    = new RootInputController(
                        validationController,
                        dataController,
                        _logger.Write);

                //Запуск главного процесса
                inputController.Run();

                //Закочили по команде, выходим...
                _logger.Write("Работа приложения завершена. Выход...");
            }
            catch (PlatformNotSupportedException pnse)
            {
                WriteMessage(_logger, "Конфигурация системы не соответствует требованиям приложения:");
                WriteMessage(_logger, pnse.Message);
            }
            catch (Exception ex)
            {
                WriteMessage(_logger, "В процессе работы приложения возникло исключение:");
                WriteMessage(_logger, ex.ToString());
                WriteMessage(_logger, ex.Source.ToString());
                WriteMessage(_logger, ex.StackTrace.ToString());
            }
        }

        private static void WriteMessage(ILogger logger, string text)
        {
            if (logger == null)
                Console.WriteLine(text);
            else
                logger.Write(text);
        }
    }
}
