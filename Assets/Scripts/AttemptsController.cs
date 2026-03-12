using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class AttemptsController : MonoBehaviour
{
    SaveSystem s = new SaveSystem();
    TMP_Text text;
    int attempts = 0;
    void Awake()
    {
        GameData data = s.Load();
        Scene scene = SceneManager.GetActiveScene(); 
        attempts = data.Levels.Find(x => x.name == scene.name).attempts;
        text = gameObject.GetComponentInChildren<TMP_Text>();
        text.text = "ATTEMPT "+attempts.ToString();
    }


}
