using Skillton.Test.Console_Net48.Abstract;
using System;
using System.IO;

namespace Skillton.Test.Console_Net48.Models
{
    /// <summary>
    /// Имплементация параметров файлового логгера
    /// </summary>
    /// <seealso cref="Skillton.Test.Console_Net48.Abstract.ILogConfigParams" />
    internal class FileLogConfigParams : ILogConfigParams
    {
        /// <summary>
        /// КОнструктор с загрузкой по умолчанию
        /// Initializes a new instance of the <see cref="FileLogConfigParams"/> class.
        /// </summary>
        public FileLogConfigParams() { Load(); }

        /// <summary>
        /// Имя файла лога
        /// </summary>
        /// <value>
        /// The name of the log file.
        /// </value>
        public string LogFileName { get; set; }

        /// <summary>
        /// Загрузка (инициализация) из настроек приложения контекста пользователя
        /// </summary>
        public void Load()
        {
            LogFileName = Properties.Settings.Default.LogFileName;

            if (LogFileName == null)
            {
                LogFileName = Constants.DEFAULT_LOG_FILENAME;
                File.WriteAllText(LogFileName, $"{DateTime.Now}: создан файл лога с именем '{LogFileName}' по умолчанию");
            }
        }

        /// <summary>
        /// Сохранение в настройках приложения контекста пользователя
        /// </summary>
        public void Save()
        {
            Properties.Settings.Default.LogFileName = LogFileName;
        }
    }
}
