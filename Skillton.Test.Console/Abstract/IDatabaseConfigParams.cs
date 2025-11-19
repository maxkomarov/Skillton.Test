namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс с параметрами БД
    /// </summary>
    internal interface IDatabaseConfigParams : IConfigParams
    {
        /// <summary>
        /// Строка подключения на основе параметров конфига
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; }

        string DatabaseFileName { get; }
    }
}
