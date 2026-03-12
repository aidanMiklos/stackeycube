using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class level
{
    public string name = "";
    public bool isCompleted = false;
    public int maxPercent = 0;
    public int attempts = 0;
    public bool star1 = false;
    public bool star2 = false;
    public bool star3 = false;

}

[System.Serializable]
public class GameData
{
    public bool fullScreen = true;
    public int graphics = 0;
    public float volumeLevel = 1.0f;
    public float sfxLevel = 1.0f;
    public string skin = "Red Smile";

    public string block = "White";

    public List<level> Levels = new List<level>() { 
    };

}
