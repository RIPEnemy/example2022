namespace AbonyInt.SmartVariables
{
    [System.Serializable]
    public class abyte : avar<ebyte>
    {
    #region Constructors
        public abyte()
        {
            val = 0;
        }

        public abyte(byte value)
        {
            val = value;
        }

        public abyte(ebyte value)
        {
            val = value;
        }

        public abyte(ebyte value, BeforeDelegate<ebyte> before)
        {
            val = value;
            BeforeChange = before;
        }

        public abyte(ebyte value, OnChangeDelegate<ebyte> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public abyte(ebyte value, AfterDelegate<ebyte> after)
        {
            val = value;
            AfterChange = after;
        }

        public abyte(ebyte value, BeforeDelegate<ebyte> before, OnChangeDelegate<ebyte> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public abyte(ebyte value, BeforeDelegate<ebyte> before, AfterDelegate<ebyte> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public abyte(ebyte value, AfterDelegate<ebyte> after, OnChangeDelegate<ebyte> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public abyte(ebyte value, BeforeDelegate<ebyte> before, OnChangeDelegate<ebyte> onChange, AfterDelegate<ebyte> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion

    #region Operators
        public static implicit operator ebyte(abyte operand)
        {
            return operand == null ? default(ebyte) : operand.val;
        }
        public static implicit operator byte(abyte operand)
        {
            return operand == null ? (byte)0 : (byte)operand.val;
        }
        public static abyte operator +(abyte left, BeforeDelegate<ebyte> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static abyte operator +(abyte left, OnChangeDelegate<ebyte> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static abyte operator +(abyte left, AfterDelegate<ebyte> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static abyte operator -(abyte left, BeforeDelegate<ebyte> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static abyte operator -(abyte left, OnChangeDelegate<ebyte> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static abyte operator -(abyte left, AfterDelegate<ebyte> right)
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