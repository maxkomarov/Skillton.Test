namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс логгера
    /// </summary>
    internal interface ILogger
    {
        /// <summary>
        /// Gets the log configuration parameters.
        /// </summary>
        /// <value>
        /// The log configuration parameters.
        /// </value>
        ILogConfigParams LogConfigParams { get; }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Write(string message);
    }
}
