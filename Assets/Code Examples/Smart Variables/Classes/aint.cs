namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class aint : avar<eint>
    {
    #region Constructors
        public aint()
        {
            val = 0;
        }

        public aint(int value)
        {
            val = value;
        }

        public aint(eint value)
        {
            val = value;
        }

        public aint(eint value, BeforeDelegate<eint> before)
        {
            val = value;
            BeforeChange = before;
        }

        public aint(eint value, OnChangeDelegate<eint> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public aint(eint value, AfterDelegate<eint> after)
        {
            val = value;
            AfterChange = after;
        }

        public aint(eint value, BeforeDelegate<eint> before, OnChangeDelegate<eint> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public aint(eint value, BeforeDelegate<eint> before, AfterDelegate<eint> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public aint(eint value, AfterDelegate<eint> after, OnChangeDelegate<eint> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public aint(eint value, BeforeDelegate<eint> before, OnChangeDelegate<eint> onChange, AfterDelegate<eint> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator eint(aint operand)
        {
            return operand == null ? default(eint) : operand.val;
        }
        public static implicit operator int(aint operand)
        {
            return operand == null ? 0 : (int)operand.val;
        }
        public static aint operator +(aint left, BeforeDelegate<eint> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static aint operator +(aint left, OnChangeDelegate<eint> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static aint operator +(aint left, AfterDelegate<eint> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static aint operator -(aint left, BeforeDelegate<eint> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static aint operator -(aint left, OnChangeDelegate<eint> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static aint operator -(aint left, AfterDelegate<eint> right)
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