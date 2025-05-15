using System;
using System.Collections.Generic;

namespace ChatBot
{
    [Serializable]
    public class PlayerObject
    {
        public string twitchName;
        public int level;
        public float gold;
        public int hp;
        public int maxHp;
        public Classes characterClass;
        public bool inAdventure;

        public enum Classes
        {
            Chatter
        }

        public PlayerObject(string twitchName)
        {
            this.twitchName = twitchName;
            level = 1;
            gold = 100;
            hp = 100;
            maxHp = 100;
            characterClass = Classes.Chatter;
            inAdventure = false;
        }
    }

    [Serializable]
    public class PlayersDataBase
    {
        public List<PlayerObject> PlayersDataList;
    }
}