using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Star : MonoBehaviour
{
    public int number;
    SaveSystem s = new SaveSystem();

    void Awake(){
            GameData data = s.Load();
            Scene scene = SceneManager.GetActiveScene(); 
            level levelData = data.Levels.FindLast(x => x.name == scene.name);
        if(number == 1){
            if(levelData.star1){
                Destroy(gameObject.transform.parent.gameObject);
            }
        }else if(number == 2){
            if(levelData.star2){
                Destroy(gameObject.transform.parent.gameObject);
            }
        }else{
            if(levelData.star3){
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

}
