namespace ChatBot
{
    public static class ChatBotGameData
    {
        
        /*public static PlayersDataBase Load()
        {
            var request = new GetAllPlayersRequest();
            
            request.GetAllRequestAsync();
            
            return JsonConvert.DeserializeObject<PlayersDataBase>();
            
            if (!File.Exists(fullPath))
            {
                Debug.LogError("Data file doesn't exist! Creating new save file!");
                File.Create(fullPath);
                return new PlayersDataBase();
            }
            
            PlayersDataBase playersData = JsonUtility.FromJson<PlayersDataBase>(File.ReadAllText(fullPath));
            return playersData;
        }*/
    
        public static void Save(PlayersDataBase playersData)
        {
            //File.WriteAllText(fullPath,JsonUtility.ToJson(playersData, true)); 
        }
    }
}

