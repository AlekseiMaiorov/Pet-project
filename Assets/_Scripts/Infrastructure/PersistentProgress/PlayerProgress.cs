using System;

namespace Infrastructure.States
{
    [Serializable]
    public class PlayerProgress
    {
        public string Level = "Main";
        public int MaximumScore;
    }
}