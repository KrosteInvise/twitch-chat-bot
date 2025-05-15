using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace ChatBot
{
    public class ChatBotGame : MonoBehaviour
    {
        [SerializeField]
        PlayersDataBase playersData;

        public void Init()
        {
            playersData = ChatBotGameData.Load();
            ChatEventListener.OnCommandReceived += ProceedCommand;
        }
        
        void OnApplicationQuit()
        {
            ChatBotGameData.Save(playersData);
        }

        void ProceedCommand(string sender, string command)
        {
            AddPlayer(sender);
            
            if (command.StartsWith("dice"))
                StartCoroutine(RollDice(sender, command));
            
            if(command.StartsWith("money"))
                ShowMoney(sender);
            
            if (command.StartsWith("ping"))
                ChatEventListener.InvokeOnGameRespond("pong");
            
            /*if (command.StartsWith("stats"))
                ShowStats(sender);

            if (command.StartsWith("hp"))
                ShowHp(sender);

            if (command.StartsWith("runes"))
                ShowMoney(sender);

            if (command.StartsWith("heal"))
                Heal(sender);

            if (command.StartsWith("class"))
                ShowPlayerClass(sender);

            if (command.StartsWith("adventure"))
                StartCoroutine(Adventure(sender));*/
        }

        void AddPlayer(string sender)
        {
            PlayerObject player = new PlayerObject(sender);

            if (!playersData.PlayersDataList.Exists(x => x.twitchName == player.twitchName))
                playersData.PlayersDataList.Add(player);

            ChatBotGameData.Save(playersData);
        }
        
        //Тут рпг методы всякие
        void ShowStats(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            if (player != null)
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}: Уровень: {player.level}. Деняк: {player.gold}");
        }

        void ShowMoney(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            if (player != null)
                ChatEventListener.InvokeOnGameRespond($"У {player.twitchName}: {player.gold} деняк.");
        }

        void ShowHp(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            if (player != null)
            {
                if (player.hp > 1)
                    ChatEventListener.InvokeOnGameRespond($"{player.twitchName} хп: {player.hp}/{player.maxHp}. Статус: Здоров");
                else if (player.hp <= 1)
                {
                    player.hp = 1;
                    ChatEventListener.InvokeOnGameRespond($"{player.twitchName} хп: {player.hp}/{player.maxHp}. Статус: Вымотан");
                }
            }
        }

        void Heal(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            int cost = (player.maxHp - player.hp) * player.level;
            if (player.hp < player.maxHp && player.gold > cost)
            {
                player.gold -= cost;
                player.hp = player.maxHp;
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName} хорошо отдохнул и заплатил {cost} деняк!");
            }
            else if (player.hp == player.maxHp)
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}, ты полностью здоров!");
            else if (player.gold < cost)
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}, недостаточно деняк!");

        }

        void ShowPlayerClass(string sender)
        {
            var index = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            SendMessage($"{index.twitchName}, ваш класс: {index.characterClass}");
        }

        //тестовое отправлялово в "приключения"
        /*IEnumerator Adventure(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            var adventureTime = Random.Range(450, 600);

            if (player.inAdventure)
            {
                SendMessage($"{player.twitchName}, ты и так уже съебался, куда ты лезешь?");
                yield break;
            }

            player.inAdventure = true;

            SendMessage($"{player.twitchName}, отправился в приключение и вернется через {adventureTime} секунд.");

            yield return new WaitForSeconds(adventureTime);

            player.inAdventure = false;
            var earnedGold = Random.Range(5, 40);

            player.gold += earnedGold;

            SendMessage($"{player.twitchName}, вернулся из приключения и принес с собой {earnedGold} деняк.");
        }*/
        

        IEnumerator RollDice(string sender, string message)
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