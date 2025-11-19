using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Helpers;

namespace Skillton.Test.Console_Net48.Models
{
    internal class DatabaseConfigParams : IDatabaseConfigParams
    {
        private readonly string _passwordKey = "cryptoKey123";
        private string _databasePassword;

        /// <summary>
        /// Загрузка параметров из конфиг-файла
        /// Initializes a new instance of the <see cref="DatabaseConfigParams"/> class.
        /// </summary>
        public DatabaseConfigParams() { Load(); }

        /// <summary>
        /// Строка подключения на основе параметров конфига
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString 
        { 
            get
            {            
                if (!string.IsNullOrEmpty(DatabaseFileName))
                    return $"DataSource=\"{DatabaseFileName}\"; Password=\"{_databasePassword}\"";
                else
                    return $"DataSource=\"{DatabaseFileName}\"";
            }
        }

        /// <summary>
        /// Имя файла БД
        /// </summary>
        /// <value>
        /// The name of the database file.
        /// </value>
        public string DatabaseFileName { get; private set; }

        /// <summary>
        /// Загрузка (инициализация) конфигурации
        /// </summary>
        public void Load()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.DatabaseFileName))
                DatabaseFileName = Properties.Settings.Default.DatabaseFileName;
            else
                DatabaseFileName = Constants.DEFAULT_DB_FILENAME;

            _databasePassword = Cryptex.Decrypt(Properties.Settings.Default.DatabasePassword, _passwordKey);            
        }
    }
}
