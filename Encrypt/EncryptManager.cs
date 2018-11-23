using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Encrypt
{
    /// <summary>
    /// utility class to manage de password security
    /// </summary>
    /// <returns></returns>
    public class EncryptManager
    {
        private byte[] key = null;
        private byte[] IV = null;
        public EncryptManager(string clave, string iv)
        {
            key = Encoding.ASCII.GetBytes(clave);
            IV = Encoding.ASCII.GetBytes(iv);
        }

        public EncryptManager()
        {
        }
        /// <summary>
        /// to encrypt any string text
        /// </summary>
        /// <param name="inputText"></param> 
        /// <returns></returns>
        public string EncryptTextBase64(string inputText)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(inputText);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// <summary>
        /// to unencrypt any string text
        /// </summary>
        /// <param name="inputText"></param> 
        /// <returns></returns>
        public string DecryptTextBase64(string inputText)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(inputText);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }


        /// <summary>
        /// to encrypt any string text with much better security
        /// </summary>
        /// <param name="inputText"></param> 
        /// <returns></returns>
        public string EncryptText(string inputText)
        {

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] message = encoding.GetBytes(inputText);
            TripleDESCryptoServiceProvider criptoProvider = new TripleDESCryptoServiceProvider();
            ICryptoTransform criptoTransform = criptoProvider.CreateEncryptor(key, IV);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, criptoTransform, CryptoStreamMode.Write);
            cryptoStream.Write(message, 0, message.Length);

            // cryptoStream.Flush();
            cryptoStream.FlushFinalBlock();
             byte[] encriptado = memoryStream.ToArray();

            string cadenaEncriptada = encoding.GetString(encriptado);
            return cadenaEncriptada;

        }

        /// <summary>
        /// to unencrypt any string text with much better security
        /// </summary>
        /// <param name="message"></param> 
        /// <returns></returns>
        public string DecryptText(byte[] message)
        {

            TripleDES cryptoProvider =  new TripleDESCryptoServiceProvider();
            ICryptoTransform cryptoTransform = cryptoProvider.CreateDecryptor(key, IV);
            MemoryStream memoryStream = new MemoryStream(message);
            CryptoStream cryptoStream = new CryptoStream(memoryStream,cryptoTransform,CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cryptoStream, true);
            string cleanText = sr.ReadToEnd();
            return cleanText;

        }
    }
}
