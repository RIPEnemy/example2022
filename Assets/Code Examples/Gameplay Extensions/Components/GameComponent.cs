using UnityEngine;

namespace AbonyInt.Gameplay
{
    [RequireComponent(typeof(GameBehaviour))]
    public abstract class GameComponent : MonoBehaviour
    {
        private GameBehaviour behaviour;
        /// <summary>
        /// Reference to the Game Behaviour attached to this game object.
        /// </summary>
        public GameBehaviour Behaviour
        {
            get
            {
                if (behaviour == null)
                    behaviour = GetComponent<GameBehaviour>();

                return behaviour;
            }
        }

        public abstract void Reset();
    }
}