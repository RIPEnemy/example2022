using UnityEngine;
using System;
using AbonyInt.EventSystem;
using AbonyInt.SmartVariables;

namespace AbonyInt.Gameplay
{
    public class HealthComponent : GameComponent
    {
        [Serializable]
        public struct HealthData
        {
            public readonly object Source;
            public readonly int Value;

            public HealthData(object source, int value)
            {
                Source = source;
                Value = value;
            }
        }

        public delegate HealthData HealthChangeDelegate(HealthData data);

        [Header("Initial values")]
        [SerializeField]
        private int _initialValue = 0;

        [Header("Global events")]
        [SerializeField]
        private GlobalEvent _onAdd = null;
        [SerializeField]
        private GlobalEvent _onSubtract = null;
        [SerializeField]
        private GlobalEvent _onChange = null;
        [SerializeField]
        private GlobalEvent _onDead = null;

        private eint? maximum;
        /// <summary>
        /// Maximum health value. Readonly.
        /// </summary>
        public eint Maximum
        {
            get
            {
                if (maximum == null)
                    maximum = new eint(_initialValue);

                return maximum.Value;
            }
        }

        private eint? current;
        /// <summary>
        /// Current health value.
        /// </summary>
        public eint Current
        {
            get
            {
                if (current == null)
                    current = new eint(_initialValue);

                return current.Value;
            }
            private set
            {
                // Unit health cannot be less than zero
                current = new eint(Mathf.Max(value, 0));
            }
        }
        /// <summary>
        /// Returns current health value in percent (value between 0 and 1).
        /// </summary>
        public float Percent
        {
            get
            {
                return (float)Current / (float)Maximum;
            }
        }

        /// <summary>
        /// Helpful action to handle health add of this object.
        ///
        /// This event (and it's global version) returns <see cref="HealthData"/>.
        /// </summary>
        public event EventHandler<HealthData> OnAdd;
        /// <summary>
        /// Helpful action to handle change of the object's health.
        ///
        ///
        /// This event (and it's global version) returns <see cref="HealthData"/>.
        /// </summary>
        public event EventHandler<HealthData> OnChange;
        /// <summary>
        /// Helpful action to handle health sutract of this object and
        /// intervene to the process. For example, to prevent health subtraction.
        /// </summary>
        public event HealthChangeDelegate BeforeSubtract;
        /// <summary>
        /// Helpful action to handle health substract of this object.
        ///
        /// This event (and it's global version) returns <see cref="HealthData"/>.
        /// </summary>
        public event EventHandler<HealthData> OnSubtract;
        /// <summary>
        /// Helpful action to handle dead of this object.
        /// </summary>
        public event EventHandler OnDead;

        private bool isDead = false;

        /// <summary>
        /// Add health.
        /// </summary>
        /// <param name="source">Source (object) of change health. For example, manager or unit.</param>
        /// <param name="value">Value of health to add.</param>
        public void Add(object source, int value)
        {
            if (isDead)
                return;

            Current += Mathf.Abs(value);

            HealthData data = new HealthData(source, value);

            OnAdd?.Invoke(this, data);
            _onAdd.Invoke(this, data);

            OnChange?.Invoke(this, data);
            _onChange.Invoke(this, data);
        }

        /// <summary>
        /// Subtract health.
        /// </summary>
        /// <param name="source">Source (object) of subtract health. For example, manager or unit.</param>
        /// <param name="value">Value of health to subtract.</param>
        public void Subtract(object source, int value)
        {
            if (isDead)
                return;

            int val = Mathf.Abs(value);

            if (BeforeSubtract != null)
            {
                HealthData beforeSubtract = BeforeSubtract(new HealthData(source, value));

                val = beforeSubtract.Value;

                if (val == 0)
                    return;
            }

            Current -= val;

            HealthData data = new HealthData(source, val);

            OnSubtract?.Invoke(this, data);
            _onSubtract.Invoke(this, data);

            OnChange?.Invoke(this, data);
            _onChange.Invoke(this, data);

            if (Current > 0)
                return;

            isDead = true;

            OnDead?.Invoke(this, null);
            _onDead.Invoke(this);
        }

        /// <summary>
        /// Set current health to target value.
        /// </summary>
        /// <param name="source">Source (object) of set target health. For example, manager or unit.</param>
        /// <param name="value">New health value.</param>
        public void Set(object source, int value)
        {
            if (value < 1)
                throw new NotSupportedException("Health can not be less then zero!");

            isDead = value < 1;

            Current = value;

            HealthData data = new HealthData(source, value);

            OnChange?.Invoke(this, data);
            _onChange.Invoke(this, data);
        }

        /// <summary>
        /// Set new values for current and maximum.
        /// </summary>
        public void ChangeCurrentAndMaximum(int current, int maximum)
        {
            this.maximum = new eint(maximum);
            this.current = new eint(current);

            isDead = current < 1;
        }

        /// <summary>
        /// Restore current health to maximum.
        /// </summary>
        public override void Reset()
        {
            Current = new eint(Maximum);

            isDead = false;
        }
    }
}