using UnityEngine;
using Zenject;

namespace ChatBot
{
    public class ChatBotEntryPoint : MonoBehaviour
    {
        [SerializeField]
        ChatBotClient chatBotClient;
        
        [SerializeField]
        ChatBotGame chatBotGame;
        
        [SerializeField]
        ChatBotApi chatBotApi;
        
        [SerializeField]
        ChatBotView chatBotView;
        
        ChatMessages chatMessages = new();
        SignalBus signalBus;

        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        
        void Awake()
        {
            chatBotClient.Init(signalBus);
            chatBotGame.Init(signalBus);
            chatBotApi.Init();
            chatBotView.Init(signalBus, chatMessages, chatBotClient);
        }
    }
}