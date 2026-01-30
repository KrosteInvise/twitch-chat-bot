using ChatBot;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "FishingCommand", menuName = "Commands/FishingCommand")]
    public class FishingCommand : ChatBotCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            await base.Execute(context);
            if(Player == null)
                return;
        }
    }
}

