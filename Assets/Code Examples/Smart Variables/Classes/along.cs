namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class along : avar<elong>
    {
    #region Constructors
        public along()
        {
            val = 0L;
        }

        public along(long value)
        {
            val = value;
        }

        public along(elong value)
        {
            val = value;
        }

        public along(elong value, BeforeDelegate<elong> before)
        {
            val = value;
            BeforeChange = before;
        }

        public along(elong value, OnChangeDelegate<elong> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public along(elong value, AfterDelegate<elong> after)
        {
            val = value;
            AfterChange = after;
        }

        public along(elong value, BeforeDelegate<elong> before, OnChangeDelegate<elong> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public along(elong value, BeforeDelegate<elong> before, AfterDelegate<elong> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public along(elong value, AfterDelegate<elong> after, OnChangeDelegate<elong> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public along(elong value, BeforeDelegate<elong> before, OnChangeDelegate<elong> onChange, AfterDelegate<elong> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator elong(along operand)
        {
            return operand == null ? default(elong) : operand.val;
        }
        public static implicit operator long(along operand)
        {
            return operand == null ? 0L : (long)operand.val;
        }
        public static along operator +(along left, BeforeDelegate<elong> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static along operator +(along left, OnChangeDelegate<elong> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static along operator +(along left, AfterDelegate<elong> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static along operator -(along left, BeforeDelegate<elong> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static along operator -(along left, OnChangeDelegate<elong> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static along operator -(along left, AfterDelegate<elong> right)
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