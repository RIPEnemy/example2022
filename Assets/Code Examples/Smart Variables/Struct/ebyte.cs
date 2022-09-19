using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct ebyte
    {
        public const Byte MaxValue = 255;
        public const Byte MinValue = 0;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public ebyte(byte value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public ebyte(string value, TypeValue type)
        {
            autoEncrypt = false;
            byte val = 0;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<byte>(value);
            serializeValue = val;
            Set(val);
        }
        
        public ebyte(byte value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private byte Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? (byte)0 : Сryptographer.Decode<byte>(a + b);
            else
                return Сryptographer.Decrypt<byte>(EncryptedValue);
        }

        private void Set(byte value)
        {
            string _encodedValue = Сryptographer.Encode(value);
            
            a = b = "";

            for (int i = 0; i < _encodedValue.Length; i++)
            {
                if (i < _encodedValue.Length / 2)
                    a += _encodedValue[i];
                else
                    b += _encodedValue[i];
            }

            if (autoEncrypt)
                Encrypt(value);
        }
    #endregion

    #region Encrypt
        public void Encrypt(byte value)
        {
            EncryptedValue = Сryptographer.Encrypt(value);
        }
    #endregion

    #region ToString
        public override string ToString()
        {
            return Get().ToString();
        }
        public string ToString(IFormatProvider provider)
        {
            return Get().ToString(provider);
        }
        public string ToString(string format)
        {
            return Get().ToString(format);
        }
        public string ToString(string format, IFormatProvider provider)
        {
            return Get().ToString(format, provider);
        }
    #endregion

    #region Implicit operatos
        public static implicit operator byte(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator int(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator uint(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator long(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator ulong(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator float(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator double(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator decimal(ebyte operand)
        {
            return operand.Get();
        }
        public static implicit operator ebyte(byte operand)
        {
            return new ebyte(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator char(ebyte operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator sbyte(ebyte operand)
        {
            return (sbyte)operand.Get();
        }
    #endregion

    #region Parse
        public static ebyte Parse(string s, IFormatProvider provider)
        {
            ebyte result = new ebyte(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(byte.Parse(s, provider));

            return result;
        }

        public static ebyte Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            ebyte result = new ebyte(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(byte.Parse(s, style, provider));

            return result;
        }

        public static ebyte Parse(string s, NumberStyles style)
        {
            ebyte result = new ebyte(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(byte.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ebyte result)
        {
            ebyte enbyte = new ebyte(0);
            byte val = 0;
            bool parseResult = byte.TryParse(s, style, provider, out val);

            enbyte.Set(val);
            result = enbyte;

            return parseResult;
        }

        public static bool TryParse(string s, out ebyte result)
        {
            ebyte enbyte = new ebyte(0);
            byte val = 0;
            bool parseResult = byte.TryParse(s, out val);

            enbyte.Set(val);
            result = enbyte;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private byte serializeValue;

        public void OnBeforeSerialize()
        {
            serializeValue = Get();
        }

        public void OnAfterDeserialize()
        {
        #if UNITY_EDITOR
            Set(serializeValue);

            Encrypt(serializeValue);
        #endif
        }
    #endregion
    }
}