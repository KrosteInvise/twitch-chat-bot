using System;

namespace DTO
{
    [Serializable]
    public class Gamble 
    {
        public string twitchName;
        public int playerRoll;
        public int botRoll;
        public int stake;
    }
}