using System.Linq;
using ChatBot;
using Signals;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RollDiceCommand", menuName = "Commands/RollDiceCommand")]
    public class RollDiceCommand : ChatBotCommand
    {
        public override void Execute(CommandContext context)
        {
            PlayerObject player = context.PlayersData.PlayersDataList.Find(x => x.twitchName == context.Sender);
            string stake = context.Args.FirstOrDefault();
            var userRoll = Random.Range(1, 13);
            var botRoll = Random.Range(1, 13);

            if (!int.TryParse(stake, out int finalStake))
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"{player.twitchName}, чел... Пиши !dice и ставку через пробел EZ"));
                return;
            }

            if (finalStake == 0)
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"{player.twitchName}, ха-ха, какой ты смешной veselo"));
                return;
            }

            if (finalStake <= player.gold)
            {
                player.gold -= finalStake;

                if (userRoll > botRoll)
                {
                    context.SignalBus.Fire(new PrintToTwitchChatSignal($"baseg : {botRoll}. {player.twitchName}: {userRoll}. Поздравляю EZ"));
                    player.gold += finalStake * 2;
                }
                else if (userRoll == botRoll)
                {
                    context.SignalBus.Fire(new PrintToTwitchChatSignal($"baseg : {botRoll}. {player.twitchName}: {userRoll}. Да клянись, ничья!"));
                    player.gold += finalStake;
                }
                else
                {
                    context.SignalBus.Fire(new PrintToTwitchChatSignal($"baseg : {botRoll}. {player.twitchName}: {userRoll}. baseg побеждает YviBusiness"));
                }
            }
            else
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"{player.twitchName}, не хватает деняк (!cash)"));
            }
        }
    }
}