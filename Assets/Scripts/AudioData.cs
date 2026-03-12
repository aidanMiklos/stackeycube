using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    public float volumeLevel;
    public float sfxLevel;

    public AudioData(AudioManager am){

        volumeLevel = am.volumeLevel;
        sfxLevel = am.sfxLevel;

    }
}
