using System.Collections.Generic;
using UnityEngine;

namespace ChatBot
{
    public abstract class ChatBotCommand : ScriptableObject
    {
        [SerializeField]
        string commandName;

        [SerializeField]
        float cooldown;

        public string CommandName => commandName.ToLower();

        public float Cooldown => cooldown;

        public abstract void Execute(string username, List<string> args, PlayersDataBase playersData);
    }
}