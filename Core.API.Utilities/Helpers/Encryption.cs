using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Core.API.Utilities.Helpers
{
    internal class Encryption
    {
        int mlngCryptContext = 0;   // this is the encryption Context handle
        int lngHashHwd = 0; // Hash handle

        [DllImport("advapi32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int CryptDestroyHash(int hhash);

        [DllImport("advapi32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int CryptEncrypt(int hkey, int hhash, int Final, int dwFlags, byte[] pbData, ref int pdwDataLen, int dwBufLen);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptDecrypt(int hKey, int hHash, int Final, int dwFlags, byte[] pbData, ref int pdwDataLen);

        [DllImport("advapi32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int CryptDeriveKey(int hProv, int algid, int hBaseData, int dwFlags, ref int phKey);

        [DllImport("advapi32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int CryptHashData(int hhash, string pbData, int dwDataLen, int dwFlags);

        [DllImport("advapi32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int CryptCreateHash(int hProv, int algid, int hkey, int dwFlags, ref int phHash);

        [DllImport("advapi32.dll", EntryPoint = "CryptAcquireContextA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int CryptAcquireContext(ref int phProv, string pszContainer, string pszProvider, int dwProvType, int dwFlags);

        internal string Encrypt(string mstrInputData, string strPassword)
        {
            // ---------------------------------------------------------------------------
            // Define local variables
            // ---------------------------------------------------------------------------
            int lngHkey = 0;
            int lngCryptLength = 0;
            int lngCryptBufLen = 0;
            string strCryptBuffer = null;

            // ---------------------------------------------------------------------------
            // Aquire the provider handle
            // ---------------------------------------------------------------------------
            if (mlngCryptContext == 0)
            {
                if (!GetProvider())
                {
                    return null;
                }
            }

            // ---------------------------------------------------------------------------
            // Create a hash object
            // ---------------------------------------------------------------------------
            int iReturn = CryptCreateHash(mlngCryptContext, 0x00008003, 0, 0, ref lngHashHwd); // 0x00008003 == MD5
            if (!System.Convert.ToBoolean(iReturn))
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Hash in the password text
            // ---------------------------------------------------------------------------
            if (!System.Convert.ToBoolean(CryptHashData(lngHashHwd, strPassword, strPassword.Length, 0)))
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Create a session key from the hash object
            // ---------------------------------------------------------------------------
            if (!System.Convert.ToBoolean(CryptDeriveKey(mlngCryptContext, 0x00006601, lngHashHwd, 0, ref lngHkey))) // 0x00006601 = DES
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Destroy hash object
            // ---------------------------------------------------------------------------
            if (lngHashHwd != 0)
            {
                if (!System.Convert.ToBoolean(CryptDestroyHash(lngHashHwd)))
                {
                    return null;
                }
            }

            // ---------------------------------------------------------------------------
            // Prepare the data to Encrypt
            // ---------------------------------------------------------------------------
            lngCryptLength = mstrInputData.Length;
            lngCryptBufLen = lngCryptLength * 2;
            strCryptBuffer = mstrInputData + new string('\0', lngCryptLength);

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(strCryptBuffer);

            // ---------------------------------------------------------------------------
            // Encrypt the text data
            // ---------------------------------------------------------------------------
            if (!System.Convert.ToBoolean(CryptEncrypt(lngHkey, 0, 1, 0, bytes, ref lngCryptLength, lngCryptBufLen)))
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Return the encrypted data string in a byte array
            // ---------------------------------------------------------------------------
            return BitConverter.ToString(bytes).Replace("-", "").Substring(0, lngCryptLength * 2);
        }

        internal string Decrypt(string mstrInputData, string strPassword)
        {
            // ---------------------------------------------------------------------------
            // Define local variables
            // ---------------------------------------------------------------------------
            int lngHkey = 0;
            int lngCryptLength = 0;
            int lngCryptBufLen = 0;

            // ---------------------------------------------------------------------------
            // Aquire the provider handle
            // ---------------------------------------------------------------------------
            if (mlngCryptContext == 0)
            {
                if (!GetProvider())
                {
                    return null;
                }
            }

            // ---------------------------------------------------------------------------
            // Create a hash object
            // ---------------------------------------------------------------------------
            int iReturn = CryptCreateHash(mlngCryptContext, 0x00008003, 0, 0, ref lngHashHwd); // 0x00008003 == MD5
            if (!System.Convert.ToBoolean(iReturn))
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Hash in the password text
            // ---------------------------------------------------------------------------
            if (!System.Convert.ToBoolean(CryptHashData(lngHashHwd, strPassword, strPassword.Length, 0)))
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Create a session key from the hash object
            // ---------------------------------------------------------------------------
            if (!System.Convert.ToBoolean(CryptDeriveKey(mlngCryptContext, 0x00006601, lngHashHwd, 0, ref lngHkey))) // 0x00006601 = DES
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Destroy hash object
            // ---------------------------------------------------------------------------
            if (lngHashHwd != 0)
            {
                if (!System.Convert.ToBoolean(CryptDestroyHash(lngHashHwd)))
                {
                    return null;
                }
            }

            // ---------------------------------------------------------------------------
            // Prepare the data to Encrypt
            // ---------------------------------------------------------------------------
            Byte[] cbytes = StringToByteArray(mstrInputData);
            lngCryptLength = cbytes.Length;
            lngCryptBufLen = lngCryptLength * 2;
            Byte[] bytes = new byte[lngCryptBufLen];
            for (int i = 0; i < lngCryptBufLen; i++)
            {
                bytes[i] = 0x0;
            }
            for (int i = 0; i < lngCryptLength; i++)
            {
                bytes[i] = cbytes[i];
            }

            // ---------------------------------------------------------------------------
            // Decrypt the text data
            // ---------------------------------------------------------------------------
            if (!System.Convert.ToBoolean(CryptDecrypt(lngHkey, 0, 1, 0, bytes, ref lngCryptLength)))
            {
                return null;
            }

            // ---------------------------------------------------------------------------
            // Return the encrypted data string in a byte array - good
            // ---------------------------------------------------------------------------
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return (enc.GetString(bytes)).Substring(0, lngCryptLength);

        }

        internal static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private bool GetProvider()
        {
            const string MS_DEFAULT_PROVIDER = "Microsoft Base Cryptographic Provider v1.0";
            const int CRYPT_VERIFYCONTEXT = unchecked((int)(0xF0000000));
            const int PROV_RSA_FULL = 1;

            string strTemp = "\0";
            string strProvider2 = System.Convert.ToString(MS_DEFAULT_PROVIDER + '\0');

            // Attempt to acquire a handle to the DEFAULT (56-bit) key container.
            if (System.Convert.ToBoolean(CryptAcquireContext(ref mlngCryptContext, strTemp, strProvider2,
                System.Convert.ToInt32(PROV_RSA_FULL), System.Convert.ToInt32(CRYPT_VERIFYCONTEXT))))
            {
                return true;
            }

            return false;
        }

        internal static string TextMask(string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }
    }
}
