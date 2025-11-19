using System;
using System.Security.Cryptography;
using System.Text;
namespace Skillton.Test.Console_Net48.Helpers
{
    /// <summary>
    /// Утилитарный шифратор/дешифратор для пароля
    /// </summary>
    internal static class Cryptex
    {
        /// <summary>
        /// Шифруем строку с ключом
        /// </summary>
        /// <param name="toEncript">The string to encrypt.</param>
        /// <param name="key">The string key.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Ошибка шифратора данных доступа к БД</exception>
        public static string Encrypt(string toEncript, string key)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = key;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(toEncript);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка шифратора данных доступа к БД", ex);
            }
        }

        /// <summary>
        /// Дешифруем строку ключом
        /// </summary>
        /// <param name="toDecript">The string encrypted.</param>
        /// <param name="key">The string key.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Ошибка дешифратора данных доступа к БД</exception>
        public static string Decrypt(string toDecript, string key)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = key;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(toDecript);
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
