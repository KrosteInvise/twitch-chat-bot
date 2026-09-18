using Cysharp.Threading.Tasks;
using DTO;
using UnityEngine;
using WebRequests;

namespace ChatBot
{
    public abstract class ChatBotCommand : ScriptableObject
    {
        [SerializeField]
        string commandName;

        [SerializeField]
        float cooldown;

        public string CommandName => commandName.ToLower();

        public float Cooldown => cooldown;

        public abstract UniTask<string> Execute(CommandContext context);

        protected async UniTask<PlayerObject> TryGetPlayer(string twitchName)
        {
            var player = await new GetByTwitchNameRequest().SendGetPlayerByTwitchName(twitchName);
            if (player == null || string.IsNullOrEmpty(player.twitchName))
                return null;

            return player;
        }

        protected static string PlayerNotFound(string sender) =>
            $"@{sender} Игрок с таким ником не найден! Зарегаться !create";
    }
}
