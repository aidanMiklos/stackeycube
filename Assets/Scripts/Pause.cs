using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public static bool Paused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButtun;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(Paused){
                Resume();
            }else{
                PauseGame();
            }
        }

    }

    public void Resume(){
        GameObject.Find("Player").GetComponent<Player>().enabled = true;
        Paused = false;
        pauseMenuUI.SetActive(false);
        pauseButtun.SetActive(true);
        Time.timeScale = 1f;
        GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().Resume();
    }

    public void PauseGame(){
        GameObject.Find("Player").GetComponent<Player>().enabled = false;
        pauseMenuUI.SetActive(true);
        pauseButtun.SetActive(false);
        Time.timeScale = 0f;
        Paused = true;
        GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().Pause();
    }
}
