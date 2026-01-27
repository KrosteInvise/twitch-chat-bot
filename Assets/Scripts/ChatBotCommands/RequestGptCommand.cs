using System.Linq;
using System.Text;
using ChatBot;
using NaughtyAttributes;
using Signals;
using UnityEngine;
using UnityEngine.Networking;
using WebRequests;
using Zenject;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RequestGptCommand", menuName = "Commands/RequestGptCommand")]
    public class RequestGptCommand : ChatBotCommand
    {
        public override void Execute(CommandContext context)
        { 
            string question = string.Join(" ", context.Args);
            if (string.IsNullOrEmpty(question))
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender}, this is not a valid question."));
                return;
            }

            var request = new AskGptRequest(); 
            request.GetGptResponse(context.Sender, question, context.SignalBus);
        }
    }
}
