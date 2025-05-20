using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChatBot;
using UnityEngine;

namespace ChatBotCommands
{
    public class RollDiceGame
    {
        public IEnumerator RollDice(string sender, PlayersDataBase playersData, List<string> args)
        {
            PlayerObject player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            string stake = args.FirstOrDefault();
            var userRoll = Random.Range(1, 13);
            var botRoll = Random.Range(1, 13);

            if (!int.TryParse(stake, out int finalStake))
            {
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}, чел... Пиши !dice и ставку через пробел EZ");
                yield break;
            }

            if (finalStake <= player.gold)
            {
                player.gold -= finalStake;

                if (userRoll > botRoll)
                {
                    ChatEventListener.InvokeOnGameRespond($"baseg : {botRoll}. {player.twitchName}: {userRoll}. Поздравляю EZ");
                    player.gold += finalStake * 2;
                }
                else if (userRoll == botRoll)
                {
                    ChatEventListener.InvokeOnGameRespond($"baseg : {botRoll}. {player.twitchName}: {userRoll}. Да клянись, ничья!");
                    player.gold += finalStake;
                }
                else
                {
                    ChatEventListener.InvokeOnGameRespond($"baseg : {botRoll}. {player.twitchName}: {userRoll}. baseg побеждает YviBusiness");
                }
            }
            else
            {
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}, не хватает деняк (!money)");
            }
        }
    }
}
