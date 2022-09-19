using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct elong
    {
        public const Int64 MaxValue = 9223372036854775807;
        public const Int64 MinValue = -9223372036854775808;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public elong(long value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public elong(string value, TypeValue type)
        {
            autoEncrypt = false;
            long val = 0L;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<long>(value);
            serializeValue = val;
            Set(val);
        }
        
        public elong(long value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private long Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? 0L : Сryptographer.Decode<long>(a + b);
            else
                return Сryptographer.Decrypt<long>(EncryptedValue);
        }

        private void Set(long value)
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
        public void Encrypt(long value)
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
        public static implicit operator long(elong operand)
        {
            return operand.Get();
        }
        public static implicit operator float(elong operand)
        {
            return operand.Get();
        }
        public static implicit operator double(elong operand)
        {
            return operand.Get();
        }
        public static implicit operator decimal(elong operand)
        {
            return operand.Get();
        }
        public static implicit operator elong(char operand)
        {
            return new elong(operand);
        }
        public static implicit operator elong(byte operand)
        {
            return new elong(operand);
        }
        public static implicit operator elong(sbyte operand)
        {
            return new elong(operand);
        }
        public static implicit operator elong(int operand)
        {
            return new elong(operand);
        }
        public static implicit operator elong(uint operand)
        {
            return new elong(operand);
        }
        public static implicit operator elong(long operand)
        {
            return new elong(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator char(elong operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(elong operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(elong operand)
        {
            return (sbyte)operand.Get();
        }
        public static explicit operator int(elong operand)
        {
            return (int)operand.Get();
        }
        public static explicit operator uint(elong operand)
        {
            return (uint)operand.Get();
        }
        public static explicit operator ulong(elong operand)
        {
            return (ulong)operand.Get();
        }
    #endregion

    #region Math operators
        public static elong operator +(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static elong operator +(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static elong operator +(elong left, sbyte right)
        {
            elong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static elong operator +(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static elong operator ++(elong left)
        {
            elong result = left;
            result.Set(result.Get() + 1);
            return result;
        }
        public static elong operator -(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static elong operator -(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static elong operator -(elong left, sbyte right)
        {
            elong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static elong operator -(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static elong operator --(elong left)
        {
            elong result = left;
            result.Set(result.Get() - 1);
            return result;
        }
        public static elong operator *(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static elong operator *(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static elong operator *(elong left, sbyte right)
        {
            elong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static elong operator *(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static elong operator /(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static elong operator /(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static elong operator /(elong left, sbyte right)
        {
            elong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static elong operator /(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static elong operator %(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static elong operator %(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static elong operator %(elong left, sbyte right)
        {
            elong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static elong operator %(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static elong operator &(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static elong operator &(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static elong operator &(elong left, sbyte right)
        {
            elong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static elong operator &(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static elong operator |(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static elong operator |(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static elong operator ^(elong left, char right)
        {
            elong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static elong operator ^(elong left, byte right)
        {
            elong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static elong operator ^(elong left, sbyte right)
        {
            elong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static elong operator ^(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static elong operator <<(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() << right);
            return result;
        }
        public static elong operator >>(elong left, int right)
        {
            elong result = left;
            result.Set(result.Get() >> right);
            return result;
        }
    #endregion

    #region Parse
        public static elong Parse(string s, IFormatProvider provider)
        {
            elong result = new elong(0L);

            if (!string.IsNullOrEmpty(s))
                result.Set(long.Parse(s, provider));

            return result;
        }

        public static elong Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            elong result = new elong(0L);

            if (!string.IsNullOrEmpty(s))
                result.Set(long.Parse(s, style, provider));

            return result;
        }

        public static elong Parse(string s, NumberStyles style)
        {
            elong result = new elong(0L);

            if (!string.IsNullOrEmpty(s))
                result.Set(long.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out elong result)
        {
            elong enlong = new elong(0L);
            long val = 0L;
            bool parseResult = long.TryParse(s, style, provider, out val);

            enlong.Set(val);
            result = enlong;

            return parseResult;
        }

        public static bool TryParse(string s, out elong result)
        {
            elong enlong = new elong(0L);
            long val = 0L;
            bool parseResult = long.TryParse(s, out val);

            enlong.Set(val);
            result = enlong;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private long serializeValue;

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