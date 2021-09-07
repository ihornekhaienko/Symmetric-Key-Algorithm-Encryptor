using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SKAEncryption
{
    abstract class SKAEncryptor
    {
        public byte[] keyArr { get; protected set; }
        public byte[] ivArr { get; protected set; }

        protected static Random random;
        protected int keySize;
        protected int ivSize;

        static SKAEncryptor()
        {
            random = new Random();
        }
        public (string, string) RandomKey()
        {
            keyArr = new byte[keySize];
            ivArr = new byte[ivSize];

            for (int i = 0; i < keySize; i++)
            {
                keyArr[i] = Convert.ToByte(random.Next(256));
            }

            for (int i = 0; i < ivSize; i++)
            {
                ivArr[i] = Convert.ToByte(random.Next(256));
            }

            return (Encoding.Unicode.GetString(keyArr), Encoding.Unicode.GetString(ivArr));
        }

        public void GenerateKey(string key, string iv)
        {
            if (key == null || iv == null)
                return;

            keyArr = new byte[keySize];
            ivArr = new byte[ivSize];

            byte[] keyByte = Encoding.Unicode.GetBytes(key);

            for (int i = 0; i < keyByte.Length; i++)
            {
                keyArr[i] = keyByte[i];
            }

            for (int i = keyByte.Length; i < keyArr.Length; i++)
            {
                keyArr[i] = 0;
            }

            byte[] ivByte = Encoding.Unicode.GetBytes(iv);

            for (int i = 0; i < ivByte.Length; i++)
            {
                ivArr[i] = ivByte[i];
            }

            for (int i = ivByte.Length; i < ivArr.Length; i++)
            {
                ivArr[i] = 0;
            }
        }
        protected string Encrypt(string data, SymmetricAlgorithm sa)
        {
            if (data == null || keyArr == null)
            {
                return data;
            }
            byte[] encrypted;
            sa.Key = keyArr;
            sa.IV = ivArr;

            ICryptoTransform encryptor = sa.CreateEncryptor(sa.Key, sa.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(data);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return Encoding.Unicode.GetString(encrypted);
        }
        protected string Decrypt(string encryptedData, SymmetricAlgorithm sa)
        {
            if (encryptedData == null || keyArr == null)
            {
                return encryptedData;
            }

            string plaintext = null;

            sa.Key = keyArr;
            sa.IV = ivArr;

            ICryptoTransform decryptor = sa.CreateDecryptor(sa.Key, sa.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Encoding.Unicode.GetBytes(encryptedData)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }
    }
}
