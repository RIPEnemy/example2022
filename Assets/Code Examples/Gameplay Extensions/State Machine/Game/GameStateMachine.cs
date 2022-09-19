using UnityEngine;

namespace AbonyInt.Gameplay
{
    public class GameStateMachine : aStateMachine<GameStates>
    {
        private static GameStateMachine instance;
        /// <summary>
        /// Reference to 'GameStateMachine' instance.
        ///
        /// You can use it to get access to state machine
        /// properies and methods.
        ///
        /// Warning! There should be no more than 1 copy on the 'Game State
        /// Machine' on the scene!
        /// </summary>
        public static GameStateMachine Instance
        {
            get
            {
                if (instance == null)
                    instance = FindOrCreateInstance();

                return instance;
            }
        }

        private static GameStateMachine FindOrCreateInstance()
        {
            GameStateMachine gameStateMachine = FindObjectOfType<GameStateMachine>(true);

            if (gameStateMachine == null)
            {
                GameObject go = new GameObject("*GSM");
                gameStateMachine = go.AddComponent<GameStateMachine>();
            }

            DontDestroyOnLoad(gameStateMachine.gameObject);

            return gameStateMachine;
        }

        private void Awake()
        {
            if (instance != null)
                return;

            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}