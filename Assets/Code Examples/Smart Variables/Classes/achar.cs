namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class achar : avar<echar>
    {
    #region Constructors
        public achar()
        {
            val = default(char);
        }

        public achar(char value)
        {
            val = value;
        }

        public achar(echar value)
        {
            val = value;
        }

        public achar(echar value, BeforeDelegate<echar> before)
        {
            val = value;
            BeforeChange = before;
        }

        public achar(echar value, OnChangeDelegate<echar> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public achar(echar value, AfterDelegate<echar> after)
        {
            val = value;
            AfterChange = after;
        }

        public achar(echar value, BeforeDelegate<echar> before, OnChangeDelegate<echar> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public achar(echar value, BeforeDelegate<echar> before, AfterDelegate<echar> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public achar(echar value, AfterDelegate<echar> after, OnChangeDelegate<echar> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public achar(echar value, BeforeDelegate<echar> before, OnChangeDelegate<echar> onChange, AfterDelegate<echar> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator echar(achar operand)
        {
            return operand == null ? default(echar) : operand.val;
        }
        public static implicit operator char(achar operand)
        {
            return operand == null ? default(echar) : operand.val;
        }
        public static achar operator +(achar left, BeforeDelegate<echar> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static achar operator +(achar left, OnChangeDelegate<echar> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static achar operator +(achar left, AfterDelegate<echar> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static achar operator -(achar left, BeforeDelegate<echar> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static achar operator -(achar left, OnChangeDelegate<echar> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static achar operator -(achar left, AfterDelegate<echar> right)
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