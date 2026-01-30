using System;

namespace ChatBot
{
    [Serializable]
    public class GoldTransaction
    {
        public int amount;

        public GoldTransaction(int amount)
        {
            this.amount = amount;
        }
    }
}