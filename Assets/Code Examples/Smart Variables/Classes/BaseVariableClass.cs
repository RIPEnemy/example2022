using UnityEngine;
using System;

namespace AbonyInt.SmartVariables
{
    public delegate T BeforeDelegate<T>(T oldValue, T newValue);
    public delegate void OnChangeDelegate<T>(T oldValue, T newValue);
    public delegate void AfterDelegate<T>(T newValue);

    [Serializable]
    public abstract class BaseVariableClass
    {
        /// <summary>
        /// Set to 'true' and next value will be setted in silent mode (without execute any actions).
        /// </summary>
        public bool SetAsSilent = false;
    }
}