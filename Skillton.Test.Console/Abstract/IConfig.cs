namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс конфигурационных параметров
    /// </summary>
    internal interface IConfig
    {
        /// <summary>
        /// Gets the employee validation parameters.
        /// </summary>
        /// <value>
        /// The employee validation parameters.
        /// </value>
        IEmployeeValidationParams EmployeeValidationParams { get; set; }

        /// <summary>
        /// Gets or sets the data base configuration parameters.
        /// </summary>
        /// <value>
        /// The data base configuration parameters.
        /// </value>
        IDatabaseConfigParams DataBaseConfigParams { get; set; }

        /// <summary>
        /// Gets or sets the log configuration parameters.
        /// </summary>
        /// <value>
        /// The log configuration parameters.
        /// </value>
        ILogConfigParams LogConfigParams { get; set; }        
    }
}
