using UnityEngine;
using System.Collections.Generic;

namespace ChatBot
{
    public class ChatBotGame : MonoBehaviour
    {
        [SerializeField]
        PlayersDataBase playersData;

        public void Init()
        {
            playersData = ChatBotGameData.Load();
            ChatEventListener.OnCommandReceived += ProceedCommand;
        }
        
        void OnApplicationQuit()
        {
            ChatBotGameData.Save(playersData);
        }

        void ProceedCommand(string sender, string command, List<string> args)
        {
            AddPlayer(sender);

            if (command.StartsWith("dice"))
                StartCoroutine(new RollDiceGame().RollDice(sender, playersData, args));
            
            if(command.StartsWith("money"))
                ShowMoney(sender);
        }

        void AddPlayer(string sender)
        {
            PlayerObject player = new PlayerObject(sender);

            if (!playersData.PlayersDataList.Exists(x => x.twitchName == player.twitchName))
                playersData.PlayersDataList.Add(player);

            ChatBotGameData.Save(playersData);
        }

        PlayerObject GetPlayer(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            return player;
        }
        
        void ShowMoney(string sender)
        {
            var player = GetPlayer(sender);
            if (player != null)
                ChatEventListener.InvokeOnGameRespond($"У {player.twitchName}: {player.gold} деняк.");
        }
    }
}