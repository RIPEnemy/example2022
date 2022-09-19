using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct edouble
    {
        public const Double Epsilon = 4.94065645841247E-324;
        public const Double MaxValue = 1.7976931348623157E+308;
        public const Double MinValue = -1.7976931348623157E+308;
        public const Double NaN = 0D / 0D;
        public const Double NegativeInfinity = -1D / 0D;
        public const Double PositiveInfinity = 1D / 0D;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public edouble(double value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public edouble(string value, TypeValue type)
        {
            autoEncrypt = false;
            double val = 0d;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<double>(value);
            serializeValue = val;
            Set(val);
        }
        
        public edouble(double value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private double Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? 0d : Сryptographer.Decode<double>(a + b);
            else
                return Сryptographer.Decrypt<double>(EncryptedValue);
        }

        private void Set(double value)
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
        public void Encrypt(double value)
        {
            EncryptedValue = Сryptographer.Encrypt(value);
        }
    #endregion

    #region Double methods
        public static bool IsInfinity(Double d)
        {
            return double.IsInfinity(d);
        }
        public static bool IsInfinity(edouble d)
        {
            return double.IsInfinity(d.Get());
        }
        public static bool IsNaN(Double d)
        {
            return double.IsNaN(d);
        }
        public static bool IsNaN(edouble d)
        {
            return double.IsNaN(d.Get());
        }
        public static bool IsNegativeInfinity(Double d)
        {
            return double.IsNegativeInfinity(d);
        }
        public static bool IsNegativeInfinity(edouble d)
        {
            return double.IsNegativeInfinity(d.Get());
        }
        public static bool IsPositiveInfinity(Double d)
        {
            return double.IsPositiveInfinity(d);
        }
        public static bool IsPositiveInfinity(edouble d)
        {
            return double.IsPositiveInfinity(d.Get());
        }
    #endregion

    #region Implicit operatos
        public static implicit operator double(edouble operand)
        {
            return operand.Get();
        }
        public static implicit operator edouble(byte operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(sbyte operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(char operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(int operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(uint operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(long operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(ulong operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(float operand)
        {
            return new edouble(operand);
        }
        public static implicit operator edouble(double operand)
        {
            return new edouble(operand);
        }
    #endregion

    #region Explicit operatos
        public static explicit operator char(edouble operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(edouble operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(edouble operand)
        {
            return (sbyte)operand.Get();
        }
        public static explicit operator int(edouble operand)
        {
            return (int)operand.Get();
        }
        public static explicit operator uint(edouble operand)
        {
            return (uint)operand.Get();
        }
        public static explicit operator long(edouble operand)
        {
            return (long)operand.Get();
        }
        public static explicit operator ulong(edouble operand)
        {
            return (ulong)operand.Get();
        }
        public static explicit operator float(edouble operand)
        {
            return (float)operand.Get();
        }
        public static explicit operator decimal(edouble operand)
        {
            return (decimal)operand.Get();
        }
    #endregion

    #region Math operators
        public static edouble operator +(edouble left, char right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator +(edouble left, byte right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator +(edouble left, sbyte right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator +(edouble left, int right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator +(edouble left, uint right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator +(edouble left, long right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator +(edouble left, ulong right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator +(edouble left, float right)
        {
            edouble result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static edouble operator ++(edouble left)
        {
            edouble result = left;
            result.Set(left.Get() + 1f);
            return result;
        }
        public static edouble operator -(edouble left, char right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator -(edouble left, byte right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator -(edouble left, sbyte right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator -(edouble left, int right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator -(edouble left, uint right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator -(edouble left, long right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator -(edouble left, ulong right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator -(edouble left, float right)
        {
            edouble result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static edouble operator --(edouble left)
        {
            edouble result = left;
            result.Set(left.Get() - 1f);
            return result;
        }
        public static edouble operator *(edouble left, char right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator *(edouble left, byte right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator *(edouble left, sbyte right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator *(edouble left, int right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator *(edouble left, uint right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator *(edouble left, long right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator *(edouble left, ulong right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator *(edouble left, float right)
        {
            edouble result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static edouble operator /(edouble left, char right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator /(edouble left, byte right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator /(edouble left, sbyte right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator /(edouble left, int right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator /(edouble left, uint right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator /(edouble left, long right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator /(edouble left, ulong right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator /(edouble left, float right)
        {
            edouble result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static edouble operator %(edouble left, char right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edouble operator %(edouble left, byte right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edouble operator %(edouble left, sbyte right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edouble operator %(edouble left, int right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edouble operator %(edouble left, uint right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edouble operator %(edouble left, long right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edouble operator %(edouble left, ulong right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static edouble operator %(edouble left, float right)
        {
            edouble result = left;
            result.Set(result.Get() % right);
            return result;
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
    
    #region Parse
        public static edouble Parse(string s, IFormatProvider provider)
        {
            edouble result = new edouble(0f);

            if (!string.IsNullOrEmpty(s))
                result.Set(float.Parse(s, provider));

            return result;
        }

        public static edouble Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            edouble result = new edouble(0f);

            if (!string.IsNullOrEmpty(s))
                result.Set(float.Parse(s, style, provider));

            return result;
        }

        public static edouble Parse(string s, NumberStyles style)
        {
            edouble result = new edouble(0f);

            if (!string.IsNullOrEmpty(s))
                result.Set(float.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out edouble result)
        {
            edouble endouble = new edouble(0f);
            float val = 0f;
            bool parseResult = float.TryParse(s, style, provider, out val);

            endouble.Set(val);
            result = endouble;

            return parseResult;
        }

        public static bool TryParse(string s, out edouble result)
        {
            edouble endouble = new edouble(0f);
            float val = 0f;
            bool parseResult = float.TryParse(s, out val);

            endouble.Set(val);
            result = endouble;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private double serializeValue;

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