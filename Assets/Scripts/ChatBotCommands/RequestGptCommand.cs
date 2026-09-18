using ChatBot;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WebRequests;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RequestGptCommand", menuName = "Commands/RequestGptCommand")]
    public class RequestGptCommand : ChatBotCommand
    {
        public override async UniTask<string> Execute(CommandContext context)
        {
            string question = string.Join(" ", context.Args);
            if (string.IsNullOrEmpty(question))
                return $"@{context.Sender}, this is not a valid question.";

            return await new AskGptRequest().GetGptResponse(context.Sender, question);
        }
    }
}
