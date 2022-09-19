using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct efloat
    {
        public const Single Epsilon = 1.401298E-45F;
        public const Single MaxValue = 3.40282347E+38F;
        public const Single MinValue = -3.40282347E+38F;
        public const Single NaN = 0F / 0F;
        public const Single NegativeInfinity = -1F / 0F;
        public const Single PositiveInfinity = 1F / 0F;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public efloat(float value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public efloat(string value, TypeValue type)
        {
            autoEncrypt = false;
            float val = 0f;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<float>(value);
            serializeValue = val;
            Set(val);
        }
        
        public efloat(float value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private float Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? 0f : Сryptographer.Decode<float>(a + b);
            else
                return Сryptographer.Decrypt<float>(EncryptedValue);
        }

        private void Set(float value)
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
        public void Encrypt(float value)
        {
            EncryptedValue = Сryptographer.Encrypt(value);
        }
    #endregion

    #region Float methods
        public static bool IsInfinity(Single f)
        {
            return float.IsInfinity(f);
        }
        public static bool IsInfinity(efloat f)
        {
            return float.IsInfinity(f.Get());
        }
        public static bool IsNaN(Single f)
        {
            return float.IsNaN(f);
        }
        public static bool IsNaN(efloat f)
        {
            return float.IsNaN(f.Get());
        }
        public static bool IsNegativeInfinity(Single f)
        {
            return float.IsNegativeInfinity(f);
        }
        public static bool IsNegativeInfinity(efloat f)
        {
            return float.IsNegativeInfinity(f.Get());
        }
        public static bool IsPositiveInfinity(Single f)
        {
            return float.IsPositiveInfinity(f);
        }
        public static bool IsPositiveInfinity(efloat f)
        {
            return float.IsPositiveInfinity(f.Get());
        }
    #endregion

    #region Implicit operatos
        public static implicit operator float(efloat operand)
        {
            return operand.Get();
        }
        public static implicit operator double(efloat operand)
        {
            return operand.Get();
        }
        public static implicit operator efloat(byte operand)
        {
            return new efloat(operand);
        }
        public static implicit operator efloat(sbyte operand)
        {
            return new efloat(operand);
        }
        public static implicit operator efloat(char operand)
        {
            return new efloat(operand);
        }
        public static implicit operator efloat(int operand)
        {
            return new efloat(operand);
        }
        public static implicit operator efloat(uint operand)
        {
            return new efloat(operand);
        }
        public static implicit operator efloat(long operand)
        {
            return new efloat(operand);
        }
        public static implicit operator efloat(ulong operand)
        {
            return new efloat(operand);
        }
        public static implicit operator efloat(float operand)
        {
            return new efloat(operand);
        }
    #endregion

    #region Explicit operatos
        public static explicit operator char(efloat operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(efloat operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(efloat operand)
        {
            return (sbyte)operand.Get();
        }
        public static explicit operator int(efloat operand)
        {
            return (int)operand.Get();
        }
        public static explicit operator uint(efloat operand)
        {
            return (uint)operand.Get();
        }
        public static explicit operator long(efloat operand)
        {
            return (long)operand.Get();
        }
        public static explicit operator ulong(efloat operand)
        {
            return (ulong)operand.Get();
        }
        public static explicit operator decimal(efloat operand)
        {
            return (decimal)operand.Get();
        }
    #endregion

    #region Math operators
        public static efloat operator +(efloat left, char right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator +(efloat left, byte right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator +(efloat left, sbyte right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator +(efloat left, int right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator +(efloat left, uint right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator +(efloat left, long right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator +(efloat left, ulong right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator +(efloat left, float right)
        {
            efloat result = left;
            result.Set(left.Get() + right);
            return result;
        }
        public static efloat operator ++(efloat left)
        {
            efloat result = left;
            result.Set(left.Get() + 1f);
            return result;
        }
        public static efloat operator -(efloat left, char right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator -(efloat left, byte right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator -(efloat left, sbyte right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator -(efloat left, int right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator -(efloat left, uint right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator -(efloat left, long right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator -(efloat left, ulong right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator -(efloat left, float right)
        {
            efloat result = left;
            result.Set(left.Get() - right);
            return result;
        }
        public static efloat operator --(efloat left)
        {
            efloat result = left;
            result.Set(left.Get() - 1f);
            return result;
        }
        public static efloat operator *(efloat left, char right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator *(efloat left, byte right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator *(efloat left, sbyte right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator *(efloat left, int right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator *(efloat left, uint right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator *(efloat left, long right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator *(efloat left, ulong right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator *(efloat left, float right)
        {
            efloat result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static efloat operator /(efloat left, char right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator /(efloat left, byte right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator /(efloat left, sbyte right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator /(efloat left, int right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator /(efloat left, uint right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator /(efloat left, long right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator /(efloat left, ulong right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator /(efloat left, float right)
        {
            efloat result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static efloat operator %(efloat left, char right)
        {
            efloat result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static efloat operator %(efloat left, byte right)
        {
            efloat result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static efloat operator %(efloat left, sbyte right)
        {
            efloat result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static efloat operator %(efloat left, int right)
        {
            efloat result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static efloat operator %(efloat left, uint right)
        {
            efloat result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static efloat operator %(efloat left, long right)
        {
            efloat result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static efloat operator %(efloat left, ulong right)
        {
            efloat result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static efloat operator %(efloat left, float right)
        {
            efloat result = left;
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
        public static efloat Parse(string s, IFormatProvider provider)
        {
            efloat result = new efloat(0f);

            if (!string.IsNullOrEmpty(s))
                result.Set(float.Parse(s, provider));

            return result;
        }

        public static efloat Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            efloat result = new efloat(0f);

            if (!string.IsNullOrEmpty(s))
                result.Set(float.Parse(s, style, provider));

            return result;
        }

        public static efloat Parse(string s, NumberStyles style)
        {
            efloat result = new efloat(0f);

            if (!string.IsNullOrEmpty(s))
                result.Set(float.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out efloat result)
        {
            efloat enfloat = new efloat(0f);
            float val = 0f;
            bool parseResult = float.TryParse(s, style, provider, out val);

            enfloat.Set(val);
            result = enfloat;

            return parseResult;
        }

        public static bool TryParse(string s, out efloat result)
        {
            efloat enfloat = new efloat(0f);
            float val = 0f;
            bool parseResult = float.TryParse(s, out val);

            enfloat.Set(val);
            result = enfloat;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private float serializeValue;

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