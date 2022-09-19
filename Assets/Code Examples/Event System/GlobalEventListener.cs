using UnityEngine;
using UnityEngine.Events;
using System;

namespace AbonyInt.EventSystem
{
    [Serializable]
    public class GlobalEventListener : MonoBehaviour
    {
        [Serializable]
        private class Listener
        {
            [SerializeField, Tooltip("Reference to Abony Global Event.")]
            private GlobalEvent _event;
            /// <summary>
            /// Reference to Abony Global Event.
            /// </summary>
            public GlobalEvent @Event => _event;
            [SerializeField, Tooltip("Event handler (methods or actions).")]
            private UnityEvent<object, object> _handler;
            /// <summary>
            /// Event handler (methods or actions).
            /// </summary>
            public UnityEvent<object, object> Handler => _handler;

            public void Initialize()
            {
                _event.Subscribe(HandleEvent);
            }

            public void Dispose()
            {
                _event.Unsubscribe(HandleEvent);
            }

            private void HandleEvent(object sender, object data)
            {
                _handler.Invoke(sender, data);
            }
        }

        [SerializeField]
        private Listener[] _listeners = null;

        private void OnEnable()
        {
            for (int i = 0; i < _listeners.Length; i++)
            {
                _listeners[i].Initialize();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _listeners.Length; i++)
            {
                _listeners[i].Dispose();
            }
        }
    }
}