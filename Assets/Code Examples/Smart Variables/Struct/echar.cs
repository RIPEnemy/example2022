using UnityEngine;
using System;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct echar
    {
        public const Char MaxValue = '\uffff';
        public const Char MinValue = '\0';

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public echar(char value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public echar(string value, TypeValue type)
        {
            autoEncrypt = false;
            char val = default(char);
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<char>(value);
            serializeValue = val;
            Set(val);
        }
        
        public echar(char value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private char Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? default(char) : Сryptographer.Decode<char>(a + b);
            else
                return Сryptographer.Decrypt<char>(EncryptedValue);
        }

        private void Set(char value)
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
        public void Encrypt(char value)
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
    #endregion

    #region Implicit operatos
        public static implicit operator char(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator int(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator uint(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator long(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator ulong(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator float(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator double(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator decimal(echar operand)
        {
            return operand.Get();
        }
        public static implicit operator echar(char operand)
        {
            return new echar(operand);
        }
    #endregion

    #region Explicit operators
        public static explicit operator byte(echar operand)
        {
            return (byte)operand.Get();
        }
        public static explicit operator sbyte(echar operand)
        {
            return (sbyte)operand.Get();
        }
    #endregion

    #region Parse
        public static echar Parse(string s)
        {
            echar result = new echar(default(char));

            if (!string.IsNullOrEmpty(s))
                result.Set(char.Parse(s));

            return result;
        }

        public static bool TryParse(string s, out echar result)
        {
            echar enchar = new echar(default(char));
            char val = default(char);
            bool parseResult = char.TryParse(s, out val);

            enchar.Set(val);
            result = enchar;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private char serializeValue;

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