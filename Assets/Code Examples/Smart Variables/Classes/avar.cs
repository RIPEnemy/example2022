using UnityEngine;
using System;

namespace AbonyInt.SmartVariables
{
    [Serializable]
    public class avar<T> : BaseVariableClass
    {
        public BeforeDelegate<T> BeforeChange;
        public OnChangeDelegate<T> OnChange;
        public AfterDelegate<T> AfterChange;

        [SerializeField]
        protected T val = default(T);
        public T Value
        {
            get { return val; }
            set
            {
                T oldValue = val;

                if (oldValue.Equals(value))
                    return;
                
                if (SetAsSilent)
                {
                    val = value;
                    SetAsSilent = false;
                    return;
                }

                T newValue = value;

                if (BeforeChange != null)
                    newValue = BeforeChange(oldValue, newValue);

                if (OnChange != null)
                    OnChange(oldValue, newValue);

                val = newValue;

                if (AfterChange != null)
                    AfterChange(val);
            }
        }

    #region Constructors
        public avar()
        {
            val = default(T);
        }

        public avar(T value)
        {
            val = value;
        }

        public avar(T value, BeforeDelegate<T> before)
        {
            val = value;
            BeforeChange = before;
        }

        public avar(T value, OnChangeDelegate<T> onChange)
        {
            val = value;
            OnChange = onChange;
        }

        public avar(T value, AfterDelegate<T> after)
        {
            val = value;
            AfterChange = after;
        }

        public avar(T value, BeforeDelegate<T> before, OnChangeDelegate<T> onChange)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
        }

        public avar(T value, BeforeDelegate<T> before, AfterDelegate<T> after)
        {
            val = value;
            BeforeChange = before;
            AfterChange = after;
        }

        public avar(T value, AfterDelegate<T> after, OnChangeDelegate<T> onChange)
        {
            val = value;
            OnChange = onChange;
            AfterChange = after;
        }

        public avar(T value, BeforeDelegate<T> before, OnChangeDelegate<T> onChange, AfterDelegate<T> after)
        {
            val = value;
            BeforeChange = before;
            OnChange = onChange;
            AfterChange = after;
        }
    #endregion
    
    #region Operators
        public static implicit operator T(avar<T> operand)
        {
            return operand == null ? default(T) : operand.val;
        }
        public static avar<T> operator +(avar<T> left, BeforeDelegate<T> right)
        {
            if (left == null)
                return null;

            left.BeforeChange += right;

            return left;
        }
        public static avar<T> operator +(avar<T> left, OnChangeDelegate<T> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static avar<T> operator +(avar<T> left, AfterDelegate<T> right)
        {
            if (left == null)
                return null;

            left.AfterChange += right;

            return left;
        }
        public static avar<T> operator -(avar<T> left, BeforeDelegate<T> right)
        {
            if (left == null)
                return null;

            left.BeforeChange -= right;

            return left;
        }
        public static avar<T> operator -(avar<T> left, OnChangeDelegate<T> right)
        {
            if (left == null)
                return null;

            left.OnChange += right;

            return left;
        }
        public static avar<T> operator -(avar<T> left, AfterDelegate<T> right)
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

    #region Methods
        public void AddAction(BeforeDelegate<T> action, bool executeNow)
        {
            BeforeChange += action;

            if (executeNow)
                action(val, val);
        }

        public void AddAction(params BeforeDelegate<T>[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                BeforeChange += actions[i];
            }
        }

        public void AddAction(OnChangeDelegate<T> action, bool executeNow)
        {
            OnChange += action;

            if (executeNow)
                action(val, val);
        }

        public void AddAction(params OnChangeDelegate<T>[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                OnChange += actions[i];
            }
        }

        public void AddAction(AfterDelegate<T> action, bool executeNow)
        {
            AfterChange += action;

            if (executeNow)
                action(val);
        }

        public void AddAction(params AfterDelegate<T>[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                AfterChange += actions[i];
            }
        }

        public void RemoveAction(params BeforeDelegate<T>[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                BeforeChange -= actions[i];
            }
        }

        public void RemoveAction(params OnChangeDelegate<T>[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                OnChange -= actions[i];
            }
        }

        public void RemoveAction(params AfterDelegate<T>[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                AfterChange -= actions[i];
            }
        }
    #endregion
    }
}