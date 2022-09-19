using UnityEngine;
using System;

namespace AbonyInt.Gameplay
{
    /// <summary>
    /// Helpful class to manage game states.
    /// </summary>
    public static class GameStateManager
    {
        /// <summary>
        /// Set current game state to 'Pause'.
        /// </summary>
        public static void Pause()
        {
            GameStateMachine.Instance.Set(GameStates.Pause);

            Time.timeScale = 0f;
        }

        /// <summary>
        /// Return game to previous (before pause) state.
        /// </summary>
        public static void Unpause()
        {
            if (GameStateMachine.Instance.Current != GameStates.Pause)
                throw new NotSupportedException("You trying to unpase the game, but the game is not on pause! This is not supported.");

            GameStateMachine.Instance.Set(GameStateMachine.Instance.Previous);

            Time.timeScale = 1f;
        }

        /// <summary>
        /// Set current game state to 'Load'.
        /// </summary>
        public static void Load()
        {
            GameStateMachine.Instance.Set(GameStates.Load);
        }

        /// <summary>
        /// Set current game state to 'Save'.
        /// </summary>
        public static void Save()
        {
            GameStateMachine.Instance.Set(GameStates.Save);
        }

        /// <summary>
        /// Set current game state to 'Lobby'.
        /// </summary>
        public static void ToLobby()
        {
            GameStateMachine.Instance.Set(GameStates.Lobby);
        }

        /// <summary>
        /// Set current game state to 'Session'.
        /// </summary>
        public static void ToSession()
        {
            GameStateMachine.Instance.Set(GameStates.Session);
        }
    }
}