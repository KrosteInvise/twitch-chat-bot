using Signals;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSignals();
        }

        void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LogToChatSignal>();
            Container.DeclareSignal<PrintToChatSignal>();
            Container.DeclareSignal<PrintToTwitchChatSignal>();
            Container.DeclareSignal<ReceiveCommandSignal>();
        }
    }
}
