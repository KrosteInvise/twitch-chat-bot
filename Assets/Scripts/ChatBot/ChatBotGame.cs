using UnityEngine;
using System.Collections.Generic;
using Signals;
using Zenject;

namespace ChatBot
{
    public class ChatBotGame : MonoBehaviour
    {
        [SerializeField]
        PlayersDataBase playersData;
        
        [SerializeField]
        ChatBotCommand[] chatBotCommands;

        SignalBus signalBus;
        
        public void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            playersData = ChatBotGameData.Load();
            signalBus.Subscribe<ReceiveCommandSignal>(ProceedCommand);
        }

        void OnApplicationQuit()
        {
            ChatBotGameData.Save(playersData);
        }

        void ProceedCommand(ReceiveCommandSignal signal)
        {
            AddPlayer(signal.Sender);

            var context = new CommandContext()
            {
                Sender = signal.Sender,
                Args = signal.Args,
                PlayersData = playersData,
                SignalBus = signalBus
            };
            
            foreach (var chatBotCommand in chatBotCommands)
            {
                if (chatBotCommand.CommandName == signal.Command)
                    chatBotCommand.Execute(context);
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