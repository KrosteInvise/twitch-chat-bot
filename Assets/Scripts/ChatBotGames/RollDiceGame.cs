using System.Collections;
using ChatBot;
using UnityEngine;

namespace ChatBotGames
{
    public class RollDiceGame
    {
        public IEnumerator RollDice(string sender, string message, PlayersDataBase playersData)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            var userRoll = Random.Range(1, 13);
            var botRoll = Random.Range(1, 13);
            var stake = message.Remove(0,  5);
            var converted = int.TryParse(stake, out int finalStake);

            if (!converted)
            {
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}, чел... Пиши !dice и ставку через пробел EZ");
                yield break;
            }

            if (finalStake <= player.gold)
            {
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName} сделал ставку, ждём результат EZ");
                player.gold -= finalStake;
                yield return new WaitForSeconds(3f);

                if (userRoll > botRoll)
                {
                    ChatEventListener.InvokeOnGameRespond($"Казино: {botRoll}. {player.twitchName}: {userRoll} Поздравляю EZ");
                    player.gold += finalStake * 2;
                }
                else if (userRoll == botRoll)
                {
                    ChatEventListener.InvokeOnGameRespond($"Казино: {botRoll}. {player.twitchName}: {userRoll} Ничья Pog");
                    player.gold += finalStake;
                }
                else
                {
                    ChatEventListener.InvokeOnGameRespond($"Казино: {botRoll}. {player.twitchName}: {userRoll} Казино побеждает YviBusiness");
                }
            }
            else
            {
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}, сэр, бабло чекните (!money)");
            }
        }
    }
}
