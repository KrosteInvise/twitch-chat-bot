using UnityEngine;
using System.Collections.Generic;
using Signals;
using Zenject;

namespace ChatBot
{
    public class ChatBotGame : MonoBehaviour
    {
        [SerializeField]
        ChatBotCommand[] chatBotCommands;
        
        Dictionary<string, ChatBotCommand> commandsDictionary = new();

        SignalBus signalBus;
        
        public void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<ReceiveCommandSignal>(ProceedCommand);

            foreach (var chatBotCommand in chatBotCommands)
                commandsDictionary.Add(chatBotCommand.CommandName, chatBotCommand);
        }

        void ProceedCommand(ReceiveCommandSignal signal)
        {
            var context = new CommandContext()
            {
                Sender = signal.Sender,
                Args = signal.Args,
                SignalBus = signalBus
            };
            
            commandsDictionary.TryGetValue(signal.Command, out ChatBotCommand chatBotCommand);
            if (chatBotCommand != null) 
                _ = chatBotCommand.Execute(context);
        }
    }
}