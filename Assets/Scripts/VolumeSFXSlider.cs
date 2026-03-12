using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSFXSlider : MonoBehaviour
{
    public Slider volume;
    public Slider sfx;

    SaveSystem s = new SaveSystem();
    GameData data;

    void Start(){
        data = s.Load();
        volume.value = data.volumeLevel;
        sfx.value = data.sfxLevel;
    }

    public void VolumeChange(){
        data = s.Load();
        data.volumeLevel = volume.value;
        s.Save(data);
    }

    public void SFXChange(){
        data = s.Load();
        data.sfxLevel = sfx.value;
        GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().updateSFX(sfx.value);
        s.Save(data);
    }

}
