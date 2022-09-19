namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class asbyte : avar<esbyte>
    {
    #region Constructors
        public asbyte()
        {
            val = 0;
        }

        public asbyte(sbyte value)
        {
            val = value;
        }

        public asbyte(esbyte value)
        {
            val = value;
        }

        public asbyte(esbyte value, BeforeDelegate<esbyte> before)
        {
            val = value;
            BeforeChange = before;
        }

        public asbyte(esbyte value, OnChangeDelegate<esbyte> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public asbyte(esbyte value, AfterDelegate<esbyte> after)
        {
            val = value;
            AfterChange = after;
        }

        public asbyte(esbyte value, BeforeDelegate<esbyte> before, OnChangeDelegate<esbyte> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public asbyte(esbyte value, BeforeDelegate<esbyte> before, AfterDelegate<esbyte> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public asbyte(esbyte value, AfterDelegate<esbyte> after, OnChangeDelegate<esbyte> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public asbyte(esbyte value, BeforeDelegate<esbyte> before, OnChangeDelegate<esbyte> onChange, AfterDelegate<esbyte> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator esbyte(asbyte operand)
        {
            return operand == null ? default(esbyte) : operand.val;
        }
        public static implicit operator sbyte(asbyte operand)
        {
            return operand == null ? default(esbyte) : operand.val;
        }
        public static asbyte operator +(asbyte left, BeforeDelegate<esbyte> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static asbyte operator +(asbyte left, OnChangeDelegate<esbyte> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static asbyte operator +(asbyte left, AfterDelegate<esbyte> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static asbyte operator -(asbyte left, BeforeDelegate<esbyte> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static asbyte operator -(asbyte left, OnChangeDelegate<esbyte> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static asbyte operator -(asbyte left, AfterDelegate<esbyte> right)
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