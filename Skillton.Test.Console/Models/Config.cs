using Skillton.Test.Console_Net48.Abstract;

namespace Skillton.Test.Console_Net48.Models
{
    /// <summary>
    /// Интерфейс конфигурации приложения
    /// </summary>
    /// <seealso cref="Skillton.Test.Console_Net48.Abstract.IConfig" />
    internal class Config : IConfig
    {
        /// <summary>
        /// Инициализация по умолчанию
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        public Config() 
        {
            EmployeeValidationParams = new EmployeeValidationParams();
            LogConfigParams = new FileLogConfigParams();
            DataBaseConfigParams = new DatabaseConfigParams();
        }

        public IEmployeeValidationParams EmployeeValidationParams {get;set;}
        public IDatabaseConfigParams DataBaseConfigParams { get; set; }
        public ILogConfigParams LogConfigParams { get; set; }
    }
}
