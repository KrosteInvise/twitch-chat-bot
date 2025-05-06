using TwitchLib.PubSub.Events;
using TwitchLib.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace ChatBot
{
	public class ChatBotPubSub : MonoBehaviour
	{
		[SerializeField]
		Text logText;
        
		PubSub pubSub;

		void Start()
		{
			pubSub = new PubSub();

			pubSub.OnFollow += OnFollow;
			
			pubSub.Connect();
		}

		void OnFollow(object sender, OnFollowArgs e)
		{
			logText.text += $"{e.Username} is just followed!\n";
		}
	}
}
