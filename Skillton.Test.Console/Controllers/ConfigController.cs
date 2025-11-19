using Microsoft.Win32;
using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Models;
using System;

namespace Skillton.Test.Console_Net48
{
    /// <summary>
    /// Имплементация контроллера конфига
    /// </summary>
    /// <seealso cref="Skillton.Test.Console_Net48.Abstract.IConfigController" />
    internal class ConfigController : IConfigController
    {
        public ConfigController()
        {
            Config = new Config();
        }

        public IConfig Config { get; private set; }

        /// <summary>
        /// Определяет наличие установленной версии SQL CE
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Ошибка при проверке доступной инсталляции SQL CE: {ex.Message}</exception>
        public Tuple<bool, string> IsSqlCeInstalled()
        {
            try
            {
                string path35 = @"SOFTWARE\Microsoft\Microsoft SQL Server Compact Edition\v3.5";
                string path40 = @"SOFTWARE\Microsoft\Microsoft SQL Server Compact Edition\v4.0";


                using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey(path40))
                {
                    if (baseKey != null)
                    {
                        string version = baseKey.GetValue("Version") as string;
                        return Tuple.Create(true, version);
                    }
                }
                using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey(path35))
                {
                    if (baseKey != null)
                    {
                        string version = baseKey.GetValue("Version") as string;
                        return Tuple.Create(true, version);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при проверке доступной инсталляции SQL CE: {ex.Message}", ex);
            }
            return Tuple.Create(false, string.Empty);
        }
    }
}
