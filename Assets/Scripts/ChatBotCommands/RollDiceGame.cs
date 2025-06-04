using System.Collections.Generic;
using System.Linq;
using ChatBot;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "ChatBotCommand", menuName = "Commands/ChatBotCommand")]
    public class RollDiceGame : ChatBotCommand
    {
        public override void Execute(string username, List<string> args, PlayersDataBase playersData)
        {
            PlayerObject player = playersData.PlayersDataList.Find(x => x.twitchName == username);
            string stake = args.FirstOrDefault();
            var userRoll = Random.Range(1, 13);
            var botRoll = Random.Range(1, 13);

            if (!int.TryParse(stake, out int finalStake))
            {
                ChatEventMediator.InvokeRespond($"{player.twitchName}, чел... Пиши !dice и ставку через пробел EZ");
                return;
            }

            if (finalStake <= player.gold)
            {
                player.gold -= finalStake;

                if (userRoll > botRoll)
                {
                    ChatEventMediator.InvokeRespond($"baseg : {botRoll}. {player.twitchName}: {userRoll}. Поздравляю EZ");
                    player.gold += finalStake * 2;
                }
                else if (userRoll == botRoll)
                {
                    ChatEventMediator.InvokeRespond($"baseg : {botRoll}. {player.twitchName}: {userRoll}. Да клянись, ничья!");
                    player.gold += finalStake;
                }
                else
                {
                    ChatEventMediator.InvokeRespond($"baseg : {botRoll}. {player.twitchName}: {userRoll}. baseg побеждает YviBusiness");
                }
            }
            else
            {
                ChatEventMediator.InvokeRespond($"{player.twitchName}, не хватает деняк (!money)");
            }
        }
    }
}