namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class astring : avar<estring>
    {
    #region Constructors
        public astring()
        {
            val = default(string);
        }

        public astring(string value)
        {
            val = value;
        }

        public astring(estring value)
        {
            val = value;
        }

        public astring(estring value, BeforeDelegate<estring> before)
        {
            val = value;
            BeforeChange = before;
        }

        public astring(estring value, OnChangeDelegate<estring> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public astring(estring value, AfterDelegate<estring> after)
        {
            val = value;
            AfterChange = after;
        }

        public astring(estring value, BeforeDelegate<estring> before, OnChangeDelegate<estring> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public astring(estring value, BeforeDelegate<estring> before, AfterDelegate<estring> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public astring(estring value, AfterDelegate<estring> after, OnChangeDelegate<estring> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public astring(estring value, BeforeDelegate<estring> before, OnChangeDelegate<estring> onChange, AfterDelegate<estring> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator estring(astring operand)
        {
            return operand == null ? default(estring) : operand.val;
        }
        public static implicit operator string(astring operand)
        {
            return operand == null ? "" : (string)operand.val;
        }
        public static astring operator +(astring left, BeforeDelegate<estring> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static astring operator +(astring left, OnChangeDelegate<estring> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static astring operator +(astring left, AfterDelegate<estring> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static astring operator -(astring left, BeforeDelegate<estring> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static astring operator -(astring left, OnChangeDelegate<estring> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static astring operator -(astring left, AfterDelegate<estring> right)
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