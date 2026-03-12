using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SMScript : MonoBehaviour
{
    SaveSystem s = new SaveSystem();
    public GameObject audioManager;

    bool win = false;
    void Awake(){
        Time.timeScale = 1f;
        if (GameObject.Find ("AudioManager(Clone)") != null) {
             
         } else {
            Instantiate(audioManager, new Vector3(0,0,0), transform.rotation);
         }
    }
    public void ReloadScene(){
        audioManager.GetComponent<AudioManager>().Reload();
        StartCoroutine(ReloadSceneInside());
    }
    public IEnumerator ReloadSceneInside(){

        yield return new WaitForSeconds(0.6f);
        GameData data = s.Load();
        Scene scene = SceneManager.GetActiveScene(); 
        level levelData = data.Levels.FindLast(x => x.name == scene.name);
        levelData.attempts++;
        int index = data.Levels.FindLastIndex(x => x.name == scene.name);
        data.Levels.RemoveAt(index);
        data.Levels.Add(levelData);
        s.Save(data);
        SceneManager.LoadScene(scene.name);
        
    }
    public void GoToLevels(){
        SceneManager.LoadScene("Levels");
    }

    public void RunLevel(string name){
        SceneManager.LoadScene(name);
    }

    public void RunLevelWithAnimation(string name){
        audioManager.GetComponent<AudioManager>().Pause();
        GameObject.Find("Transition/Panel").GetComponent<Image>().raycastTarget = true;
        GameObject.Find("Transition/Panel").GetComponent<Animator>().SetTrigger("SceneTransitionOut");
        StartCoroutine(LoadWait(name));
    }

    public void Win(){
        if(!win){
        win = true;
        audioManager.GetComponent<AudioManager>().Pause();
        Time.timeScale = 0f;
        StartCoroutine(WinWait());
        }

    }

    IEnumerator WinWait(){
        GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("win");
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Levels");
    }

    IEnumerator LoadWait(string name){
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(name);
    }
}
