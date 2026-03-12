using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateSlider : MonoBehaviour
{
    GameObject player;
    GameObject win;
    Slider slider;
    TMP_Text text;
    public float percent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        win = GameObject.Find("WIN");
        text = gameObject.GetComponentInChildren<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        try{
            float playerPos = player.transform.position.x;
            float winPos = win.transform.position.x;
            percent = ((playerPos/winPos));
            gameObject.GetComponent<Slider>().value = percent;
            text.text = ((int)(percent*100)).ToString()+"%";
        }catch{

        }
    }
}
