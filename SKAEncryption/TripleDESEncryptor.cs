using System.Security.Cryptography;

namespace SKAEncryption
{
    class TripleDESEncryptor : SKAEncryptor
    {
        public TripleDESEncryptor()
        {
            keySize = 24;
            ivSize = 8;
        }

        public string Encrypt(string data)
        {
            string encryptedText = null;

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                encryptedText = base.Encrypt(data, tdes);
            }

            return encryptedText;
        }
        public string Decrypt(string text)
        {
            string plainText;

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                plainText = base.Decrypt(text, tdes);
            }

            return plainText;
        }
    }
}
