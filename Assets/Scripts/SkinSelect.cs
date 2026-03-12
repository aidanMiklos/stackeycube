using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinSelect : MonoBehaviour
{
    SaveSystem s = new SaveSystem();
    void Start()
    {
        GameData data = s.Load();
        ResetSkins();
        SetSkin(data.skin);
    }

    public void SetSkin(string name){
        ResetSkins();
        GameObject.Find("Canvas/Page Area/Page 1").transform.Find(name).GetChild(0).GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        GameData newData = s.Load();
        newData.skin = name;
        s.Save(newData);
    }

    public void SetBlock(string name){
        ResetSkins();
        GameObject.Find("Canvas/Page Area/Page 2").transform.Find(name).GetChild(0).GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        GameData newData = s.Load();
        newData.block = name;
        s.Save(newData);
    }

    public void StartBlock(){
        string name = s.Load().block;
        ResetSkins();
        GameObject.Find("Canvas/Page Area/Page 2").transform.Find(name).GetChild(0).GetComponent<Image>().color = new Color(1f,1f,1f,1f);
    }

    public void StartSkins(){
        string name = s.Load().skin;
        ResetSkins();
        GameObject.Find("Canvas/Page Area/Page 1").transform.Find(name).GetChild(0).GetComponent<Image>().color = new Color(1f,1f,1f,1f);
    }

    public void ResetSkins(){
        GameObject.Find("Canvas/Page Area").GetComponentsInChildren<Transform>();
        List<Transform> list = new List<Transform>();
        for(int i = 0; i <= GameObject.Find("Canvas/Page Area").transform.childCount-1; i++){
            list.Add(GameObject.Find("Canvas/Page Area").transform.GetChild(i).GetComponent<Transform>());
        }

        Transform[] page = list.ToArray();

        foreach(Transform p in page){
            for(int s = 0; s <= p.childCount-1; s++ ){
                p.GetChild(s).GetChild(0).GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.7f);
            }
        }
    }
}
