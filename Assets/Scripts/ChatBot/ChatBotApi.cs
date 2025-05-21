using TwitchLib.Api.Services;
using TwitchLib.Unity;
using UnityEngine;

namespace ChatBot
{
    public class ChatBotApi : MonoBehaviour
    {
        LiveStreamMonitorService liveStreams;
        Api api;

        public void Init()
        {
            api = new Api();
            api.Settings.ClientId = Secrets.client_id;
            api.Settings.Secret = Secrets.client_secret;
            api.Settings.AccessToken = Secrets.bot_access_token;
        }
    }
}
