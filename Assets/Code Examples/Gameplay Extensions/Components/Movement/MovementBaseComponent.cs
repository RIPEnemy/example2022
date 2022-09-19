using UnityEngine;
using System;
using AbonyInt.SmartVariables;
using AbonyInt.EventSystem;

namespace AbonyInt.Gameplay
{
    public abstract class MovementBaseComponent : GameComponent
    {
        protected const float DEFAULT_STOPPING_DISTANCE = 1.25f;

        [Header("General")]
        [SerializeField]
        private bool _autoLook = false;

        [Header("Global Events")]
        [SerializeField]
        private GlobalEvent _onStart = null;
        [SerializeField]
        private GlobalEvent _onFinish = null;
        [SerializeField]
        private GlobalEvent _onMove = null;

        /// <summary>
        /// The object will be automatically look at the destination position if true.
        /// </summary>
        public bool AutoLook => _autoLook;

        /// <summary>
        /// Do not mofidy it without very important reason! This is a default
        /// cached and encoded speed value!
        /// </summary>
        private efloat? defaultSpeed = null;
        /// <summary>
        /// Default object movement speed.
        /// </summary>
        public float DefaultSpeed
        {
            get
            {
                if (defaultSpeed == null)
                    defaultSpeed = new efloat(InitialSpeed);

                return (float)defaultSpeed.Value;
            }
        }
        /// <summary>
        /// Do not mofidy it without very important reason!
        /// Use <see cref="CurrentSpeed"/> instead.
        /// </summary>
        private efloat? currentSpeed = null;
        /// <summary>
        /// Current object movement speed.
        /// </summary>
        protected float CurrentSpeed
        {
            get
            {
                if (currentSpeed == null)
                    currentSpeed = new efloat(InitialSpeed);

                return currentSpeed.Value;
            }
            set
            {
                currentSpeed = new efloat(value);
            }
        }

        /// <summary>
        /// Helpful event to detect where object start move to target position.
        /// Return object's destination.
        /// </summary>
        public event EventHandler<Vector3> OnStart;
        /// <summary>
        /// Helpful event to detect where object finish move to target position.
        /// </summary>
        public event EventHandler OnFinish;
        /// <summary>
        /// Helpful event to detect where object change it's position while move.
        /// </summary>
        public event EventHandler OnMove;

        /// <summary>
        /// Object stopping distance.
        /// </summary>
        protected float StoppingDistance { get; private set; }

        protected Vector3? destination;
        /// <summary>
        /// Current object moving destination. Can be null.
        /// </summary>
        public Vector3? Destination
        {
            get
            {
                return destination;
            }
            protected set
            {
                destination = value;
            }
        }
        /// <summary>
        /// Current target which object will move.
        /// </summary>
        public Transform Target { get; protected set; } = null;

        private void OnDisable()
        {
            Disable();

            destination = null;
            Target = null;
        }

        /// <summary>
        /// Set default speed to new value and recalculate current speed
        /// to from it.
        /// </summary>
        public void SetSpeed(float speed)
        {
            float speedPercent = CurrentSpeed / DefaultSpeed;
            defaultSpeed = speed;
            CurrentSpeed = DefaultSpeed * speedPercent;
        }

        /// <summary>
        /// Move object to the specific position.
        /// </summary>
        /// <param name="position">Target position (destination).</param>
        /// <param name="stoppingDistance">Custom stopping distance./></param>
        public void Move(Vector3 position, float? stoppingDistance = null)
        {
            MoveTo(position, stoppingDistance);
        }

        /// <summary>
        /// Move object to the specific target.
        /// </summary>
        /// <param name="target">Reference to target.</param>
        /// <param name="stoppingDistance">Custom stopping distance.</param>
        public void Move(GameBehaviour target, float? stoppingDistance = null)
        {
            Move(target.transform, stoppingDistance);
        }

        /// <summary>
        /// Move object to the specific target.
        /// </summary>
        /// <param name="target">Reference to target.</param>
        /// <param name="stoppingDistance">Custom stopping distance.</param>
        public void Move<T>(T target, float? stoppingDistance = null) where T : GameComponent
        {
            Move(target.transform, stoppingDistance);
        }

        /// <summary>
        /// Move object to the specific target.
        /// </summary>
        /// <param name="target">Reference to target.</param>
        /// <param name="stoppingDistance">Custom stopping distance.</param>
        public void Move(GameObject target, float? stoppingDistance = null)
        {
            Move(target.transform, stoppingDistance);
        }

        /// <summary>
        /// Move object to the specific target.
        /// </summary>
        /// <param name="target">Reference to target.</param>
        /// <param name="stoppingDistance">Custom stopping distance.</param>
        public void Move(Transform target, float? stoppingDistance = null)
        {
            Target = target;

            MoveTo(Target.position, stoppingDistance);
        }

        /// <summary>
        /// Put here move to position object's logic.
        /// </summary>
        protected void MoveTo(Vector3 position, float? stoppingDistance)
        {
            destination = position;

            this.StoppingDistance = stoppingDistance == null
                ? DEFAULT_STOPPING_DISTANCE
                : stoppingDistance.Value;

            BeginMove();
        }

        /// <summary>
        /// Invoke this method to invoke all 'on start' events and actions.
        /// </summary>
        protected void OnStartInvoke()
        {
            if (destination == null)
                throw new OperationCanceledException("Destination is null, but you try to invoke start event. This is not supported.");

            OnStart?.Invoke(this, destination.Value);
            _onStart.Invoke(this, destination.Value);
        }

        /// <summary>
        /// Invoke this method to invoke all 'on move' events and actions.
        /// </summary>
        protected void OnMoveInvoke()
        {
            OnMove?.Invoke(this, null);
            _onMove.Invoke(this);
        }

        /// <summary>
        /// Invoke this method to invoke all 'on finish' events and actions.
        /// </summary>
        protected void OnFinishInvoke()
        {
            OnFinish?.Invoke(this, null);
            _onFinish.Invoke(this);
        }

        /// <summary>
        /// Immidiate stop the object.
        /// </summary>
        public void Stop()
        {
            destination = null;
            Target = null;

            OnFinishInvoke();

            StopMove();
        }

        /// <summary>
        /// Reset current object movement speed to default value.
        /// </summary>
        public override void Reset()
        {
            CurrentSpeed = DefaultSpeed;
        }

        #region Abstract properies & methods
        /// <summary>
        /// Override to set up initial object movement speed.
        /// </summary>
        protected abstract float InitialSpeed { get; }
        /// <summary>
        /// Put here the disable object movement logic.
        /// </summary>
        protected abstract void Disable();
        /// <summary>
        /// Put here the begin object movement login.
        /// </summary>
        protected abstract void BeginMove();
        /// <summary>
        /// Put here the stop object movement logic.
        /// </summary>
        protected abstract void StopMove();
        #endregion
    }
}