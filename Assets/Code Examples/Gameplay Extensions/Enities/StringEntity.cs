using UnityEngine;
using System;

namespace AbonyInt.Gameplay
{
    [Serializable, CreateAssetMenu(menuName = "Game Entity/Entity (string)")]
    public class StringEntity : GameEntity<string>
    {
        [SerializeField]
        private string _initialValue = "stringEntity";

        [NonSerialized]
        private string val = null;
        /// <summary>
        /// Current int entity value.
        /// </summary>
        public string Value
        {
            get
            {
                if (val == null)
                    Read();

                if (val == null)
                    val = _initialValue;

                return val;
            }
            set
            {
                if (val == value)
                    return;

                val = value;

                OnChangeValue(value);

                Write();
            }
        }

        protected override void Read()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (_cheatsEnable)
            {
                val = _cheatValue;

                return;
            }
#endif

            if (!PlayerPrefs.HasKey(Key))
                return;

            val = PlayerPrefs.GetString(Key);
        }

        protected override void Write()
        {
            PlayerPrefs.SetString(Key, Value);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        [Header("Cheats! Editor only.")]
        [SerializeField]
        private bool _cheatsEnable = false;
        [SerializeField]
        private string _cheatValue = "cheatValue";

        [ContextMenu("Clear Data")]
        private void Clear()
        {
            if (!PlayerPrefs.HasKey(Key))
                return;

            PlayerPrefs.DeleteKey(Key);

            PlayerPrefs.Save();
        }
#endif
    }
}