using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace antrasPraktinis
{
    class AEScipher
    {
        Aes aes;
        public AEScipher()
        {
            aes = Aes.Create();
            aes.BlockSize = 128;
            byte[] iv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7;
        }
        public void setKey(string key)
        {
            aes.KeySize = key.Length * 8;
            aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
        }

        public void setCipherMode(CipherMode mode)
        {
            aes.Mode = mode;
        }

        public string Encrypt(string plainText)
        {
            byte[] plainBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(plainBytes, 0, plainBytes.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();
            return Convert.ToBase64String(cipherBytes);
        }

        public string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.FlushFinalBlock();
            byte[] plainBytes = ms.ToArray();
            return ASCIIEncoding.ASCII.GetString(plainBytes);
        }
    }
}
