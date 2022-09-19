using UnityEngine;
using System;

namespace AbonyInt.Gameplay
{
    [Serializable, CreateAssetMenu(menuName = "Game Entity/Entity (float)")]
    public class FloatEntity : GameEntity<float>
    {
        [SerializeField]
        private float _initialValue = 0f;

        [NonSerialized]
        private float? val;
        /// <summary>
        /// Current int entity value.
        /// </summary>
        public float Value
        {
            get
            {
                if (val == null)
                    Read();

                if (val == null)
                    val = _initialValue;

                return val.Value;
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

            val = PlayerPrefs.GetInt(Key);
        }

        protected override void Write()
        {
            PlayerPrefs.SetFloat(Key, Value);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        [Header("Cheats! Editor only.")]
        [SerializeField]
        private bool _cheatsEnable = false;
        [SerializeField]
        private float _cheatValue = 0f;

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