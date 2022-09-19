using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct esbyte
    {
        public const SByte MaxValue = 127;
        public const SByte MinValue = -128;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public esbyte(sbyte value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public esbyte(string value, TypeValue type)
        {
            autoEncrypt = false;
            sbyte val = 0;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<sbyte>(value);
            serializeValue = val;
            Set(val);
        }
        
        public esbyte(sbyte value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private sbyte Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? (sbyte)0 : Сryptographer.Decode<sbyte>(a + b);
            else
                return Сryptographer.Decrypt<sbyte>(EncryptedValue);
        }

        private void Set(sbyte value)
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
        public void Encrypt(sbyte value)
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
        public static implicit operator sbyte(esbyte operand)
        {
            return operand.Get();
        }
        public static implicit operator int(esbyte operand)
        {
            return operand.Get();
        }
        public static implicit operator long(esbyte operand)
        {
            return operand.Get();
        }
        public static implicit operator float(esbyte operand)
        {
            return operand.Get();
        }
        public static implicit operator double(esbyte operand)
        {
            return operand.Get();
        }
        public static implicit operator decimal(esbyte operand)
        {
            return operand.Get();
        }
        public static implicit operator esbyte(sbyte operand)
        {
            return new esbyte(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator char(esbyte operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(esbyte operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator uint(esbyte operand)
        {
            return (uint)operand.Get();
        }
        public static explicit operator ulong(esbyte operand)
        {
            return (ulong)operand.Get();
        }
    #endregion

    #region Parse
        public static esbyte Parse(string s, IFormatProvider provider)
        {
            esbyte result = new esbyte(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(sbyte.Parse(s, provider));

            return result;
        }

        public static esbyte Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            esbyte result = new esbyte(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(sbyte.Parse(s, style, provider));

            return result;
        }

        public static esbyte Parse(string s, NumberStyles style)
        {
            esbyte result = new esbyte(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(sbyte.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out esbyte result)
        {
            esbyte ensbyte = new esbyte(0);
            sbyte val = 0;
            bool parseResult = sbyte.TryParse(s, style, provider, out val);

            ensbyte.Set(val);
            result = ensbyte;

            return parseResult;
        }

        public static bool TryParse(string s, out esbyte result)
        {
            esbyte ensbyte = new esbyte(0);
            sbyte val = 0;
            bool parseResult = sbyte.TryParse(s, out val);

            ensbyte.Set(val);
            result = ensbyte;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private sbyte serializeValue;

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