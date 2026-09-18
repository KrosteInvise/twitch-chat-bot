using ChatBot;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "CheckBalanceCommand", menuName = "Commands/CheckBalanceCommand")]
    public class CheckBalanceCommand : ChatBotCommand
    {
        public override async UniTask<string> Execute(CommandContext context)
        {
            var player = await TryGetPlayer(context.Sender);
            if (player == null)
                return PlayerNotFound(context.Sender);

            return $"@{context.Sender} у вас {player.gold} деняк veselo";
        }
    }
}
