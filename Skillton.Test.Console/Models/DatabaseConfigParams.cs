using Skillton.Test.Console_Net48.Abstract;
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

            _databasePassword = Decrypt(Properties.Settings.Default.DatabasePassword, _passwordKey);            
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка шифратора данных доступа к БД", ex);
            }
        }
        
        public string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка дешифратора данных доступа к БД", ex);
            }
        }
    }
}
