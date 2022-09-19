using UnityEngine;
using System;
using System.Collections.Generic;
using AbonyInt.Gameplay;

namespace Game.VisualEffects
{
    public class VFXController : MonoBehaviour
    {
        private static VFXController instance;
        public static VFXController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<VFXController>();

                return instance;
            }
        }

        [SerializeField]
        private ObjectPool _pool = null;

        private readonly List<VFX> currentVFXs = new List<VFX>();
        private bool disabled;

        private void OnEnable()
        {
            disabled = false;
        }

        private void Start()
        {
            GameStateMachine.Instance.AfterChange += HandleGameState;
        }

        private void OnDisable()
        {
            disabled = true;
        }

        private void OnDestroy()
        {
            disabled = true;
        }

        public VFX Add(string name, Transform target, Vector3? offset = null)
        {
            if (disabled)
                return null;
                
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (_pool == null) // Possible in 'OnDestroy' & 'OnDisable' triggers
                return null;

            VFX newVFX = _pool.Obtain<VFX>(name);

            newVFX.Construct(target, offset);

            newVFX.gameObject.SetActive(true);

            newVFX.Play();

            newVFX.OnHide += HandleVFXHide;

            currentVFXs.Add(newVFX);

            return newVFX;
        }

        public void Remove(VFX vfx)
        {
            vfx.OnHide -= HandleVFXHide;

            currentVFXs.Remove(vfx);

            _pool.Release(vfx);
        }

        public void Clear()
        {
            foreach (VFX vfx in currentVFXs)
            {
                vfx.OnHide -= HandleVFXHide;

                _pool.Release(vfx);
            }

            currentVFXs.Clear();
        }

        private void HandleGameState(GameStates previous, GameStates next)
        {
            if (next == GameStates.Session)
                return;

            Clear();
        }

        private void HandleVFXHide(object sender, EventArgs _)
        {
            Remove((VFX)sender);            
        }
    }
}