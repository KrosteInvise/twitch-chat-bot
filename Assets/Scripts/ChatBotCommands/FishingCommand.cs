using ChatBot;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "FishingCommand", menuName = "Commands/FishingCommand")]
    public class FishingCommand : ChatBotCommand
    {
        public override async UniTask<string> Execute(CommandContext context)
        {
            var player = await TryGetPlayer(context.Sender);
            if (player == null)
                return PlayerNotFound(context.Sender);

            return null;
        }
    }
}
