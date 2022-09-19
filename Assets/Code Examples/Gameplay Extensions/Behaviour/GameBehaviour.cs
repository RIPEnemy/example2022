using UnityEngine;
using System;
using System.Collections.Generic;

namespace AbonyInt.Gameplay
{
    public class GameBehaviour : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        private Owners _owner = Owners.Neutral;

        /// <summary>
        /// Current owner of Game Behaviour.
        /// </summary>
        public Owners Owner => _owner;
        /// <summary>
        /// The Game Behaviour is active in hierarchy right now or not.
        /// </summary>
        public bool activeInHierarchy => gameObject.activeInHierarchy;
        /// <summary>
        /// The Game Behaviour is active self or not. Any game object
        /// can be active self event if it's parent is not active.
        ///
        /// If you need to check active the object on the scene (or screen)
        /// right now use <see cref="activeInHierarchy"/> instead.
        /// </summary>
        public bool activeSelf => gameObject.activeSelf;

        /// <summary>
        /// Unique ID of game behaviour.
        /// </summary>
        public string ID { get; private set; } = null;

        private List<GameComponent> gameComponents = null;

        private void Awake()
        {
            if (!string.IsNullOrEmpty(ID))
                return;

            ID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Turn on or off Game Behaviour in hierarchy.
        /// </summary>
        public void SetActive(bool value)
        {
            if (value && string.IsNullOrEmpty(ID))
                ID = Guid.NewGuid().ToString();

            gameObject.SetActive(value);
        }

        /// <summary>
        /// Return specific game component if exists.
        /// </summary>
        public T GetGameComponent<T>() where T : GameComponent
        {
            if (gameComponents == null)
                CacheGameComponents();

            GameComponent result = gameComponents.Find(x => x is T);

            return result == null
                ? null
                : (result as T);
        }

        private void CacheGameComponents()
        {
            gameComponents = new List<GameComponent>();
            Component[] components = gameObject.GetComponents(typeof(GameComponent));

            if (components == null)
                return;

            for (int i = 0; i < components.Length; i++)
            {
                gameComponents.Add((GameComponent)components[i]);
            }
        }
    }
}