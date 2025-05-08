using UnityEngine;
using System.Collections;
using System.IO;
using Random = UnityEngine.Random;

namespace ChatBot
{
    public class ChatBotGame : MonoBehaviour
    {
        [SerializeField]
        PlayersDataBase playersData;

        string FilePath => Path.Combine(Application.persistentDataPath, "ChatData.json");
        
        public void Init(ChatEventListener chatEventListener)
        {
            Load();
            chatEventListener.OnCommandReceived += ProceedCommand;
        }
        
        void OnApplicationQuit()
        {
            Save();
        }

        void ProceedCommand(string sender, string command)
        {
            AddPlayer(sender);
            
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
            
            Debug.Log(command);

            if (command.StartsWith("dice"))
                StartCoroutine(RollDice(sender, command));
        }

        void AddPlayer(string sender)
        {
            PlayerObject player = new PlayerObject(sender);

            if (!playersData.PlayersDataList.Exists(x => x.twitchName == player.twitchName))
                playersData.PlayersDataList.Add(player);

            Save();
        }

        void Save()
        {
            // Перенести сэйв в отдельную папку в корневой папке проекта, как в MWS было
            File.WriteAllText(FilePath,JsonUtility.ToJson(playersData, true)); 
        }

        void Load()
        {
            if (File.Exists(FilePath))
                playersData = JsonUtility.FromJson<PlayersDataBase>(File.ReadAllText(FilePath));
            else
                Debug.LogError("Data file doesn't exist!");
        }

        #region КОМАНДЫ РПГ КОТОРЫЕ ПОКА СКРЫТЬ
        //Тут рпг методы всякие
        /*void ShowStats(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            if (player != null)
                SendMessage($"{player.twitchName} stats. Level: {player.level}. Gold: {player.gold}");
        }

        void ShowMoney(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            if (player != null)
                SendMessage($"{player.twitchName} have: {player.gold} gold.");
        }

        void ShowHp(string sender)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            if (player != null)
            {
                if (player.hp > 1)
                    SendMessage($"{player.twitchName} hp: {player.hp}/{player.maxHp}. Status: Alive");
                else if (player.hp <= 1)
                {
                    player.hp = 1;
                    SendMessage($"{player.twitchName} hp: {player.hp}/{player.maxHp}. Status: Exhausted");
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
                SendMessage($"{player.twitchName} take a good rest and pay for this {cost} gold!");
            }
            else if (player.hp == player.maxHp)
                SendMessage($"{player.twitchName}, ты полностью здоров!");
            else if (player.gold < cost)
                SendMessage($"{player.twitchName}, недостаточно деняк!");

        }

        void ShowPlayerClass(string sender)
        {
            var index = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            SendMessage($"{index.twitchName}, ваш класс: {index.characterClass}");
        }*/

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
        #endregion
        

        IEnumerator RollDice(string sender, string message)
        {
            var player = playersData.PlayersDataList.Find(x => x.twitchName == sender);
            var userRoll = Random.Range(1, 13);
            var botRoll = Random.Range(1, 13);
            var stake = message.Remove(0, 6);
            var converted = int.TryParse(stake, out int finalStake);

            if (!converted)
            {
                SendMessage($"{player.twitchName}, чел... Пиши !dice и ставку через пробел EZ");
                yield break;
            }

            if (finalStake <= player.gold)
            {
                SendMessage($"{player.twitchName} сделал ставку, ждём результат EZ");
                player.gold -= finalStake;
                yield return new WaitForSeconds(4f);

                if (userRoll > botRoll)
                {
                    SendMessage($"Казино: {botRoll}. {player.twitchName}: {userRoll} Поздравляю EZ");
                    player.gold += finalStake * 2;
                }
                else if (userRoll == botRoll)
                {
                    SendMessage($"Казино: {botRoll}. {player.twitchName}: {userRoll} Ничья Pog");
                    player.gold += finalStake;
                }
                else
                {
                    SendMessage($"Казино: {botRoll}. {player.twitchName}: {userRoll} Казино побеждает YviBusiness");
                }
            }
            else
            {
                SendMessage($"{player.twitchName}, сэр, бабло чекните (!money)");
            }
        }
    }
}