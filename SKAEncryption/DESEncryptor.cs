using System.Security.Cryptography;

namespace SKAEncryption
{
    class DESEncryptor : SKAEncryptor
    {
        public DESEncryptor()
        {
            keySize = 8;
            ivSize = 8;
        }

        public string Encrypt(string data)
        {
            string encryptedText;

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                encryptedText = base.Encrypt(data, des);
            }

            return encryptedText;
        }

        public string Decrypt(string text)
        {
            string plainText;

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                plainText = base.Decrypt(text, des);
            }

            return plainText;
        }
    }
}
