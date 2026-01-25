using System.Text;
using ChatBot;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests
{
    public class CreatePlayerRequest : MonoBehaviour
    {
        [SerializeField]
        string twitchName;

        [SerializeField]
        int gold;
        
        string url = "http://localhost:8080/api/players";
        
        public void TestCreatePost()
        {
            
        }
        
        async void CreatePlayerRequestAsync(string url)
        {
            PlayerObject playerObject = new PlayerObject(twitchName)
            {
                twitchName = twitchName,
                gold = gold
            };

            string jsonString = JsonUtility.ToJson(playerObject);
            
            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
            
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            //request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Данные успешно отправлены!");
            }
            else
            {
                Debug.Log("Ошибка: " + request.error);
            }
        }
    }
}