using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct edecimal
    {
        public const Decimal MaxValue = 79228162514264337593543950335M;
        public const Decimal MinusOne = -1;
        public const Decimal MinValue = -79228162514264337593543950335M;
        public const Decimal One = 1;
        public const Decimal Zero = 0;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public edecimal(decimal value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public edecimal(string value, TypeValue type)
        {
            autoEncrypt = false;
            decimal val = 0m;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<decimal>(value);
            serializeValue = val;
            Set(val);
        }
        
        public edecimal(decimal value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private decimal Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? 0m : Сryptographer.Decode<decimal>(a + b);
            else
                return Сryptographer.Decrypt<decimal>(EncryptedValue);
        }

        private void Set(decimal value)
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
        public void Encrypt(decimal value)
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
        public static implicit operator decimal(edecimal operand)
        {
            return operand.Get();
        }
        public static implicit operator edecimal(char operand)
        {
            return new edecimal(operand);
        }
        public static implicit operator edecimal(byte operand)
        {
            return new edecimal(operand);
        }
        public static implicit operator edecimal(sbyte operand)
        {
            return new edecimal(operand);
        }
        public static implicit operator edecimal(int operand)
        {
            return new edecimal(operand);
        }
        public static implicit operator edecimal(uint operand)
        {
            return new edecimal(operand);
        }
        public static implicit operator edecimal(long operand)
        {
            return new edecimal(operand);
        }
        public static implicit operator edecimal(ulong operand)
        {
            return new edecimal(operand);
        }
        public static implicit operator edecimal(decimal operand)
        {
            return new edecimal(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator char(edecimal operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(edecimal operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(edecimal operand)
        {
            return (sbyte)operand.Get();
        }
        public static explicit operator int(edecimal operand)
        {
            return (int)operand.Get();
        }
        public static explicit operator uint(edecimal operand)
        {
            return (uint)operand.Get();
        }
        public static explicit operator long(edecimal operand)
        {
            return (long)operand.Get();
        }
        public static explicit operator ulong(edecimal operand)
        {
            return (ulong)operand.Get();
        }
        public static explicit operator float(edecimal operand)
        {
            return (float)operand.Get();
        }
        public static explicit operator double(edecimal operand)
        {
            return (double)operand.Get();
        }
    #endregion

    #region Math operators
        public static edecimal operator +(edecimal left, char right)
        {
            edecimal result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static edecimal operator +(edecimal left, byte right)
        {
            edecimal result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static edecimal operator +(edecimal left, sbyte right)
        {
            edecimal result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static edecimal operator +(edecimal left, int right)
        {
            edecimal result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static edecimal operator ++(edecimal left)
        {
            edecimal result = left;
            result.Set(result.Get() + 1);
            return result;
        }
        public static edecimal operator -(edecimal left, char right)
        {
            edecimal result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static edecimal operator -(edecimal left, byte right)
        {
            edecimal result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static edecimal operator -(edecimal left, sbyte right)
        {
            edecimal result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static edecimal operator -(edecimal left, int right)
        {
            edecimal result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static edecimal operator --(edecimal left)
        {
            edecimal result = left;
            result.Set(result.Get() - 1);
            return result;
        }
        public static edecimal operator *(edecimal left, char right)
        {
            edecimal result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edecimal operator *(edecimal left, byte right)
        {
            edecimal result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edecimal operator *(edecimal left, sbyte right)
        {
            edecimal result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edecimal operator *(edecimal left, int right)
        {
            edecimal result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edecimal operator /(edecimal left, char right)
        {
            edecimal result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edecimal operator /(edecimal left, byte right)
        {
            edecimal result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edecimal operator /(edecimal left, sbyte right)
        {
            edecimal result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edecimal operator /(edecimal left, int right)
        {
            edecimal result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edecimal operator %(edecimal left, char right)
        {
            edecimal result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edecimal operator %(edecimal left, byte right)
        {
            edecimal result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edecimal operator %(edecimal left, sbyte right)
        {
            edecimal result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edecimal operator %(edecimal left, int right)
        {
            edecimal result = left;
            result.Set(result.Get() % right);
            return result;
        }
    #endregion

    #region Parse
        public static edecimal Parse(string s, IFormatProvider provider)
        {
            edecimal result = new edecimal(0m);

            if (!string.IsNullOrEmpty(s))
                result.Set(decimal.Parse(s, provider));

            return result;
        }

        public static edecimal Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            edecimal result = new edecimal(0m);

            if (!string.IsNullOrEmpty(s))
                result.Set(decimal.Parse(s, style, provider));

            return result;
        }

        public static edecimal Parse(string s, NumberStyles style)
        {
            edecimal result = new edecimal(0m);

            if (!string.IsNullOrEmpty(s))
                result.Set(decimal.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out edecimal result)
        {
            edecimal endecimal = new edecimal(0m);
            decimal val = 0m;
            bool parseResult = decimal.TryParse(s, style, provider, out val);

            endecimal.Set(val);
            result = endecimal;

            return parseResult;
        }

        public static bool TryParse(string s, out edecimal result)
        {
            edecimal endecimal = new edecimal(0m);
            decimal val = 0m;
            bool parseResult = decimal.TryParse(s, out val);

            endecimal.Set(val);
            result = endecimal;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private decimal serializeValue;

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