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
        
        [SerializeField]
        ChatBotConfig chatBotConfig;
        
        ChatMessages chatMessages = new();
        SignalBus signalBus;

        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        
        void Awake()
        {
            chatBotClient.Init(signalBus, chatBotConfig);
            chatBotGame.Init(signalBus);
            chatBotApi.Init();
            chatBotView.Init(signalBus, chatMessages, chatBotClient, chatBotConfig);
        }
    }
}