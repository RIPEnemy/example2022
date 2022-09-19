using System;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public class aDateTime : avar<eDateTime>
    {
    #region Constructors
        public aDateTime()
        {
            val = default(DateTime);
        }

        public aDateTime(DateTime value)
        {
            val = value;
        }

        public aDateTime(eDateTime value)
        {
            val = value;
        }

        public aDateTime(eDateTime value, BeforeDelegate<eDateTime> before)
        {
            val = value;
            BeforeChange = before;
        }

        public aDateTime(eDateTime value, OnChangeDelegate<eDateTime> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public aDateTime(eDateTime value, AfterDelegate<eDateTime> after)
        {
            val = value;
            AfterChange = after;
        }

        public aDateTime(eDateTime value, BeforeDelegate<eDateTime> before, OnChangeDelegate<eDateTime> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public aDateTime(eDateTime value, BeforeDelegate<eDateTime> before, AfterDelegate<eDateTime> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public aDateTime(eDateTime value, AfterDelegate<eDateTime> after, OnChangeDelegate<eDateTime> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public aDateTime(eDateTime value, BeforeDelegate<eDateTime> before, OnChangeDelegate<eDateTime> onChange, AfterDelegate<eDateTime> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator eDateTime(aDateTime operand)
        {
            return operand == null ? default(eDateTime) : operand.val;
        }
        public static implicit operator DateTime(aDateTime operand)
        {
            return operand == null ? default(DateTime) : (DateTime)operand.val;
        }
        public static aDateTime operator +(aDateTime left, BeforeDelegate<eDateTime> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static aDateTime operator +(aDateTime left, OnChangeDelegate<eDateTime> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static aDateTime operator +(aDateTime left, AfterDelegate<eDateTime> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static aDateTime operator -(aDateTime left, BeforeDelegate<eDateTime> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static aDateTime operator -(aDateTime left, OnChangeDelegate<eDateTime> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static aDateTime operator -(aDateTime left, AfterDelegate<eDateTime> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
    #endregion
    
    #region ToString
        public override string ToString()
        {
            return val.ToString();
        }
    #endregion
    }
}