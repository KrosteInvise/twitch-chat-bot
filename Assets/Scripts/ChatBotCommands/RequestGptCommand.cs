using ChatBot;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using WebRequests;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RequestGptCommand", menuName = "Commands/RequestGptCommand")]
    public class RequestGptCommand : ChatBotCommand
    {
        public override async UniTask Execute(CommandContext context)
        { 
            string question = string.Join(" ", context.Args);
            if (string.IsNullOrEmpty(question))
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender}, this is not a valid question."));
                return;
            }

            var request = new AskGptRequest(); 
            await request.GetGptResponse(context.Sender, question, context.SignalBus);
        }
    }
}
