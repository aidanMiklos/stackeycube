using UnityEngine;
using System.IO;
using System;


public class SaveSystem 
{
    public IDataService DataService = new JsonDataService();

    public void Save(GameData data){
        try
        {
            DataService.SaveData("/game-data.json", data, false);
        }

        catch(Exception e)
        {
            Debug.LogError("could not save file "+ e);
        }

    }

    public GameData Load(){
        GameData data = null;
        try
        {
            data = DataService.LoadData<GameData>("/game-data.json", false);
        }
        catch (Exception e)
        {

            throw e;
        }
        return data;
    }

}
