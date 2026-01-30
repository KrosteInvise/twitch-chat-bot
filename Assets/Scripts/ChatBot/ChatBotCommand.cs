using System;
using Cysharp.Threading.Tasks;
using Signals;
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

        protected PlayerObject Player;

        public string CommandName => commandName.ToLower();

        public float Cooldown => cooldown;
        
        public virtual async UniTask Execute(CommandContext context)
        {
            var getPlayerRequest = new GetByTwitchNameRequest();
            Player = await getPlayerRequest.GetPlayerByTwitchName(context.Sender);

            if (Player == null || String.IsNullOrEmpty(Player.twitchName))
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender} Игрок с таким ником не найден! Зарегаться !create"));
        }
    }
}