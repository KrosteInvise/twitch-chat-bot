using UnityEngine;
using System.Collections.Generic;

namespace ChatBot
{
    public class ChatBotGame : MonoBehaviour
    {
        [SerializeField]
        PlayersDataBase playersData;
        
        [SerializeField]
        ChatBotCommand[] chatBotCommands;
        
        public void Init()
        {
            playersData = ChatBotGameData.Load();
            ChatEventMediator.OnCommandReceived += ProceedCommand;
        }

        void OnApplicationQuit()
        {
            ChatBotGameData.Save(playersData);
        }

        void ProceedCommand(string sender, string command, List<string> args)
        {
            AddPlayer(sender);
            
            foreach (var chatBotCommand in chatBotCommands)
            {
                if (chatBotCommand.CommandName == command)
                    chatBotCommand.Execute(sender, args, playersData);
            }
        }

        void AddPlayer(string sender)
        {
            PlayerObject player = new PlayerObject(sender);

            if (!playersData.PlayersDataList.Exists(x => x.twitchName == player.twitchName))
                playersData.PlayersDataList.Add(player);

            ChatBotGameData.Save(playersData);
        }
    }
}