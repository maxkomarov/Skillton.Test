using Skillton.Test.Console_Net48.Abstract;
using System;
using System.IO;

namespace Skillton.Test.Console_Net48.Services
{
    /// <summary>
    /// Реализация интерфейса ILogger
    /// </summary>
    /// <seealso cref="Skillton.Test.Console_Net48.Abstract.ILogger" />
    internal class LogService : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="logConfigParams">The log configuration parameters.</param>
        public LogService(ILogConfigParams logConfigParams)
        {
            LogConfigParams = logConfigParams;
        }

        /// <summary>
        /// Gets the log configuration parameters.
        /// </summary>
        /// <value>
        /// The log configuration parameters.
        /// </value>
        public ILogConfigParams LogConfigParams {  get; private set; }

        /// <summary>
        /// Запись сообщения в файл лога
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Write(string message)
        {
            File.AppendAllText(LogConfigParams.LogFileName, $"{DateTime.Now}: {message}\r\n");
            //Console.WriteLine(message); //для дублирования вывода комманд к БД на консоль раскомментитть
        }
    }
}
