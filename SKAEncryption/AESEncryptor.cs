using System.Security.Cryptography;

namespace SKAEncryption
{
    class AESEncryptor : SKAEncryptor
    {
        public AESEncryptor()
        {
            keySize = 16;
            ivSize = 16;
        }

        public string Encrypt(string data)
        {
            string encryptedText;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                encryptedText = base.Encrypt(data, aes);
            }

            return encryptedText;
        }

        public string Decrypt(string text)
        {
            string plainText;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                plainText = base.Decrypt(text, aes);
            }

            return plainText;
        }
    }
}
