using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    public struct estring
    {
        public static readonly String Empty = "";
        public echar this[int index] { get { return new echar(Get()[index]); } }
        public int Length { get { return Get().Length; } }

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public estring(string value, TypeValue type = TypeValue.Normal)
        {
            autoEncrypt = false;
            string val = "";
            a = b = EncryptedValue = serializeValue = "";
            
            if (type == TypeValue.Encoded)
            {
                val = Сryptographer.Decode<string>(value);
            }
            else if (type == TypeValue.Encrypted)
            {
                EncryptedValue = value;
                val = Сryptographer.Decrypt<string>(value);
            }
            else
            {
                val = value;
            }

            serializeValue = val;
            Set(val);
        }

        public estring(string value, bool allowAutoEncrypt, TypeValue type = TypeValue.Normal)
        {
            autoEncrypt = allowAutoEncrypt;
            string val = "";
            a = b = EncryptedValue = serializeValue = "";
            
            if (type == TypeValue.Encoded)
            {
                val = Сryptographer.Decode<string>(value);
            }
            else if (type == TypeValue.Encrypted)
            {
                EncryptedValue = value;
                val = Сryptographer.Decrypt<string>(value);
            }
            else
            {
                val = value;
            }

            serializeValue = val;
            Set(val);
        }

        public estring(char[] value, TypeValue type = TypeValue.Normal)
        {
            autoEncrypt = false;
            string val = "";
            string str = new String(value);
            a = b = EncryptedValue = serializeValue = "";
            
            if (type == TypeValue.Encoded)
            {
                val = Сryptographer.Decode<string>(str);
            }
            else if (type == TypeValue.Encrypted)
            {
                EncryptedValue = str;
                val = Сryptographer.Decrypt<string>(str);
            }
            else
            {
                val = str;
            }

            serializeValue = val;
            Set(val);
        }

        public estring(char[] value, bool allowAutoEncrypt, TypeValue type = TypeValue.Normal)
        {
            autoEncrypt = allowAutoEncrypt;
            string val = "";
            string str = new String(value);
            a = b = EncryptedValue = serializeValue = "";
            
            if (type == TypeValue.Encoded)
            {
                val = Сryptographer.Decode<string>(str);
            }
            else if (type == TypeValue.Encrypted)
            {
                EncryptedValue = str;
                val = Сryptographer.Decrypt<string>(str);
            }
            else
            {
                val = str;
            }

            serializeValue = val;
            Set(val);
        }
    #endregion

    #region Get / Set
        private string Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? "" : Сryptographer.Decode<string>(a + b);
            else
                return Сryptographer.Decrypt<string>(EncryptedValue);
        }

        private void Set(string value)
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
        public void Encrypt(string value)
        {
            EncryptedValue = Сryptographer.Encrypt(value);
        }
    #endregion

    #region Implicit operators
        public static implicit operator estring(string str)
        {
            return new estring(str, TypeValue.Normal);
        }
        public static implicit operator string(estring str)
        {
            return str.Get();
        }
    #endregion

    #region String methods
        public static estring Concat(String str0, String str1)
        {
            return new estring(String.Concat(str0, str1));
        }
        public static estring Concat(String str0, String str1, String str2)
        {
            return new estring(String.Concat(str0, str1, str2));
        }
        public static estring Concat(String str0, String str1, String str2, String str3)
        {
            return new estring(String.Concat(str0, str1, str2, str3));
        }
        public static estring Concat<T>(IEnumerable<T> values)
        {
            return new estring(String.Concat(values));
        }
        public static estring Concat(IEnumerable<String> values)
        {
            return new estring(String.Concat(values));
        }
        public static estring Concat(params String[] values)
        {
            return new estring(String.Concat(values), TypeValue.Normal);
        }
        public static estring Concat(object arg0)
        {
            return new estring(String.Concat(arg0), TypeValue.Normal);
        }
        public static estring Concat(object arg0, object arg1)
        {
            return new estring(String.Concat(arg0, arg1), TypeValue.Normal);
        }
        public static estring Concat(object arg0, object arg1, object arg2)
        {
            return new estring(String.Concat(arg0, arg1, arg2), TypeValue.Normal);
        }
        public static estring Concat(params object[] args)
        {
            return new estring(String.Concat(args), TypeValue.Normal);
        }
        public static estring Copy(String str)
        {
            return new estring(str, TypeValue.Normal);
        }
        public static estring Format(String format, object arg0)
        {
            return new estring(String.Format(format, arg0));
        }
        public static estring Format(String format, object arg0, object arg1)
        {
            return new estring(String.Format(format, arg0, arg1));
        }
        public static estring Format(String format, object arg0, object arg1, object arg2)
        {
            return new estring(String.Format(format, arg0, arg1, arg2));
        }
        public static estring Format(String format, params object[] args)
        {
            return new estring(String.Format(format, args));
        }
        public static estring Format(IFormatProvider provider, String format, object arg0)
        {
            return new estring(String.Format(provider, format, arg0));
        }
        public static estring Format(IFormatProvider provider, String format, object arg0, object arg1)
        {
            return new estring(String.Format(provider, format, arg0, arg1));
        }
        public static estring Format(IFormatProvider provider, String format, object arg0, object arg1, object arg2)
        {
            return new estring(String.Format(provider, format, arg0, arg1, arg2));
        }
        public static estring Format(IFormatProvider provider, String format, params object[] args)
        {
            return new estring(String.Format(provider, format, args));
        }
        public static estring Intern(String str)
        {
            return new estring(String.Intern(str));
        }
        public static estring IsInterned(String str)
        {
            return new estring(String.IsInterned(str));
        }
        public static bool IsNullOrEmpty(String value)
        {
            return String.IsNullOrEmpty(value);
        }
        public static bool IsNullOrWhiteSpace(String value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
        public static estring Join(String separator, IEnumerable<String> values)
        {
            return new estring(String.Join(separator, values));
        }
        public static estring Join(String separator, params object[] values)
        {
            return new estring(String.Join(separator, values));
        }
        public static estring Join(String separator, params String[] value)
        {
            return new estring(String.Join(separator, value));
        }
        public static estring Join(String separator, String[] value, int startIndex, int count)
        {
            return new estring(String.Join(separator, value, startIndex, count));
        }
        public static estring Join<T>(String separator, IEnumerable<T> values)
        {
            return new estring(String.Join(separator, values));
        }
        public bool Contains(String value)
        {
            return Get().Contains(value);
        }
        public bool EndsWith(String value, StringComparison comparisonType)
        {
            return Get().EndsWith(value, comparisonType);
        }
        public bool EndsWith(String value, bool ignoreCase, CultureInfo culture)
        {
            return Get().EndsWith(value, ignoreCase, culture);
        }
        public bool EndsWith(String value)
        {
            return Get().EndsWith(value);
        }
        public CharEnumerator GetEnumerator()
        {
            return Get().GetEnumerator();
        }
        public int IndexOf(String value)
        {
            return Get().IndexOf(value);
        }
        public int IndexOf(String value, int startIndex, StringComparison comparisonType)
        {
            return Get().IndexOf(value, startIndex, comparisonType);
        }
        public int IndexOf(String value, StringComparison comparisonType)
        {
            return Get().IndexOf(value, comparisonType);
        }
        public int IndexOf(String value, int startIndex, int count)
        {
            return Get().IndexOf(value, startIndex, count);
        }
        public int IndexOf(Char value, int startIndex, int count)
        {
            return Get().IndexOf(value, startIndex, count);
        }
        public int IndexOf(Char value, int startIndex)
        {
            return Get().IndexOf(value, startIndex);
        }
        public int IndexOf(Char value)
        {
            return Get().IndexOf(value);
        }
        public int IndexOf(String value, int startIndex, int count, StringComparison comparisonType)
        {
            return Get().IndexOf(value, startIndex, count, comparisonType);
        }
        public int IndexOf(String value, int startIndex)
        {
            return Get().IndexOf(value, startIndex);
        }
        public int IndexOfAny(Char[] anyOf)
        {
            return Get().IndexOfAny(anyOf);
        }
        public int IndexOfAny(Char[] anyOf, int startIndex, int count)
        {
            return Get().IndexOfAny(anyOf, startIndex, count);
        }
        public int IndexOfAny(Char[] anyOf, int startIndex)
        {
            return Get().IndexOfAny(anyOf, startIndex);
        }
        public estring Insert(int startIndex, String value)
        {
            return new estring(Get().Insert(startIndex, value));
        }
        public bool IsNormalized()
        {
            return Get().IsNormalized();
        }
        public bool IsNormalized(NormalizationForm normalizationForm)
        {
            return Get().IsNormalized(normalizationForm);
        }
        public int LastIndexOf(String value, int startIndex, StringComparison comparisonType)
        {
            return Get().LastIndexOf(value, startIndex, comparisonType);
        }
        public int LastIndexOf(String value, int startIndex, int count, StringComparison comparisonType)
        {
            return Get().LastIndexOf(value, startIndex, count, comparisonType);
        }
        public int LastIndexOf(String value, int startIndex, int count)
        {
            return Get().LastIndexOf(value, startIndex, count);
        }
        public int LastIndexOf(String value, StringComparison comparisonType)
        {
            return Get().LastIndexOf(value, comparisonType);
        }
        public int LastIndexOf(String value)
        {
            return Get().LastIndexOf(value);
        }
        public int LastIndexOf(Char value, int startIndex, int count)
        {
            return Get().LastIndexOf(value, startIndex, count);
        }
        public int LastIndexOf(Char value, int startIndex)
        {
            return Get().LastIndexOf(value, startIndex);
        }
        public int LastIndexOf(String value, int startIndex)
        {
            return Get().LastIndexOf(value, startIndex);
        }
        public int LastIndexOf(Char value)
        {
            return Get().LastIndexOf(value);
        }
        public int LastIndexOfAny(Char[] anyOf)
        {
            return Get().LastIndexOfAny(anyOf);
        }
        public int LastIndexOfAny(Char[] anyOf, int startIndex)
        {
            return Get().LastIndexOfAny(anyOf, startIndex);
        }
        public int LastIndexOfAny(Char[] anyOf, int startIndex, int count)
        {
            return Get().LastIndexOfAny(anyOf, startIndex, count);
        }
        public estring Normalize()
        {
            return new estring(Get().Normalize());
        }
        public estring Normalize(NormalizationForm normalizationForm)
        {
            return new estring(Get().Normalize(normalizationForm));
        }
        public estring PadLeft(int totalWidth)
        {
            return new estring(Get().PadLeft(totalWidth));
        }
        public estring PadLeft(int totalWidth, Char paddingChar)
        {
            return new estring(Get().PadLeft(totalWidth, paddingChar));
        }
        public estring PadRight(int totalWidth)
        {
            return new estring(Get().PadLeft(totalWidth));
        }
        public estring PadRight(int totalWidth, Char paddingChar)
        {
            return new estring(Get().PadLeft(totalWidth, paddingChar));
        }
        public estring Remove(int startIndex)
        {
            return new estring(Get().Remove(startIndex));
        }
        public estring Remove(int startIndex, int count)
        {
            return new estring(Get().Remove(startIndex, count));
        }
        public estring Replace(String oldValue, String newValue)
        {
            return new estring(Get().Replace(oldValue, newValue));
        }
        public estring Replace(Char oldChar, Char newChar)
        {
            return new estring(Get().Replace(oldChar, newChar));
        }
        public estring[] Split(String[] separator, int count, StringSplitOptions options)
        {
            String[] array = Get().Split(separator, count, options);
            estring[] result = new estring[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new estring(array[i], TypeValue.Normal); 
            }
            return result;
        }
        public estring[] Split(params Char[] separator)
        {
            String[] array = Get().Split(separator);
            estring[] result = new estring[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new estring(array[i], TypeValue.Normal); 
            }
            return result;
        }
        public estring[] Split(Char[] separator, int count)
        {
            String[] array = Get().Split(separator, count);
            estring[] result = new estring[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new estring(array[i], TypeValue.Normal); 
            }
            return result;
        }
        public estring[] Split(Char[] separator, int count, StringSplitOptions options)
        {
            String[] array = Get().Split(separator, count, options);
            estring[] result = new estring[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new estring(array[i], TypeValue.Normal); 
            }
            return result;
        }
        public estring[] Split(Char[] separator, StringSplitOptions options)
        {
            String[] array = Get().Split(separator, options);
            estring[] result = new estring[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new estring(array[i], TypeValue.Normal); 
            }
            return result;
        }
        public estring[] Split(String[] separator, StringSplitOptions options)
        {
            String[] array = Get().Split(separator, options);
            estring[] result = new estring[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new estring(array[i], TypeValue.Normal); 
            }
            return result;
        }
        public bool StartsWith(String value)
        {
            return Get().StartsWith(value);
        }
        public bool StartsWith(String value, bool ignoreCase, CultureInfo culture)
        {
            return Get().StartsWith(value, ignoreCase, culture);
        }
        public bool StartsWith(String value, StringComparison comparisonType)
        {
            return Get().StartsWith(value, comparisonType);
        }
        public estring Substring(int startIndex)
        {
            return new estring(Get().Substring(startIndex));
        }
        public estring Substring(int startIndex, int length)
        {
            return new estring(Get().Substring(startIndex, length));
        }
        public echar[] ToCharArray(int startIndex, int length)
        {
            Char[] array = Get().ToCharArray(startIndex, length);
            echar[] result = new echar[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new echar(array[i]);
            }
            return result;
        }
        public echar[] ToCharArray()
        {
            Char[] array = Get().ToCharArray();
            echar[] result = new echar[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new echar(array[i]);
            }
            return result;
        }
        public estring ToLower()
        {
            return new estring(Get().ToLower());
        }
        public estring ToLower(CultureInfo culture)
        {
            return new estring(Get().ToLower(culture));
        }
        public estring ToLowerInvariant()
        {
            return new estring(Get().ToLowerInvariant());
        }
        public override String ToString()
        {
            return Get().ToString();
        }
        public String ToString(IFormatProvider provider)
        {
            return Get().ToString(provider);
        }
        public estring ToUpper()
        {
            return new estring(Get().ToUpper());
        }
        public estring ToUpper(CultureInfo culture)
        {
            return new estring(Get().ToUpper(culture));
        }
        public estring ToUpperInvariant()
        {
            return new estring(Get().ToUpperInvariant());
        }
        public estring Trim()
        {
            return new estring(Get().Trim());
        }
        public estring Trim(params Char[] trimChars)
        {
            return new estring(Get().Trim(trimChars));
        }
        public estring TrimEnd(params Char[] trimChars)
        {
            return new estring(Get().TrimEnd(trimChars));
        }
        public estring TrimStart(params Char[] trimChars)
        {
            return new estring(Get().TrimStart(trimChars));
        }
    #endregion

    #region Math operators
        public static estring operator +(estring left, object right)
        {
            estring result = left;
            result.Set(result.Get() + right);
            return result;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private string serializeValue;

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