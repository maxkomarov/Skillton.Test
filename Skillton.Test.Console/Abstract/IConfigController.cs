using System;

namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс конфигурации
    /// </summary>
    internal interface IConfigController
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        IConfig Config { get; }

        /// <summary>
        /// Определяет наличие установленной версии SQL CE
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Ошибка при проверке доступной инсталляции SQL CE: {ex.Message}</exception>
        Tuple<bool, string> IsSqlCeInstalled();
    }
}
