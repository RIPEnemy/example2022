using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct eulong
    {
        public const UInt64 MaxValue = 18446744073709551615;
        public const UInt64 MinValue = 0;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public eulong(ulong value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public eulong(string value, TypeValue type)
        {
            autoEncrypt = false;
            ulong val = 0L;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<ulong>(value);
            serializeValue = val;
            Set(val);
        }
        
        public eulong(ulong value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private ulong Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? 0L : Сryptographer.Decode<ulong>(a + b);
            else
                return Сryptographer.Decrypt<ulong>(EncryptedValue);
        }

        private void Set(ulong value)
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
        public void Encrypt(ulong value)
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
        public static implicit operator ulong(eulong operand)
        {
            return operand.Get();
        }
        public static implicit operator float(eulong operand)
        {
            return operand.Get();
        }
        public static implicit operator double(eulong operand)
        {
            return operand.Get();
        }
        public static implicit operator decimal(eulong operand)
        {
            return operand.Get();
        }
        public static implicit operator eulong(char operand)
        {
            return new eulong(operand);
        }
        public static implicit operator eulong(byte operand)
        {
            return new eulong(operand);
        }
        public static implicit operator eulong(uint operand)
        {
            return new eulong(operand);
        }
        public static implicit operator eulong(ulong operand)
        {
            return new eulong(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator char(eulong operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(eulong operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(eulong operand)
        {
            return (sbyte)operand.Get();
        }
        public static explicit operator int(eulong operand)
        {
            return (int)operand.Get();
        }
        public static explicit operator uint(eulong operand)
        {
            return (uint)operand.Get();
        }
        public static explicit operator long(eulong operand)
        {
            return (long)operand.Get();
        }
    #endregion

    #region Math operators
        public static eulong operator +(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eulong operator +(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eulong operator +(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eulong operator +(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eulong operator ++(eulong left)
        {
            eulong result = left;
            result.Set(result.Get() + 1);
            return result;
        }
        public static eulong operator -(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eulong operator -(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eulong operator -(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eulong operator -(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eulong operator --(eulong left)
        {
            eulong result = left;
            result.Set(result.Get() - 1);
            return result;
        }
        public static eulong operator *(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eulong operator *(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eulong operator *(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eulong operator *(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eulong operator /(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eulong operator /(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eulong operator /(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eulong operator /(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eulong operator %(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eulong operator %(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eulong operator %(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eulong operator %(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eulong operator &(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eulong operator &(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eulong operator &(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eulong operator &(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eulong operator |(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static eulong operator |(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static eulong operator |(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static eulong operator |(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static eulong operator ^(eulong left, char right)
        {
            eulong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eulong operator ^(eulong left, byte right)
        {
            eulong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eulong operator ^(eulong left, uint right)
        {
            eulong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eulong operator ^(eulong left, ulong right)
        {
            eulong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eulong operator <<(eulong left, int right)
        {
            eulong result = left;
            result.Set(result.Get() << right);
            return result;
        }
        public static eulong operator >>(eulong left, int right)
        {
            eulong result = left;
            result.Set(result.Get() >> right);
            return result;
        }
    #endregion

    #region Parse
        public static eulong Parse(string s, IFormatProvider provider)
        {
            eulong result = new eulong(0L);

            if (!string.IsNullOrEmpty(s))
                result.Set(ulong.Parse(s, provider));

            return result;
        }

        public static eulong Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            eulong result = new eulong(0L);

            if (!string.IsNullOrEmpty(s))
                result.Set(ulong.Parse(s, style, provider));

            return result;
        }

        public static eulong Parse(string s, NumberStyles style)
        {
            eulong result = new eulong(0L);

            if (!string.IsNullOrEmpty(s))
                result.Set(ulong.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out eulong result)
        {
            eulong enlong = new eulong(0L);
            ulong val = 0L;
            bool parseResult = ulong.TryParse(s, style, provider, out val);

            enlong.Set(val);
            result = enlong;

            return parseResult;
        }

        public static bool TryParse(string s, out eulong result)
        {
            eulong enlong = new eulong(0L);
            ulong val = 0L;
            bool parseResult = ulong.TryParse(s, out val);

            enlong.Set(val);
            result = enlong;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private ulong serializeValue;

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