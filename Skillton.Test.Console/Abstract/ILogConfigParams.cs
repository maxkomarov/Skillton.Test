namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс параметров логгирования
    /// </summary>
    internal interface ILogConfigParams : IConfigParams
    {
        /// <summary>
        /// Имя файла лога
        /// </summary>
        /// <value>
        /// The name of the log file.
        /// </value>
        string LogFileName { get; set; }
    }
}
