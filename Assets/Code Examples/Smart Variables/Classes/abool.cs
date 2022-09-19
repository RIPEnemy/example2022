namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class abool : avar<ebool>
    {
    #region Constructors
        public abool()
        {
            val = false;
        }

        public abool(bool value)
        {
            val = value;
        }

        public abool(ebool value)
        {
            val = value;
        }

        public abool(ebool value, BeforeDelegate<ebool> before)
        {
            val = value;
            BeforeChange = before;
        }

        public abool(ebool value, OnChangeDelegate<ebool> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public abool(ebool value, AfterDelegate<ebool> after)
        {
            val = value;
            AfterChange = after;
        }

        public abool(ebool value, BeforeDelegate<ebool> before, OnChangeDelegate<ebool> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public abool(ebool value, BeforeDelegate<ebool> before, AfterDelegate<ebool> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public abool(ebool value, AfterDelegate<ebool> after, OnChangeDelegate<ebool> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public abool(ebool value, BeforeDelegate<ebool> before, OnChangeDelegate<ebool> onChange, AfterDelegate<ebool> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator ebool(abool operand)
        {
            return operand == null ? default(ebool) : operand.val;
        }
        public static implicit operator bool(abool operand)
        {
            return operand == null ? false : (bool)operand.val;
        }
        public static abool operator +(abool left, BeforeDelegate<ebool> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static abool operator +(abool left, OnChangeDelegate<ebool> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static abool operator +(abool left, AfterDelegate<ebool> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static abool operator -(abool left, BeforeDelegate<ebool> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static abool operator -(abool left, OnChangeDelegate<ebool> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static abool operator -(abool left, AfterDelegate<ebool> right)
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