using Signals;
using Zenject;

namespace ChatBot
{
    public class AutoHelloResponse 
    {
        public void AutoHello(string lastUserPinged, SignalBus signalBus)
        {
            if(string.IsNullOrEmpty(lastUserPinged))
                return;
            
            signalBus.Fire(new PrintToTwitchChatSignal($"@{lastUserPinged}, peepoSitHey"));
        }
    }
}
