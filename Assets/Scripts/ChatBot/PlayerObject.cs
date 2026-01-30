using System;

namespace ChatBot
{
    [Serializable]
    public class PlayerObject
    {
        public string twitchName;
        public int gold;

        public PlayerObject(string twitchName)
        {
            this.twitchName = twitchName;
            gold = 100;
        }
    }
}