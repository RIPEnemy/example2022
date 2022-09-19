using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct ebool
    {
        public static readonly string FalseString = "False";
        public static readonly string TrueString = "True";

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public ebool(bool value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = false;
            Set(value);
        }

        public ebool(string value, TypeValue type)
        {
            autoEncrypt = false;
            bool val = false;
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<bool>(value);
            serializeValue = val;
            Set(val);
        }
        
        public ebool(bool value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value;
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private bool Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? false : Сryptographer.Decode<bool>(a + b);
            else
                return Сryptographer.Decrypt<bool>(EncryptedValue);
        }

        private void Set(bool value)
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
        public void Encrypt(bool value)
        {
            EncryptedValue = Сryptographer.Encrypt(value);
        }
    #endregion

    #region Implicit operatos
        public static implicit operator bool(ebool operand)
        {
            return operand.Get();
        }
        public static implicit operator ebool(bool operand)
        {
            return new ebool(operand);
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

    #region Parse
        public static ebool Parse(string s)
        {
            ebool result = new ebool(false);

            if (!string.IsNullOrEmpty(s))
                result.Set(bool.Parse(s));

            return result;
        }

        public static bool TryParse(string s, out ebool result)
        {
            ebool enbool = new ebool(false);
            bool val = false;
            bool parseResult = bool.TryParse(s, out val);

            enbool.Set(val);
            result = enbool;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private bool serializeValue;

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