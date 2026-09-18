using System.Linq;
using ChatBot;
using Cysharp.Threading.Tasks;
using DTO;
using UnityEngine;
using WebRequests;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RollDiceCommand", menuName = "Commands/RollDiceCommand")]
    public class RollDiceCommand : ChatBotCommand
    {
        public override async UniTask<string> Execute(CommandContext context)
        {
            var player = await TryGetPlayer(context.Sender);
            if (player == null)
                return PlayerNotFound(context.Sender);

            string stakeRaw = context.Args.FirstOrDefault();
            if (!int.TryParse(stakeRaw, out int finalStake) || finalStake <= 0)
                return $"@{context.Sender} чел... Пиши !{CommandName} и ставку через пробел EZ";

            Gamble result = await new PlayGambleRequest().SendPlayGamble(context.Sender, finalStake);
            if (result == null)
                return $"@{context.Sender} не получилось сыграть, попробуй ещё раз";

            string status = result.playerRoll > result.botRoll ? "Победа! EZ Clap" :
                result.playerRoll == result.botRoll ? "Ничья!" : "Вы проиграли... YviBusiness";

            return $"@{context.Sender}: {result.playerRoll} vs {result.botRoll}. {status}";
        }
    }
}
