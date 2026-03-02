using System.Text;
using DTO;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace WebRequests
{
    public class PlayGambleRequest
    {
        string url = "http://localhost:8080/api/gambling/play";

        public async UniTask<Gamble> SendPlayGamble(string twitchName, int stake)
        {
            GambleRequestData data = new GambleRequestData { twitchName = twitchName, stake = stake };
            string jsonString = JsonUtility.ToJson(data);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
            
            using var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"PlayGamble Success: {request.downloadHandler.text}");
                return JsonUtility.FromJson<Gamble>(request.downloadHandler.text);
            }
            
            Debug.LogError($"PlayGamble Failed: {request.downloadHandler.text}");
            return null;
        }
    }
}