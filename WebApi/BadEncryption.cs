using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class BadEncryption
    {
        private const string EqualSignReplacement = "ESreplacedES";
        private const string PlusSignReplacement = "PSreplacedPS";
        private const string AmpReplacement = "ARreplacedAR";
        private const string EncryptionKey = "#d9HE$^&";
        private string Encrypt(string stringToEncrypt)
        {

            byte[] bytes1 = Encoding.UTF8.GetBytes(EncryptionKey);
            byte[] rgbIV = new byte[8]
            {
                (byte) 18,
                (byte) 52,
                (byte) 86,
                (byte) 120,
                (byte) 144,
                (byte) 171,
                (byte) 205,
                (byte) 239
            };
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes2 = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(bytes1, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(bytes2, 0, bytes2.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
