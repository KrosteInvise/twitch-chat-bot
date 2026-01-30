using System;
using ChatBot;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using WebRequests;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "CreatePlayerCommand", menuName = "Commands/CreatePlayerCommand")]
    public class CreatePlayerCommand : ChatBotCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            var getPlayerRequest = new GetByTwitchNameRequest();
            Player = await getPlayerRequest.GetPlayerByTwitchName(context.Sender);
            
            if (Player == null)
            {
                var createRequest = new CreatePlayerRequest();
                await createRequest.CreatePlayerRequestAsync(context.Sender);
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender} успешно создан!"));
            }
            else
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender} уже создан Em"));
        }
    }
}