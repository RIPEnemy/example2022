using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct eint
    {
        public const Int32 MaxValue = 2147483647;
        public const Int32 MinValue = -2147483648;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public eint(int value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public eint(string value, TypeValue type)
        {
            autoEncrypt = false;
            int val = 0;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<int>(value);
            serializeValue = val;
            Set(val);
        }
        
        public eint(int value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private int Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? 0 : Сryptographer.Decode<int>(a + b);
            else
                return Сryptographer.Decrypt<int>(EncryptedValue);
        }

        private void Set(int value)
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
        public void Encrypt(int value)
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
        public static implicit operator int(eint operand)
        {
            return operand.Get();
        }
        public static implicit operator long(eint operand)
        {
            return operand.Get();
        }
        public static implicit operator float(eint operand)
        {
            return operand.Get();
        }
        public static implicit operator double(eint operand)
        {
            return operand.Get();
        }
        public static implicit operator decimal(eint operand)
        {
            return operand.Get();
        }
        public static implicit operator eint(char operand)
        {
            return new eint(operand);
        }
        public static implicit operator eint(byte operand)
        {
            return new eint(operand);
        }
        public static implicit operator eint(sbyte operand)
        {
            return new eint(operand);
        }
        public static implicit operator eint(int operand)
        {
            return new eint(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator char(eint operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(eint operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(eint operand)
        {
            return (sbyte)operand.Get();
        }
        public static explicit operator uint(eint operand)
        {
            return (uint)operand.Get();
        }
        public static explicit operator ulong(eint operand)
        {
            return (ulong)operand.Get();
        }
    #endregion

    #region Math operators
        public static eint operator +(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eint operator +(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eint operator +(eint left, sbyte right)
        {
            eint result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eint operator +(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static eint operator ++(eint left)
        {
            eint result = left;
            result.Set(result.Get() + 1);
            return result;
        }
        public static eint operator -(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eint operator -(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eint operator -(eint left, sbyte right)
        {
            eint result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eint operator -(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static eint operator --(eint left)
        {
            eint result = left;
            result.Set(result.Get() - 1);
            return result;
        }
        public static eint operator *(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eint operator *(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eint operator *(eint left, sbyte right)
        {
            eint result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eint operator *(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static eint operator /(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eint operator /(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eint operator /(eint left, sbyte right)
        {
            eint result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eint operator /(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static eint operator %(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eint operator %(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eint operator %(eint left, sbyte right)
        {
            eint result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eint operator %(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static eint operator &(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eint operator &(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eint operator &(eint left, sbyte right)
        {
            eint result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eint operator &(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static eint operator |(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static eint operator |(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static eint operator |(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static eint operator ^(eint left, char right)
        {
            eint result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eint operator ^(eint left, byte right)
        {
            eint result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eint operator ^(eint left, sbyte right)
        {
            eint result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eint operator ^(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static eint operator <<(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() << right);
            return result;
        }
        public static eint operator >>(eint left, int right)
        {
            eint result = left;
            result.Set(result.Get() >> right);
            return result;
        }
    #endregion

    #region Parse
        public static eint Parse(string s, IFormatProvider provider)
        {
            eint result = new eint(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(int.Parse(s, provider));

            return result;
        }

        public static eint Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            eint result = new eint(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(int.Parse(s, style, provider));

            return result;
        }

        public static eint Parse(string s, NumberStyles style)
        {
            eint result = new eint(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(int.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out eint result)
        {
            eint enint = new eint(0);
            int val = 0;
            bool parseResult = int.TryParse(s, style, provider, out val);

            enint.Set(val);
            result = enint;

            return parseResult;
        }

        public static bool TryParse(string s, out eint result)
        {
            eint enint = new eint(0);
            int val = 0;
            bool parseResult = int.TryParse(s, out val);

            enint.Set(val);
            result = enint;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private int serializeValue;

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