using System.Text;
using DTO;
using UnityEngine;
using UnityEngine.Networking;
using UniTask = Cysharp.Threading.Tasks.UniTask;

namespace WebRequests
{
    public class CreatePlayerRequest
    {
        public async UniTask SendCreatePlayerRequest(string twitchName)
        {
            string url = "http://localhost:8080/api/players/create";

            PlayerObject playerObject = new PlayerObject(twitchName);

            string jsonString = JsonUtility.ToJson(playerObject);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);

            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Данные успешно отправлены!");
                return;
            }
            
            Debug.LogError("Ошибка: " + request.error);
        }
    }
}