using ChatBot;
using Signals;
using UnityEngine;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "CheckBalanceCommand", menuName = "Commands/CheckBalanceCommand")]
    public class CheckBalanceCommand : ChatBotCommand
    {
        public override void Execute(CommandContext context)
        {
            PlayerObject player = context.PlayersData.PlayersDataList.Find(x => x.twitchName == context.Sender);
            context.SignalBus.Fire(new PrintToTwitchChatSignal($"{player.twitchName}, у вас {player.gold} деняк baseg"));
        }
    }
}
