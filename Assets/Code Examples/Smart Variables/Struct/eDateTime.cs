using UnityEngine;
using System;
using System.Globalization;
using AbonyInt.Security;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public struct eDateTime
    {
        public static readonly DateTime MaxValue = DateTime.MaxValue;
        public static readonly DateTime MinValue = DateTime.MinValue;

        private string a;
        private string b;
        
        public string EncryptedValue { get; private set; }

        [SerializeField]
        private bool autoEncrypt;

    #region Constructors
        public eDateTime(DateTime value)
        {
            a = b = EncryptedValue = "";
            serializeValue = value.ToString();
            autoEncrypt = false;
            Set(value);
        }

        public eDateTime(string value, TypeValue type)
        {
            autoEncrypt = false;
            DateTime val = default(DateTime);
            a = b = "";
            EncryptedValue = type == TypeValue.Encrypted ? value : "";
            val = Сryptographer.Decrypt<DateTime>(value);
            serializeValue = val.ToString();
            Set(val);
        }
        
        public eDateTime(DateTime value, bool allowAutoEncrypt)
        {
            a = b = EncryptedValue = "";
            serializeValue = value.ToString();
            autoEncrypt = allowAutoEncrypt;
            Set(value);
        }
    #endregion

    #region Get / Set
        private DateTime Get()
        {
            if (string.IsNullOrEmpty(EncryptedValue))
                return string.IsNullOrEmpty(a) ? default(DateTime) : Сryptographer.Decode<DateTime>(a + b);
            else
                return Сryptographer.Decrypt<DateTime>(EncryptedValue);
        }

        private void Set(DateTime value)
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

    #region Add methods
        public eDateTime Add(TimeSpan span)
        {
            eDateTime result = new eDateTime(Get());
            result.Add(span);
            return result;
        }

        public eDateTime AddDays(double days)
        {
            eDateTime result = new eDateTime(Get());
            result.AddDays(days);
            return result;
        }

        public eDateTime AddHours(double hours)
        {
            eDateTime result = new eDateTime(Get());
            result.AddHours(hours);
            return result;
        }

        public eDateTime AddMilliseconds(double milliseconds)
        {
            eDateTime result = new eDateTime(Get());
            result.AddMilliseconds(milliseconds);
            return result;
        }

        public eDateTime AddMinutes(double minutes)
        {
            eDateTime result = new eDateTime(Get());
            result.AddMinutes(minutes);
            return result;
        }

        public eDateTime AddMonths(int months)
        {
            eDateTime result = new eDateTime(Get());
            result.AddMonths(months);
            return result;
        }

        public eDateTime AddTicks(long ticks)
        {
            eDateTime result = new eDateTime(Get());
            result.AddTicks(ticks);
            return result;
        }

        public eDateTime AddYears(int years)
        {
            eDateTime result = new eDateTime(Get());
            result.AddYears(years);
            return result;
        }
    #endregion

    #region Time Format methods
        public string[] GetDateTimeFormats(char format, IFormatProvider provider)
        {
            return Get().GetDateTimeFormats(format, provider);
        }
        public string[] GetDateTimeFormats(char format)
        {
            return Get().GetDateTimeFormats(format);
        }
        public string[] GetDateTimeFormats()
        {
            return Get().GetDateTimeFormats();
        } 
        public string[] GetDateTimeFormats(IFormatProvider provider)
        {
            return Get().GetDateTimeFormats(provider);
        }
    #endregion

    #region Substruct methods
        public TimeSpan Subtract(DateTime value)
        {
            return Get().Subtract(value);
        }

        public eDateTime Subtract(TimeSpan value)
        {
            return new eDateTime(Get().Subtract(value));
        }
    #endregion

    #region To methods
        public long ToBinary()
        {
            return Get().ToBinary();
        }

        public long ToFileTime()
        {
            return Get().ToFileTime();
        }

        public long ToFileTimeUtc()
        {
            return Get().ToFileTimeUtc();
        }

        public DateTime ToLocalTime()
        {
            return Get().ToLocalTime();
        }

        public string ToLongDateString()
        {
            return Get().ToLongDateString();
        }

        public string ToLongTimeString()
        {
            return Get().ToLongDateString();
        }

        public double ToOADate()
        {
            return Get().ToOADate();
        }

        public string ToShortDateString()
        {
            return Get().ToShortDateString();
        }

        public string ToShortTimeString()
        {
            return Get().ToShortDateString();
        }

        public DateTime ToUniversalTime()
        {
            return Get().ToUniversalTime();
        }
    #endregion

    #region Encrypt
        public void Encrypt(DateTime value)
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
        public static implicit operator DateTime(eDateTime operand)
        {
            return operand.Get();
        }
        public static implicit operator eDateTime(DateTime operand)
        {
            return new eDateTime(operand);
        }
    #endregion

    #region Math operators
        public static eDateTime operator +(eDateTime d, TimeSpan t)
        {
            return new eDateTime(d.Get() + t);
        }
        public static TimeSpan operator -(eDateTime d1, DateTime d2)
        {
            return d1.Get() - d2;
        }
        public static TimeSpan operator -(eDateTime d1, eDateTime d2)
        {
            return d1.Get() - d2.Get();
        }
        public static TimeSpan operator -(DateTime d1, eDateTime d2)
        {
            return d1 - d2.Get();
        }
        public static eDateTime operator -(eDateTime d, TimeSpan t)
        {
            return new eDateTime(d.Get() - t);
        }
    #endregion

    #region Parse
        public static eDateTime Parse(string s, IFormatProvider provider)
        {
            eDateTime result = new eDateTime(default(DateTime));

            if (!string.IsNullOrEmpty(s))
                result.Set(DateTime.Parse(s, provider));

            return result;
        }

        public static eDateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
        {
            eDateTime result = new eDateTime(default(DateTime));

            if (!string.IsNullOrEmpty(s))
                result.Set(DateTime.Parse(s, provider, styles));

            return result;
        }

        public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out eDateTime result)
        {
            eDateTime enDateTime = new eDateTime(default(DateTime));
            DateTime val = default(DateTime);
            bool parseResult = DateTime.TryParse(s, provider, styles, out val);

            enDateTime.Set(val);
            result = enDateTime;

            return parseResult;
        }

        public static bool TryParse(string s, out eDateTime result)
        {
            eDateTime enDateTime = new eDateTime(default(DateTime));
            DateTime val = default(DateTime);
            bool parseResult = DateTime.TryParse(s, out val);

            enDateTime.Set(val);
            result = enDateTime;

            return parseResult;
        }
    #endregion

    #region Unity Serialization
        [SerializeField]
        private string serializeValue;

        public void OnBeforeSerialize()
        {
            serializeValue = Get().ToString();
        }

        public void OnAfterDeserialize()
        {
        #if UNITY_EDITOR
            Set(Convert.ToDateTime(serializeValue));

            Encrypt(Convert.ToDateTime(serializeValue));
        #endif
        }
    #endregion
    }
}