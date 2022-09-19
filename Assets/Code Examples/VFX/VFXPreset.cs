using UnityEngine;
using System;

namespace Game.VisualEffects
{
    [Serializable]
    public struct VFXPreset
    {
        [SerializeField, Tooltip("VFX position offset in world space. Optional.")]
        private Vector3 _offset;
        /// <summary>
        /// VFX position offset in world space. Optional.
        /// </summary>
        public Vector3 Offset => _offset;
        [SerializeField, Tooltip("Method to create VFX.")]
        private TriggerMethods _method;
        /// <summary>
        /// Method to create VFX.
        /// </summary>
        public TriggerMethods Method => _method;
        [SerializeField, Tooltip("Reference to VFX prefab.")]
        private VFX _prefab;
        /// <summary>
        /// Reference to VFX prefab.
        /// </summary>
        public VFX Prefab => _prefab;
    }
}