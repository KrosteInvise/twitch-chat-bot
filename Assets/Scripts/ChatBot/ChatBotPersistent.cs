using UnityEngine;

namespace ChatBot
{
    public class ChatBotPersistent : MonoBehaviour
    {
        [SerializeField]
        ChatBotConfig config;
        
        [SerializeField]
        ChatBotClient chatBotClient;
        
        [SerializeField]
        ChatBotGame chatBotGame;
        
        ChatEventListener chatEventListener = new();
        
        void Awake()
        {
            chatBotClient.Init(config, chatEventListener);
            chatBotGame.Init(chatEventListener);
        }
    }
}