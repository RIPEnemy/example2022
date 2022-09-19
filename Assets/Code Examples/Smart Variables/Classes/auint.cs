namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class auint : avar<euint>
    {
    #region Constructors
        public auint()
        {
            val = 0;
        }

        public auint(uint value)
        {
            val = value;
        }

        public auint(euint value)
        {
            val = value;
        }

        public auint(euint value, BeforeDelegate<euint> before)
        {
            val = value;
            BeforeChange = before;
        }

        public auint(euint value, OnChangeDelegate<euint> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public auint(euint value, AfterDelegate<euint> after)
        {
            val = value;
            AfterChange = after;
        }

        public auint(euint value, BeforeDelegate<euint> before, OnChangeDelegate<euint> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public auint(euint value, BeforeDelegate<euint> before, AfterDelegate<euint> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public auint(euint value, AfterDelegate<euint> after, OnChangeDelegate<euint> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public auint(euint value, BeforeDelegate<euint> before, OnChangeDelegate<euint> onChange, AfterDelegate<euint> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator euint(auint operand)
        {
            return operand == null ? default(euint) : operand.val;
        }
        public static implicit operator uint(auint operand)
        {
            return operand == null ? 0 : (uint)operand.val;
        }
        public static auint operator +(auint left, BeforeDelegate<euint> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static auint operator +(auint left, OnChangeDelegate<euint> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static auint operator +(auint left, AfterDelegate<euint> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static auint operator -(auint left, BeforeDelegate<euint> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static auint operator -(auint left, OnChangeDelegate<euint> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static auint operator -(auint left, AfterDelegate<euint> right)
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