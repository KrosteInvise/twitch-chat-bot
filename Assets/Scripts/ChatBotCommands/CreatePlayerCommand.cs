using ChatBot;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WebRequests;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "CreatePlayerCommand", menuName = "Commands/CreatePlayerCommand")]
    public class CreatePlayerCommand : ChatBotCommand
    {
        public override async UniTask<string> Execute(CommandContext context)
        {
            var player = await TryGetPlayer(context.Sender);

            if (player == null)
            {
                await new CreatePlayerRequest().SendCreatePlayerRequest(context.Sender);
                return $"@{context.Sender} успешно создан!";
            }

            return $"@{context.Sender} уже создан Em";
        }
    }
}
