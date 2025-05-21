using UnityEngine;

namespace ChatBot
{
    public class ChatBotEntryPoint : MonoBehaviour
    {
        [SerializeField]
        ChatBotConfig config;
        
        [SerializeField]
        ChatBotClient chatBotClient;
        
        [SerializeField]
        ChatBotGame chatBotGame;
        
        [SerializeField]
        ChatBotApi chatBotApi;
        
        [SerializeField]
        ChatBotView chatBotView;
        
        void Awake()
        {
            chatBotClient.Init(config);
            chatBotGame.Init();
            chatBotApi.Init();
            //chatBotView.Init(chatBotClient, config);
        }
    }
}