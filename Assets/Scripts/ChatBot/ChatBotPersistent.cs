using UnityEngine;

namespace ChatBot
{
    public class ChatBotPersistent : MonoBehaviour
    {
        [SerializeField]
        ChatBotClient chatBotClient;
        
        [SerializeField]
        ChatBotGame chatBotGame;
        
        void Awake()
        {
            chatBotClient.Init();
            chatBotGame.Init();
        }
    }
}