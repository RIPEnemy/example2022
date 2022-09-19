using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct euint
    {
        public const UInt32 MaxValue = 4294967295;
        public const UInt32 MinValue = 0;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public euint(uint value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public euint(string value, TypeValue type)
        {
            autoEncrypt = false;
            uint val = 0;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<uint>(value);
            serializeValue = val;
            Set(val);
        }
        
        public euint(uint value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private uint Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? 0 : Сryptographer.Decode<uint>(a + b);
            else
                return Сryptographer.Decrypt<uint>(EncryptedValue);
        }

        private void Set(uint value)
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
        public void Encrypt(uint value)
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
        public static implicit operator uint(euint operand)
        {
            return operand.Get();
        }
        public static implicit operator long(euint operand)
        {
            return operand.Get();
        }
        public static implicit operator ulong(euint operand)
        {
            return operand.Get();
        }
        public static implicit operator float(euint operand)
        {
            return operand.Get();
        }
        public static implicit operator double(euint operand)
        {
            return operand.Get();
        }
        public static implicit operator decimal(euint operand)
        {
            return operand.Get();
        }
        public static implicit operator euint(char operand)
        {
            return new euint(operand);
        }
        public static implicit operator euint(byte operand)
        {
            return new euint(operand);
        }
         public static implicit operator euint(uint operand)
        {
            return new euint(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator char(euint operand)
        {
            return (char)operand.Get();
        }
        public static explicit operator byte(euint operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(euint operand)
        {
            return (sbyte)operand.Get();
        }
        public static explicit operator int(euint operand)
        {
            return (int)operand.Get();
        }
    #endregion

    #region Math operators
        public static euint operator +(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static euint operator +(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static euint operator +(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() + right);
            return result;
        }
        public static euint operator ++(euint left)
        {
            euint result = left;
            result.Set(result.Get() + 1);
            return result;
        }
        public static euint operator -(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static euint operator -(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static euint operator -(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() - right);
            return result;
        }
        public static euint operator --(euint left)
        {
            euint result = left;
            result.Set(result.Get() - 1);
            return result;
        }
        public static euint operator *(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static euint operator *(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static euint operator *(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() * right);
            return result;
        }
        public static euint operator /(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static euint operator /(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static euint operator /(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() / right);
            return result;
        }
        public static euint operator %(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static euint operator %(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static euint operator %(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() % right);
            return result;
        }
        public static euint operator &(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static euint operator &(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static euint operator &(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() & right);
            return result;
        }
        public static euint operator |(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static euint operator |(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static euint operator |(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() | right);
            return result;
        }
        public static euint operator ^(euint left, char right)
        {
            euint result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static euint operator ^(euint left, byte right)
        {
            euint result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static euint operator ^(euint left, uint right)
        {
            euint result = left;
            result.Set(result.Get() ^ right);
            return result;
        }
        public static euint operator <<(euint left, int right)
        {
            euint result = left;
            result.Set(result.Get() << right);
            return result;
        }
        public static euint operator >>(euint left, int right)
        {
            euint result = left;
            result.Set(result.Get() >> right);
            return result;
        }
    #endregion

    #region Parse
        public static euint Parse(string s, IFormatProvider provider)
        {
            euint result = new euint(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(uint.Parse(s, provider));

            return result;
        }

        public static euint Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            euint result = new euint(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(uint.Parse(s, style, provider));

            return result;
        }

        public static euint Parse(string s, NumberStyles style)
        {
            euint result = new euint(0);

            if (!string.IsNullOrEmpty(s))
                result.Set(uint.Parse(s, style));

            return result;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out euint result)
        {
            euint enuint = new euint(0);
            uint val = 0;
            bool parseResult = uint.TryParse(s, style, provider, out val);

            enuint.Set(val);
            result = enuint;

            return parseResult;
        }

        public static bool TryParse(string s, out euint result)
        {
            euint enuint = new euint(0);
            uint val = 0;
            bool parseResult = uint.TryParse(s, out val);

            enuint.Set(val);
            result = enuint;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private uint serializeValue;

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