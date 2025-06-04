using System.Collections.Generic;
using ChatBot;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "FishingCommand", menuName = "Commands/FishingCommand")]
    public class FishingCommand : ChatBotCommand
    {
        public override void Execute(string username, List<string> args, PlayersDataBase playersData)
        {
            
        }
    }
}

