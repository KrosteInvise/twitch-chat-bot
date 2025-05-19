using System;
using System.Collections.Generic;

namespace ChatBot
{
    [Serializable]
    public class PlayerObject
    {
        public string twitchName;
        public float gold;
        public int level;
        public int hp;
        public int maxHp;
        //public bool inAdventure;

        public PlayerObject(string twitchName)
        {
            this.twitchName = twitchName;
            gold = 100;
            level = 1;
            hp = 100;
            maxHp = 100;
            //inAdventure = false;
        }
    }

    [Serializable]
    public class PlayersDataBase
    {
        public List<PlayerObject> PlayersDataList;
    }
}