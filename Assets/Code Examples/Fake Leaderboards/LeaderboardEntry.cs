using System;

namespace Game
{
    [Serializable]
    public struct LeaderboardEntry
    {
        public readonly int Place;
        public readonly string CountryCode;
        public readonly string Name;
        public readonly int Score;

        public LeaderboardEntry(int place, string countryCode, string name, int score)
        {
            Place = place;
            CountryCode = countryCode;
            Name = name;
            Score = score;
        }
    }
}