using UnityEngine;
using System;
using System.Collections.Generic;
using AbonyInt.Extensions;
using AbonyInt.Gameplay;
using Random = UnityEngine.Random;

namespace Game
{
    [Serializable, CreateAssetMenu(menuName = "Game/Leaderboard")]
    public class Leaderboard : ScriptableObject
    {
        private readonly string[] COUNTRIES = new string[]
        {
            "ua",
            "us",
            "de",
            "fr",
            "ca",
            "br",
            "it",
            "jp",
            "cl",
            "gb",
            "hu",
            "id",
            "in",
            "kr",
            "la",
            "my",
            "pl",
            "ru",
            "cn",
            "th",
            "vn"
        };

        [Header("Settings")]
        [SerializeField, Tooltip("Best score in leaderboard (for 1st place).")]
        private int _firstPlaceScore = 12345;
        [SerializeField, Tooltip("How many places are in the leaderboard.")]
        private int _leaderboardSize = 2000;
        [SerializeField, Tooltip("Array of the AI bot names.")]
        private string[] _names = null;
        [SerializeField, Tooltip("How many rating player reach for specific action (for example, win in battle).")]
        private int _rewardForAction = 10;
        public int RewardForAction => _rewardForAction;

        [Header("Player Info")]
        [SerializeField]
        private StringEntity _playerCountry = null;
        [SerializeField]
        private StringEntity _playerName = null;
        [SerializeField, Tooltip("Reference to score entity.")]
        private IntEntity _playerScore = null;

        /// <summary>
        /// Generate and return a new player entry.
        /// </summary>
        public LeaderboardEntry PlayerEntry
        {
            get
            {
                return new LeaderboardEntry(
                    CalcPlayerPlace(),
                    _playerCountry.Value,
                    _playerName.Value,
                    _playerScore.Value
                );
            }
        }

        private int CalcPlayerPlace()
        {
            float scorePercent = _playerScore.Value / (float)_firstPlaceScore;
            return 1 + (_leaderboardSize - Mathf.RoundToInt((_leaderboardSize * scorePercent)));
        }

        /// <summary>
        /// Returns a list of random leaderboard entries.
        /// </summary>
        /// <param name="above">How many entries will be above the player.</param>
        /// <param name="under">How many entries wiil be below the player.</param>
        public List<LeaderboardEntry> Get(int above, int below)
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
            
            LeaderboardEntry playerEntry = PlayerEntry;

            leaderboard.Add(playerEntry);

            if (playerEntry.Place <= above)
            {
                // Calc real above places count
                int realAbove = (playerEntry.Place - 1);
                below += above - realAbove;
                above = realAbove;
            }
            else if (playerEntry.Place + below > _leaderboardSize)
            {
                // Calc real below places count
                int realBelow = (_leaderboardSize - playerEntry.Place);
                above += (below - realBelow);
                below = realBelow;
            }

            int i = 0;
            List<string> names = new List<string>(_names);
            string name;

            for ( ; i < above; i++)
            {
                name = names[Random.Range(0, names.Count)];
                names.Remove(name);

                leaderboard.Insert(
                    0,
                    new LeaderboardEntry(
                        playerEntry.Place - i - 1,
                        COUNTRIES[Random.Range(0, COUNTRIES.Length)],
                        name,
                        playerEntry.Score + (_rewardForAction * (i + 1))
                    )
                );
            }

            for (i = 0; i < below; i++)
            {
                name = names[Random.Range(0, names.Count)];
                names.Remove(name);

                leaderboard.Add(
                    new LeaderboardEntry(
                        playerEntry.Place + i + 1,
                        COUNTRIES[Random.Range(0, COUNTRIES.Length)],
                        name,
                        playerEntry.Score - (_rewardForAction * (i + 1))
                    )
                );
            }

            return leaderboard;
        }
    }
}