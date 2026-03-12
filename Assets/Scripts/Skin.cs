using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Skin")]
public class Skin : ScriptableObject
{
    public new string name;
    public string unlockInstructions;
    public Sprite artwork;
    public bool locked;
}
