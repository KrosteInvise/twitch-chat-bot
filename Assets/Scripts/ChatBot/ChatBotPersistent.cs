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
        
        void Awake()
        {
            chatBotClient.Init(config);
            chatBotGame.Init();
        }
    }
}