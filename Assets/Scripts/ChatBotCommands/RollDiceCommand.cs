using System.Linq;
using ChatBot;
using Cysharp.Threading.Tasks;
using DTO;
using Signals;
using UnityEngine;
using WebRequests;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RollDiceCommand", menuName = "Commands/RollDiceCommand")]
    public class RollDiceCommand : ChatBotCommand
    {
        public override async UniTask Execute(CommandContext context) {
            await base.Execute(context);
            if(Player == null) return;

            string stakeRaw = context.Args.FirstOrDefault();
            if (!int.TryParse(stakeRaw, out int finalStake) || finalStake <= 0) {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender} чел... Пиши !{CommandName} и ставку через пробел EZ"));
                return;
            }
            
            var request = new PlayGambleRequest();
            Gamble result = await request.SendPlayGamble(context.Sender, finalStake);
                
            string status = result.playerRoll > result.botRoll ? "Победа! EZ Clap" : 
                result.playerRoll == result.botRoll ? "Ничья!" : "Вы проиграли... YviBusiness";

            string chatMsg = $"@{context.Sender}: {result.playerRoll} vs {result.botRoll}. {status}";
            context.SignalBus.Fire(new PrintToTwitchChatSignal(chatMsg));
        }
    }
}