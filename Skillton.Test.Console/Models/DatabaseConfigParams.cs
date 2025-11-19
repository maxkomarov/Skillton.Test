using Skillton.Test.Console_Net48.Abstract;
using Skillton.Test.Console_Net48.Helpers;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Skillton.Test.Console_Net48.Models
{
    internal class DatabaseConfigParams : IDatabaseConfigParams
    {
        private readonly string _passwordKey = "cryptoKey123";
        private string _databasePassword;
        public DatabaseConfigParams() { Load(); }

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

        public string DatabaseFileName { get; private set; }

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
