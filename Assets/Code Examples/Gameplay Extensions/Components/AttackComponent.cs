using UnityEngine;
using System;
using AbonyInt.SmartVariables;

namespace AbonyInt.Gameplay
{
    public class AttackComponent : GameComponent
    {
        [Header("General")]
        [SerializeField]
        private int _damage = 1;
        [SerializeField]
        private float _range = 0.1f;

        [NonSerialized]
        private eint? damage = null;
        [NonSerialized]
        private eint? maxDamage = null;
        public int Damage
        {
            get
            {
                if (damage == null)
                    damage = new eint(_damage);

                if (maxDamage == null)
                    maxDamage = new eint(_damage);

                return (int)damage.Value;
            }
            protected set
            {
                damage = value;
            }
        }

        private efloat? range;
        /// <summary>
        /// Current attack range.
        /// </summary>
        public float Range
        {
            get
            {
                if (range == null)
                    range = new efloat(_range);

                return (float)range.Value;
            }
        }

        public override void Reset()
        {
            
        }
    }
}