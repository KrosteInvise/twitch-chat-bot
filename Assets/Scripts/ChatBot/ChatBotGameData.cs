using System.IO;
using UnityEngine;

namespace ChatBot
{
    public static class ChatBotGameData
    {
        static string fileName = "ChatBotGameData.json";
        static string projectRoot = Directory.GetParent(Application.dataPath)?.FullName;
        static string fullPath = Path.Combine(projectRoot, fileName);
        
        public static PlayersDataBase Load()
        {
            if (!File.Exists(fullPath))
            {
                Debug.LogError("Data file doesn't exist! Creating new save file!");
                File.Create(fullPath);
                return new PlayersDataBase();
            }
            
            PlayersDataBase playersData = JsonUtility.FromJson<PlayersDataBase>(File.ReadAllText(fullPath));
            return playersData;
        }
    
        public static void Save(PlayersDataBase playersData)
        {
            File.WriteAllText(fullPath,JsonUtility.ToJson(playersData, true)); 
        }
    }
}

