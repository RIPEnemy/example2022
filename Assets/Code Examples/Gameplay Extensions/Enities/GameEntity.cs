using UnityEngine;
using System;

namespace AbonyInt.Gameplay
{
    [Serializable]
    public abstract class GameEntity<T> : ScriptableObject
    {
        [SerializeField]
        private string _key = "newGameEntity";

        /// <summary>
        /// Key or name of the Game Entity.
        /// </summary>
        public string Key => _key;

        public event EventHandler<T> OnChange;

        protected virtual void OnChangeValue(T value)
        {
            OnChange?.Invoke(this, value);
        }

        protected abstract void Write();
        protected abstract void Read();
    }
}