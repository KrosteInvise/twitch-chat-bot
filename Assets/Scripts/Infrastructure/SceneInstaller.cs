using ChatBot;
using Signals;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ChatBotClient>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ChatBotGame>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ChatBotView>().FromComponentInHierarchy().AsSingle();

            BindSignals();
        }

        void BindSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<LogToChatSignal>();
            Container.DeclareSignal<PrintToLocalChatSignal>();
            Container.DeclareSignal<PrintToTwitchChatSignal>();
            Container.DeclareSignal<ReceiveCommandSignal>();

            Container.BindSignal<PrintToTwitchChatSignal>()
                .ToMethod<ChatBotClient>(client => client.SendMessageToChat)
                .FromResolve();

            Container.BindSignal<PrintToLocalChatSignal>()
                .ToMethod<ChatBotView>(view => view.OnPrintToChat)
                .FromResolve();

            Container.BindSignal<LogToChatSignal>()
                .ToMethod<ChatBotView>(view => view.OnLogToChat)
                .FromResolve();

            Container.BindSignal<ReceiveCommandSignal>()
                .ToMethod<ChatBotGame>(game => game.ProceedCommand)
                .FromResolve();
        }
    }
}
