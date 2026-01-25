using System;
using System.Collections.Generic;

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

    [Serializable]
    public class PlayersDataBase
    {
        public List<PlayerObject> PlayersDataList;
    }
}