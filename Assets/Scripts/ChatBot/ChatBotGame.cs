using UnityEngine;
using System.Collections.Generic;

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

        void ProceedCommand(string sender, string command, List<string> args)
        {
            AddPlayer(sender);

            if (command.StartsWith("dice"))
                StartCoroutine(new RollDiceGame().RollDice(sender, playersData, args));
            
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

        PlayerObject GetPlayer(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            return player;
        }
        
        void ShowMoney(string sender)
        {
            var player = GetPlayer(sender);
            if (player != null)
                ChatEventListener.InvokeOnGameRespond($"У {player.twitchName}: {player.gold} деняк.");
        }
        
        //Obsolete
        /*void ShowStats(string sender)
        {
            var player = GetPlayer(sender);
            if (player != null)
                ChatEventListener.InvokeOnGameRespond($"{player.twitchName}: Уровень: {player.level}. Деняк: {player.gold}");
        }

        void ShowHp(string sender)
        {
            var player = GetPlayer(sender);
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
            var player = GetPlayer(sender);
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
            var player = GetPlayer(sender);
            SendMessage($"{player.twitchName}, ваш класс: {player.characterClass}");
        }
        
        IEnumerator Adventure(string sender)
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
    }
}