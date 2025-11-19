using System;

namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Абстрактная база контроллера, чтобы не тиражировать обертку писателя логгера
    /// </summary>
    internal abstract class ServiceBase
    {
        /// <summary>
        /// Зависимость, которая умеет писать куда-нибудь лог
        /// </summary>
        /// <value>
        /// The write log action.
        /// </value>
        protected Action<string> WriteLogAction { get; set; }

        /// <summary>
        /// Обертка для вызова делегата, записывающего лог
        /// </summary>
        /// <param name="message">The message.</param>
        protected void WriteLog(string message)
        {
            WriteLogAction?.Invoke(message);
        }
    }
}
