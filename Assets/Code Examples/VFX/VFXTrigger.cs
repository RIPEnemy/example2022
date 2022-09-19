using UnityEngine;
using System.Collections.Generic;

namespace Game.VisualEffects
{
    public class VFXTrigger : MonoBehaviour
    {
        [SerializeField, Tooltip("List of effects for this object.")]
        public List<VFXPreset> Effects = new List<VFXPreset>();

        private void Awake()
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnAwake).ForEach(x => Call(x));
        }

        private void OnEnable()
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnEnable).ForEach(x => Call(x));
        }

        private void Start()
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnStart).ForEach(x => Call(x));
        }

        private void OnDisable()
        {
            if (VFXController.Instance == null)
                return;

            Effects.FindAll(x => x.Method == TriggerMethods.OnDisable).ForEach(x => Call(x));
        }

        private void OnDestroy()
        {
            if (VFXController.Instance == null)
                return;

            Effects.FindAll(x => x.Method == TriggerMethods.OnDestroy).ForEach(x => Call(x));
        }

        private void OnTriggerEnter(Collider other)
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnTriggerEnter).ForEach(x => Call(x));
        }

        private void OnTriggerStay(Collider other)
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnTriggerStay).ForEach(x => Call(x));
        }

        private void OnTriggerExit(Collider other)
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnTriggerExit).ForEach(x => Call(x));
        }

        private void OnCollisionEnter(Collision other)
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnCollisionEnter).ForEach(x => Call(x));
        }

        private void OnCollisionStay(Collision other)
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnCollisionStay).ForEach(x => Call(x));
        }

        private void OnCollisionExit(Collision other)
        {
            Effects.FindAll(x => x.Method == TriggerMethods.OnCollisionExit).ForEach(x => Call(x));
        }

        /// <summary>
        /// Invoke this method to create VFX.
        /// </summary>
        public void Call(VFXPreset preset, Transform customPosition = null)
        {
            VFXController.Instance.Add(
                preset.Prefab.name,
                customPosition == null
                    ? transform
                    : customPosition,
                preset.Offset
            );
        }
    }
}