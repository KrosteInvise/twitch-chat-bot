using ChatBot;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "CheckBalanceCommand", menuName = "Commands/CheckBalanceCommand")]
    public class CheckBalanceCommand : ChatBotCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            await base.Execute(context);
            if(Player == null)
                return;
            context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender} у вас {Player.gold} деняк veselo"));
        }
    } 
}
