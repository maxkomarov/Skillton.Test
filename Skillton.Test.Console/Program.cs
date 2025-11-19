using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Controllers;
using System;
using System.Text;

namespace Skillton.Test.Console_Net48
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            LogController logController = null;

            //Цепляемся к необработанным
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += (o,e) =>
            {
                logController?.Write(e.ToString());
                WriteMessage(logController, "\r\nОШИБКА!!! Возникло необработанное исключение!");
                if (e.IsTerminating)
                    WriteMessage(logController, "Выполняется аварийная остановка приложения.\r\n");
                WriteMessage(logController, e.ExceptionObject?.ToString());
            };

            try
            {
                //Инициализация контроллера конфигурации
                ConfigController configController = new ConfigController();
                if (configController.Config == null)
                    throw new Exception("Конфигурация приложения не инициализирована: " +
                        "продолжение невозможно, приложение остановлено!");

                //Проверяем наличие SQL CE
                Tuple<bool, string> res = configController.IsSqlCeInstalled();
                if (res.Item1)
                    WriteMessage(logController, $"\r\nMS SQL Compact Edition установлена, версия: [{res.Item2}]");
                else
                    throw new PlatformNotSupportedException(
                        "MS SQL Compact Edition не установлена, продолжение невозможно");

                //Инициализация логгера
                logController = new LogController(configController.Config.LogConfigParams);
                logController.Write("Приложение запущено...");

                //Инициализация валидатора сущности
                ValidationController validationController
                    = new ValidationController(configController.Config.EmployeeValidationParams);

                //Инициализация контроллера БД
                SqlCEDatabaseController databaseController
                    = new SqlCEDatabaseController(
                        configController.Config.DataBaseConfigParams,
                        logController.Write);

                //Инициализация БД (если её нет)
                databaseController.EnsureCreated();

                //Инициализация моста к контроллеру БД
                DataController dataController
                    = new DataController(
                        databaseController,
                        logController.Write);

                //Инициализация контроллера ввода
                InputController inputController
                    = new InputController(
                        validationController,
                        dataController,
                        logController.Write);

                //Запуск главного процесса
                inputController.Run();

                //Закочили по команде, выходим...
                logController.Write("Работа приложения завершена. Выход...");
            }
            catch (PlatformNotSupportedException pnse)
            {
                WriteMessage(logController, "Конфигурация системы не соответствует требованиям приложения:");
                WriteMessage(logController, pnse.Message);
            }
            catch (Exception ex)
            {
                WriteMessage(logController, "В процессе работы приложения возникло исключение:");
                WriteMessage(logController, ex.ToString());
                WriteMessage(logController, ex.Source.ToString());
                WriteMessage(logController, ex.StackTrace.ToString());
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
