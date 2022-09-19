namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class adouble : avar<edouble>
    {
    #region Constructors
        public adouble()
        {
            val = 0d;
        }

        public adouble(double value)
        {
            val = value;
        }

        public adouble(edouble value)
        {
            val = value;
        }

        public adouble(edouble value, BeforeDelegate<edouble> before)
        {
            val = value;
            BeforeChange = before;
        }

        public adouble(edouble value, OnChangeDelegate<edouble> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public adouble(edouble value, AfterDelegate<edouble> after)
        {
            val = value;
            AfterChange = after;
        }

        public adouble(edouble value, BeforeDelegate<edouble> before, OnChangeDelegate<edouble> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public adouble(edouble value, BeforeDelegate<edouble> before, AfterDelegate<edouble> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public adouble(edouble value, AfterDelegate<edouble> after, OnChangeDelegate<edouble> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public adouble(edouble value, BeforeDelegate<edouble> before, OnChangeDelegate<edouble> onChange, AfterDelegate<edouble> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator edouble(adouble operand)
        {
            return operand == null ? default(edouble) : operand.val;
        }
        public static implicit operator double(adouble operand)
        {
            return operand == null ? 0d : (double)operand.val;
        }
        public static adouble operator +(adouble left, BeforeDelegate<edouble> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static adouble operator +(adouble left, OnChangeDelegate<edouble> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static adouble operator +(adouble left, AfterDelegate<edouble> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static adouble operator -(adouble left, BeforeDelegate<edouble> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static adouble operator -(adouble left, OnChangeDelegate<edouble> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static adouble operator -(adouble left, AfterDelegate<edouble> right)
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