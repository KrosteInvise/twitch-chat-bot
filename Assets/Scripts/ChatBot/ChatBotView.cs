using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatBot
{
    public class ChatBotView : MonoBehaviour
    {
        [SerializeField]
        Button connectButton, disconnectButton, repeatButton;
        
        [SerializeField]
        TextMeshProUGUI chatText;
        
        [SerializeField]
        TMP_InputField repeatTextInputField;

        public void Init(ChatBotClient chatBotClient, ChatBotConfig config)
        {
            //connectButton.onClick.AddListener(Connect);
            //disconnectButton.onClick.AddListener(Disconnect);
            //repeatButton.onClick.AddListener(() => new Repeat().RepeatExecute(chatBotClient, config, repeatTextInputField));
        }
    }
}