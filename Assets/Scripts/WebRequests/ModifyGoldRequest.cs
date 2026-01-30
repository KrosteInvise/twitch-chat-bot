using System.Text;
using ChatBot;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests
{
    public class ModifyGoldRequest
    {
        public async UniTask ModifyGold(string twitchName, int amount)
        {
            var requestData = new GoldTransaction(amount);
            string jsonString = JsonUtility.ToJson(requestData);
            string url = $"http://localhost:8080/api/players/{twitchName}/gold";
            
            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            { 
                Debug.Log(request.downloadHandler.text);
                return;
            }
            
            Debug.LogError($"ModifyGold Failed: {request.error}");
        }
    }
}