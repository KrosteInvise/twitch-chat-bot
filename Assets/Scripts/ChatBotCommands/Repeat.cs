using System.Collections.Generic;
using System.Linq;
using ChatBot;
using TMPro;
using TwitchLib.Client.Models.Common;
using UnityEngine;

namespace ChatBotCommands
{
    public class Repeat
    {
        public void RepeatExecute(TMP_InputField repeatTextInputField)
        {
            List<string> arguments = Helpers.ParseQuotesAndNonQuotes(repeatTextInputField.text);
            string iterations = arguments.First();
            string message = arguments.ElementAtOrDefault(1);

            if (arguments.Count != 2 || !int.TryParse(iterations, out int repeatCount) || repeatCount <= 0 || repeatCount > 8)
            {
                Debug.LogError("<color=\"red\">Something went wrong when trying to spam</color>");
                return;
            }
            
            for (int i = 0; i < repeatCount; i++)
                ChatEventMediator.InvokeRespond($"{message}");
        }
    }
}