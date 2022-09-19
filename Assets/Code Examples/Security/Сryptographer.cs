//----------------------------------------
// 	     Abony Int. - Сryptographer
// Copyright © 2020 'OOO EBONI INTERAKTIV'
//----------------------------------------

using System;
using System.Text;

namespace AbonyInt.Security
{
	public static class Сryptographer
	{
        public static byte[] EncryptKey = { 3, 1, 4, 8, 2, 9, 5, 0, 1, 1, 0, 2, 1, 9, 9, 1, 6, 58, 47 };
		public static byte[] EncodeKey = Guid.NewGuid().ToByteArray();

        #region Encode / Decode
        private static byte[] Encode(byte[] bytes, byte[] key)
        {
            if (key == null)
                key = EncodeKey;

            int j = 0;

            for (int i = 0; i < bytes.Length; i++) {
                bytes[i] ^= key[j];

                if (++j == key.Length)
                    j = 0;
            }

            return bytes;
        }

        /// <summary>
        /// Encode value (fast and lightweight encryption).
        /// </summary>
        /// <param name="value">Object to encrypt (the result will be empty if value is null)</param>
        /// <returns>Returns: encrypted string.</returns>
        public static string Encode(object value, byte[] key = null)
        {
            return value == null
                ? ""
                : Convert.ToBase64String(Encode(Encoding.UTF8.GetBytes(value.ToString()), key));
        }

        /// <summary>
        /// Decode the string. Only works with strings ecnrypted by 'Encode' method.
        /// </summary>
        /// <param name="value">Ecnrypted string. The result will be empty if value is null.</param>
        /// <returns>Returns: decrypted string.</returns>
        public static string Decode(string value, byte[] key = null) 
        {
            if (key == null)
                key = EncodeKey;

            return string.IsNullOrEmpty(value)
                ? ""
                : Encoding.UTF8.GetString(Encode(Convert.FromBase64String(value), key));
        }

        /// <summary>
        /// Decode the string. Only works with strings ecnrypted by 'Encode' method.
        /// </summary>
        /// <param name="value">Ecnrypted string. The result will be empty if value is null.</param>
        /// <returns>Returns: decrypted object with type of T.</returns>
        public static T Decode<T>(string value, byte[] key = null)
        {
            if (key == null)
                key = EncodeKey;

            return string.IsNullOrEmpty(value)
                ? default(T)
                : (T)Convert.ChangeType(Encoding.UTF8.GetString(Encode(Convert.FromBase64String(value), key)), typeof(T));
        }
        #endregion

        #region Encrypt / Decrypt
        /// <summary>
        /// Heavy encryption. It's very slow, don't use it without very important reason.
        /// </summary>
        /// <param name="value">Object to encrypt. The result will be empty if value is null.</param>
        /// <param name="key">Key to encrypt the string. Without this key you can not decrypt the string back!</param>
        /// <returns>Returns: encrypted string.</returns>
        public static string Encrypt(object value, byte[] key = null)
        {
            if (key == null)
                key = EncryptKey;

            return value == null
                ? ""
                : Convert.ToBase64String(Encode(Encoding.UTF8.GetBytes(value.ToString()), key));
        }

        /// <summary>
        /// Decrypt the string. Only works with strings encrypted by 'Ecnrypt' method.
        /// </summary>
        /// <param name="value">Encrypted string. The result will be empty if value is null.</param>
        /// <param name="key">Key to decrypt the string (the same key that you specified during encryption).</param>
        /// <returns>Returns: decrypted string.</returns>
        public static string Decrypt(string value, byte[] key = null)
        {
            if (key == null)
                key = EncryptKey;

            return string.IsNullOrEmpty(value)
                ? ""
                : Encoding.UTF8.GetString(Encode(Convert.FromBase64String(value), key));
        }

        /// <summary>
        /// Decrypt the string. Only works with strings encrypted by 'Ecnrypt' method.
        /// </summary>
        /// <param name="value">Ecnrypted string. The result will be empty if value is null.</param>
        /// <param name="key">Key to decrypt the string (the same key that you specified during encryption).</param>
        /// <returns>Returns: decrypted object type of T.</returns>
        public static T Decrypt<T>(string value, byte[] key = null)
        {
            if (key == null)
                key = EncryptKey;

            return string.IsNullOrEmpty(value)
                ? default(T)
                : (T)Convert.ChangeType(Encoding.UTF8.GetString(Encode(Convert.FromBase64String(value), key)), typeof(T));
        }
        #endregion
	}
}