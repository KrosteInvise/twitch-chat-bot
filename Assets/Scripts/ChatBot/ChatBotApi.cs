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
            api = new Api
            {
                Settings =
                {
                    ClientId = Secrets.client_id,
                    Secret = Secrets.client_secret,
                    AccessToken = Secrets.bot_access_token
                }
            };
        }
    }
}
