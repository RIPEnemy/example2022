namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class afloat : avar<efloat>
    {
    #region Constructors
        public afloat()
        {
            val = 0f;
        }

        public afloat(float value)
        {
            val = value;
        }

        public afloat(efloat value)
        {
            val = value;
        }

        public afloat(efloat value, BeforeDelegate<efloat> before)
        {
            val = value;
            BeforeChange = before;
        }

        public afloat(efloat value, OnChangeDelegate<efloat> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public afloat(efloat value, AfterDelegate<efloat> after)
        {
            val = value;
            AfterChange = after;
        }

        public afloat(efloat value, BeforeDelegate<efloat> before, OnChangeDelegate<efloat> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public afloat(efloat value, BeforeDelegate<efloat> before, AfterDelegate<efloat> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public afloat(efloat value, AfterDelegate<efloat> after, OnChangeDelegate<efloat> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public afloat(efloat value, BeforeDelegate<efloat> before, OnChangeDelegate<efloat> onChange, AfterDelegate<efloat> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator efloat(afloat operand)
        {
            return operand == null ? default(efloat) : operand.val;
        }
        public static implicit operator float(afloat operand)
        {
            return operand == null ? 0f : (float)operand.val;
        }
        public static afloat operator +(afloat left, BeforeDelegate<efloat> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static afloat operator +(afloat left, OnChangeDelegate<efloat> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static afloat operator +(afloat left, AfterDelegate<efloat> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static afloat operator -(afloat left, BeforeDelegate<efloat> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static afloat operator -(afloat left, OnChangeDelegate<efloat> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static afloat operator -(afloat left, AfterDelegate<efloat> right)
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