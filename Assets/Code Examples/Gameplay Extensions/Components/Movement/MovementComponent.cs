using UnityEngine;
using System.Collections;

namespace AbonyInt.Gameplay
{
    public class MovementComponent : MovementBaseComponent
    {
        /// <summary>
        /// Movement systems variants.
        /// </summary>
        public enum MovementSystems
        {
            /// <summary>
            /// Object will be move every frame (or another timing) by manager
            /// or controller (or another system).
            /// </summary>
            Manual,
            /// <summary>
            /// Object will be move self every frame (update).
            /// </summary>
            Update,
            /// <summary>
            /// Object will be move self every fixed frame update.
            /// </summary>
            FixedUpdate
        }

        [Header("General")]
        [SerializeField]
        private MovementSystems _movementSystem = MovementSystems.FixedUpdate;
        [SerializeField]
        private float _speed = 1f;

        protected override float InitialSpeed => _speed;

        private Coroutine moveCoroutine;

        protected override void Disable()
        {
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);

            moveCoroutine = null;
        }

        protected override void BeginMove()
        {
            if (_movementSystem == MovementSystems.Manual)
            {
                MakeStep();

                destination = null;
                Target = null;

                return;
            }

            if (moveCoroutine != null)
                return;

            moveCoroutine = StartCoroutine(Moving());

            OnStartInvoke();
        }

        protected override void StopMove()
        {
            if (moveCoroutine == null)
                return;

            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        private void MakeStep()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destination.Value,
                CurrentSpeed * Time.deltaTime
            );

            if (AutoLook)
                transform.LookAt(destination.Value);

            OnMoveInvoke();
        }

        protected IEnumerator Moving()
        {
            bool isFixedUpdate = _movementSystem == MovementSystems.FixedUpdate;
            float magnitude = (transform.position - destination.Value).magnitude;

            while (magnitude > StoppingDistance)
            {
                if (Target != null)
                    destination = Target.transform.position;

                MakeStep();

                if (isFixedUpdate)
                    yield return new WaitForFixedUpdate();
                else
                    yield return new WaitForEndOfFrame();

                magnitude = (transform.position - destination.Value).magnitude;
            }

            moveCoroutine = null;

            Stop();
        }
    }
}