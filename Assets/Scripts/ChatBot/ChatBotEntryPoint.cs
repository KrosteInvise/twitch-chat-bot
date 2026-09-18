using UnityEngine;

namespace ChatBot
{
    public class ChatBotEntryPoint : MonoBehaviour
    {
        [SerializeField]
        ChatBotClient chatBotClient;

        [SerializeField]
        ChatBotApi chatBotApi;

        [SerializeField]
        ChatBotView chatBotView;

        void Awake()
        {
            chatBotApi.Init();
            chatBotView.ConnectClicked += chatBotClient.Connect;
            chatBotView.DisconnectClicked += chatBotClient.Disconnect;
            chatBotView.AutoHelloClicked += chatBotClient.SendAutoHello;
        }

        void OnDestroy()
        {
            if (chatBotView == null)
                return;

            chatBotView.ConnectClicked -= chatBotClient.Connect;
            chatBotView.DisconnectClicked -= chatBotClient.Disconnect;
            chatBotView.AutoHelloClicked -= chatBotClient.SendAutoHello;
        }
    }
}
