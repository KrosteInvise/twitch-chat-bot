using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;

namespace ChatBot
{
    public class ChatBotGame : MonoBehaviour
    {
        [SerializeField]
        ChatBotCommand[] chatBotCommands;

        readonly Dictionary<string, ChatBotCommand> commandsDictionary = new();
        readonly Dictionary<string, float> lastUsedByPlayerAndCommand = new();

        SignalBus signalBus;

        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        void Awake()
        {
            foreach (var chatBotCommand in chatBotCommands)
            {
                if (chatBotCommand == null)
                    continue;

                commandsDictionary[chatBotCommand.CommandName] = chatBotCommand;
            }
        }

        public async void ProceedCommand(ReceiveCommandSignal signal)
        {
            string commandKey = signal.Command?.ToLowerInvariant();
            if (string.IsNullOrEmpty(commandKey) ||
                !commandsDictionary.TryGetValue(commandKey, out ChatBotCommand chatBotCommand) ||
                chatBotCommand == null)
                return;

            string cooldownKey = $"{signal.Sender}:{commandKey}";
            if (chatBotCommand.Cooldown > 0f &&
                lastUsedByPlayerAndCommand.TryGetValue(cooldownKey, out float lastUsed) &&
                Time.time < lastUsed + chatBotCommand.Cooldown)
                return;

            lastUsedByPlayerAndCommand[cooldownKey] = Time.time;

            var context = new CommandContext
            {
                Sender = signal.Sender,
                Args = signal.Args
            };

            string message = await chatBotCommand.Execute(context);
            if (!string.IsNullOrEmpty(message))
                signalBus.Fire(new PrintToTwitchChatSignal(message));
        }
    }
}
