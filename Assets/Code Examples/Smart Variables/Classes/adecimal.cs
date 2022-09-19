namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class adecimal : avar<edecimal>
    {
    #region Constructors
        public adecimal()
        {
            val = 0;
        }

        public adecimal(decimal value)
        {
            val = value;
        }

        public adecimal(edecimal value)
        {
            val = value;
        }

        public adecimal(edecimal value, BeforeDelegate<edecimal> before)
        {
            val = value;
            BeforeChange = before;
        }

        public adecimal(edecimal value, OnChangeDelegate<edecimal> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public adecimal(edecimal value, AfterDelegate<edecimal> after)
        {
            val = value;
            AfterChange = after;
        }

        public adecimal(edecimal value, BeforeDelegate<edecimal> before, OnChangeDelegate<edecimal> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public adecimal(edecimal value, BeforeDelegate<edecimal> before, AfterDelegate<edecimal> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public adecimal(edecimal value, AfterDelegate<edecimal> after, OnChangeDelegate<edecimal> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public adecimal(edecimal value, BeforeDelegate<edecimal> before, OnChangeDelegate<edecimal> onChange, AfterDelegate<edecimal> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator edecimal(adecimal operand)
        {
            return operand == null ? default(edecimal) : operand.val;
        }
        public static implicit operator decimal(adecimal operand)
        {
            return operand == null ? 0 : operand.val;
        }
        public static adecimal operator +(adecimal left, BeforeDelegate<edecimal> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static adecimal operator +(adecimal left, OnChangeDelegate<edecimal> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static adecimal operator +(adecimal left, AfterDelegate<edecimal> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static adecimal operator -(adecimal left, BeforeDelegate<edecimal> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static adecimal operator -(adecimal left, OnChangeDelegate<edecimal> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static adecimal operator -(adecimal left, AfterDelegate<edecimal> right)
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