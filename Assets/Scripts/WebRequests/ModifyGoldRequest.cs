using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests
{
    public class ModifyGoldRequest
    {
        public async UniTask SendModifyGold(string twitchName, int amount)
        {
            string url = $"http://localhost:8080/api/players/{twitchName}/gold?amount={amount}";
            
            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            { 
                Debug.Log($"ModifyGold Success: Gold updated for {twitchName} by {amount}. (Status: {request.responseCode})");
                return;
            }
            
            Debug.LogError($"ModifyGold Failed: {request.error}");
        }
    }
}