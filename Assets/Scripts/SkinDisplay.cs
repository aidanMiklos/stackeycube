using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinDisplay : MonoBehaviour
{
    public GameObject skinSprite;
    public Skin skin;

    void Start()
    {        
        if(!skin.locked){
            skinSprite.GetComponent<Image>().sprite = skin.artwork;
        }
        
    }


}
