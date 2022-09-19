//----------------------------------------
// 	            Object Pool
// Copyright © 2021 'OOO EBONI INTERAKTIV'
//---------------------------------------

using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using AbonyInt.EventSystem;

public class PoolGlobalEventHandler : MonoBehaviour
{
    [Serializable]
    public class Entry
    {
        [SerializeField]
        public MonoBehaviour target;
        [SerializeField]
        public string method;
        [SerializeField]
        public int methodIndex = 0;
    }

    [Serializable]
    public class Handler
    {
        [SerializeField]
        private GlobalEvent _event;
        /// <summary>
        /// Reference to Pool Global Event object.
        /// </summary>
        public GlobalEvent Event => _event;

        [SerializeField]
        private List<Entry> _methods;
        /// <summary>
        /// List of methods to invoke.
        /// </summary>
        public List<Entry> Methods => _methods;
        
        public void Subscribe()
        {
            _event.Subscribe(OnInvoke);
        }

        public void Unsubscribe()
        {
            _event.Unsubscribe(OnInvoke);
        }

        private void OnInvoke(object sender, object data)
        {
            for (int i = 0; i < _methods.Count; ++i)
            {
                MethodInfo[] methods = _methods[i].target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);

                for (int j = 0; j < methods.Length; j++)
                {
                    if (methods[j].Name != _methods[i].method)
                        continue;

                    methods[j].Invoke(
                        _methods[i].target,
                        methods[j].GetParameters().Length == 0
                            ? null
                            : new object[] { sender, data }
                    );
                }
            }
        }
    }

    [SerializeField]
    private List<Handler> _handlers = null;

    private void Awake()
    {
        if (_handlers == null)
            return;

        for (int i = 0; i < _handlers.Count; i++)
        {
            _handlers[i].Subscribe();
        }
    }

    private void OnDestroy()
    {
        if (_handlers == null)
            return;

        for (int i = 0; i < _handlers.Count; i++)
        {
            _handlers[i].Unsubscribe();
        }
    }
}
