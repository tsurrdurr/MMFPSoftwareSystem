using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MMFPCommonDataStructures.Crypto
{
    class Crypto
    {
        private String Password = "TvoyaMomasha";
        private int Rfc2898KeygenIterations = 100;
        private int AesKeySizeInBits = 128;

        public byte[] CreateSalt()
        {
            byte[] Salt = new byte[16];
            var rnd = new System.Random();
            rnd.NextBytes(Salt);
            return Salt;
        }

        public byte[] Encode (int correctAns, byte[] Salt)
        {
            byte[] rawPlaintext = BitConverter.GetBytes(correctAns);
            byte[] cipherText;
            using (Aes aes = new AesManaged())
            {
                PrepareAesObject(Salt, aes);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                    }
                    cipherText = ms.ToArray();
                }

            }
            return cipherText;
        }

        public int Decode(byte[] cryptedAns, byte[] Salt)
        {
            byte[] xd;
            using (Aes aes = new AesManaged())
            {
                PrepareAesObject(Salt, aes);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cryptedAns, 0, cryptedAns.Length);
                    }
                    xd = ms.ToArray();
                }
            }
            return BitConverter.ToInt32(xd, 0);
        }

        private void PrepareAesObject(byte[] Salt, Aes aes)
        {
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = AesKeySizeInBits;
            int KeyStrengthInBytes = aes.KeySize / 8;
            System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                new System.Security.Cryptography.Rfc2898DeriveBytes(Password, Salt, Rfc2898KeygenIterations);
            aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
            aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
        }

        
    }
}
