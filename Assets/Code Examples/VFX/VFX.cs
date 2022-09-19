using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game.VisualEffects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VFX : MonoBehaviour
    {
        public EventHandler OnHide;

        private List<ParticleSystem> particleSystems = null;
        private Coroutine hideCoroutine = null;

        private void OnDisable()
        {
            if (hideCoroutine == null)
                return;

            StopCoroutine(hideCoroutine);

            hideCoroutine = null;

            OnHide?.Invoke(this, null);
        }

        public void Construct(Transform target, Vector3? offset = null)
        {
            transform.position = new Vector3(
                target.position.x,
                target.position.y,
                target.position.z
            );

            if (offset != null)
                transform.position += offset.Value;

            if (particleSystems != null)
                return;

            particleSystems = new List<ParticleSystem>();

            particleSystems.Add(GetComponent<ParticleSystem>());

            for (int i = 0; i < transform.childCount; i++)
            {
                ParticleSystem particleSystem = transform.GetChild(i).GetComponent<ParticleSystem>();

                if (particleSystem == null)
                    continue;

                particleSystems.Add(particleSystem);
            }
        }

        public void Play()
        {
            if (hideCoroutine != null)
                return;

            particleSystems.ForEach(x => x.Play());

            hideCoroutine = StartCoroutine(Hide());
        }

        private IEnumerator Hide()
        {
            float time = particleSystems.Select(x => x.main.duration).Max();

            yield return new WaitForSeconds(time);

            hideCoroutine = null;

            OnHide?.Invoke(this, null);
        }
    }
}