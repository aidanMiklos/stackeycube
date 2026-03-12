using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelUI : MonoBehaviour
{
    SaveSystem s = new SaveSystem();
    public Sprite star;
    void Awake()
    {
        GameData data = s.Load();
        int lvlNumber = 0;
        foreach(level l in data.Levels){
            transform.Find(l.name).Find("Slider").GetComponent<Slider>().value = (float)(l.maxPercent/100f);
            if(l.star1){
                transform.Find(l.name).Find("Star 1").GetComponent<Image>().sprite = star;
            }if(l.star2){
                transform.Find(l.name).Find("Star 2").GetComponent<Image>().sprite = star;
            }if(l.star3){
                transform.Find(l.name).Find("Star 3").GetComponent<Image>().sprite = star;
            }
            
            lvlNumber++;
        }
    }

}
