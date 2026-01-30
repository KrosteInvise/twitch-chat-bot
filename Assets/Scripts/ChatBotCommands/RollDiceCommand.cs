using System.Linq;
using ChatBot;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using WebRequests;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RollDiceCommand", menuName = "Commands/RollDiceCommand")]
    public class RollDiceCommand : ChatBotCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            await base.Execute(context);
            if(Player == null)
                return;
            var stake = context.Args.FirstOrDefault();
            var userRoll = Random.Range(1, 13);
            var botRoll = Random.Range(1, 13);

            if (!int.TryParse(stake, out int finalStake))
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender} чел... Пиши !{CommandName} и ставку через пробел EZ"));
                return;
            }

            if (finalStake <= 0)
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender} ха-ха, какой ты смешной veselo"));
                return;
            }

            if (finalStake <= Player.gold)
            {
                var request = new ModifyGoldRequest();
                
                if (userRoll > botRoll)
                {
                    context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender}: {userRoll}. baseg : {botRoll}. Поздравляю EZ Clap"));
                    await request.ModifyGold(Player.twitchName, finalStake * 2);
                }
                else if (userRoll == botRoll)
                {
                    context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender}: {userRoll}. baseg : {botRoll}. Ничья!"));
                }
                else
                {
                    context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender}: {userRoll}. baseg : {botRoll}. baseg побеждает YviBusiness"));
                    await request.ModifyGold(Player.twitchName, -finalStake);
                }
            }
            else
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender}, не хватает деняк (!coins)"));
            }
        }
    }
}