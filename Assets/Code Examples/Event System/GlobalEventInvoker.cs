using UnityEngine;
using System;
using System.Collections.Generic;

namespace AbonyInt.EventSystem
{
    /// <summary>
    /// Helpful class for auto-invoke global events without data in Unity methods.
    /// </summary>
    public class GlobalEventInvoker : MonoBehaviour
    {
        private enum MethodTypes
        {
            Awake,
            OnEnable,
            Start,
            Update,
            FixedUpdate,
            OnCollisionEnter2D,
            OnCollisionStay2D,
            OnCollisionExit2D,
            OnCollisionEnter3D,
            OnCollisionStay3D,
            OnCollisionExit3D,
            OnTriggerEnter2D,
            OnTriggerStay2D,
            OnTriggerExit2D,
            OnTriggerEnter3D,
            OnTriggerStay3D,
            OnTriggerExit3D,
            OnDisable,
            OnDestroy
        }

        /// <summary>
        /// Internal struct for create invokers.
        /// </summary>
        [Serializable]
        private struct Invoker
        {
            [SerializeField, Tooltip("Specific method type.")]
            private MethodTypes _methodType;
            /// <summary>
            /// Specific method type.
            /// </summary>
            public MethodTypes MethodType => _methodType;
            [SerializeField, Tooltip("Reference to global event.")]
            private GlobalEvent _event;
            /// <summary>
            /// Reference to global event.
            /// </summary>
            public GlobalEvent @Event => _event;
        }

        [SerializeField, Tooltip("List of the invokers.")]
        private List<Invoker> _invokers = new List<Invoker>();

        private List<Invoker> updateInvokers = new List<Invoker>();
        private List<Invoker> fixedUpdateInvokers = new List<Invoker>();

        private void Awake()
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.Awake);

            if (list.Count > 0)
                list.ForEach(i => i.Event.Invoke(gameObject));
            
            _invokers.RemoveAll(x => x.MethodType == MethodTypes.Awake);

            updateInvokers = _invokers.FindAll(x => x.MethodType == MethodTypes.Update);
            fixedUpdateInvokers = _invokers.FindAll(x => x.MethodType == MethodTypes.FixedUpdate);
        }

        private void OnEnable()
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnEnable);
            
            if (list.Count < 1)
                return;
            
            list.ForEach(i => i.Event.Invoke(gameObject));
        }

        private void Start()
        {
           List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.Start);

           if (list.Count > 0)
                list.ForEach(i => i.Event.Invoke(gameObject));

            _invokers.RemoveAll(x => x.MethodType == MethodTypes.Start);
        }

        private void Update()
        {
            if (updateInvokers.Count < 1)
                return;

            updateInvokers.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void FixedUpdate()
        {
            if (fixedUpdateInvokers.Count < 1)
                return;

            fixedUpdateInvokers.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnCollisionEnter2D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnCollisionStay2D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnCollisionExit2D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnCollisionEnter(Collision other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnCollisionEnter3D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnCollisionStay(Collision other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnCollisionStay3D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnCollisionExit(Collision other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnCollisionExit3D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnTriggerEnter2D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnTriggerStay2D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnTriggerExit2D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnTriggerEnter(Collider other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnTriggerEnter3D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnTriggerStay(Collider other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnTriggerStay3D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnTriggerExit(Collider other)
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnTriggerExit3D);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnDisable()
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnDisable);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }

        private void OnDestroy()
        {
            List<Invoker> list = _invokers.FindAll(x => x.MethodType == MethodTypes.OnDestroy);

            if (list.Count < 1)
                return;

            list.ForEach(x => x.Event.Invoke(gameObject));
        }
    }
}