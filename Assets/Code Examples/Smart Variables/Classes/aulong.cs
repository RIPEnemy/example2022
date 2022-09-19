namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class aulong : avar<eulong>
    {
    #region Constructors
        public aulong()
        {
            val = 0L;
        }

        public aulong(ulong value)
        {
            val = value;
        }

        public aulong(eulong value)
        {
            val = value;
        }

        public aulong(eulong value, BeforeDelegate<eulong> before)
        {
            val = value;
            BeforeChange = before;
        }

        public aulong(eulong value, OnChangeDelegate<eulong> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public aulong(eulong value, AfterDelegate<eulong> after)
        {
            val = value;
            AfterChange = after;
        }

        public aulong(eulong value, BeforeDelegate<eulong> before, OnChangeDelegate<eulong> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public aulong(eulong value, BeforeDelegate<eulong> before, AfterDelegate<eulong> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public aulong(eulong value, AfterDelegate<eulong> after, OnChangeDelegate<eulong> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public aulong(eulong value, BeforeDelegate<eulong> before, OnChangeDelegate<eulong> onChange, AfterDelegate<eulong> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator eulong(aulong operand)
        {
            return operand == null ? default(eulong) : operand.val;
        }
        public static implicit operator ulong(aulong operand)
        {
            return operand == null ? 0L : (ulong)operand.val;
        }
        public static aulong operator +(aulong left, BeforeDelegate<eulong> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static aulong operator +(aulong left, OnChangeDelegate<eulong> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static aulong operator +(aulong left, AfterDelegate<eulong> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static aulong operator -(aulong left, BeforeDelegate<eulong> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static aulong operator -(aulong left, OnChangeDelegate<eulong> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static aulong operator -(aulong left, AfterDelegate<eulong> right)
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