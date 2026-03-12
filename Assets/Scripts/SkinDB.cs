using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDB : MonoBehaviour
{
    public Skin[] skins;
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    public Skin FindSkin(string name){

        foreach(Skin s in skins){
            if(s.name == name){
                return s;
            }
        }
        Debug.Log("COULDNT FIND SKIN");
        return skins[0];

    }
}
