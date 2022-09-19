using UnityEngine;
using System;
using AbonyInt.EventSystem;

namespace AbonyInt.Gameplay
{
    public delegate T BeforeDelegate<T>(T previousState, T newState);
    public delegate void AfterDelegate<T>(T previousState, T newState);

    public abstract class aStateMachine<T> : MonoBehaviour
    {
        [Header("Global Events")]
        [SerializeField]
        private GlobalEvent _onChange = null;

        /// <summary>
        /// Reference to previous state. Readonly.
        /// </summary>
        public T Previous { get; private set; }
        /// <summary>
        /// Reference to current state. Readonly.
        /// </summary>
        public T Current { get; private set; }

        /// <summary>
        /// Helpful delegate to prevent or handle
        /// state before it change.
        /// </summary>
        public BeforeDelegate<T> BeforeChange;
        /// <summary>
        /// Action inovke after change current state.
        /// </summary>
        public Action<T> OnChange;
        /// <summary>
        /// Helpful delegate to trace state changes.
        /// Returns previous and new states.
        /// </summary>
        public AfterDelegate<T> AfterChange;

        /// <summary>
        /// Change current state to new.
        /// </summary>
        public virtual void Set(T newState)
        {
            if (Current.Equals(newState))
                return;

            if (BeforeChange != null)
                newState = BeforeChange(Previous, newState);

            if (newState.Equals(Current))
                return;

            Previous = Current;

            Current = newState;

            OnChange?.Invoke(Current);

            if (_onChange != null)
                _onChange.Invoke(this, Current);

            AfterChange?.Invoke(Previous, Current);
        }
    }
}