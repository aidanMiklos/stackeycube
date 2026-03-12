using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    SaveSystem s = new SaveSystem();
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public Toggle fullscreenToggle;
    public TMP_Dropdown qaulityDropdown;
    void Start(){
        GameData data = s.Load();
        fullscreenToggle.isOn = data.fullScreen;
        setFullScreen(data.fullScreen);
        qaulityDropdown.value = data.graphics;
        SetQaulity(data.graphics);
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width ==  Screen.currentResolution.width && resolutions[i].height ==  Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution (int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQaulity(int qaulityIndex){
        GameData data = s.Load();
        data.graphics = qaulityIndex;
        QualitySettings.SetQualityLevel(Math.Abs(qaulityIndex-2));
        s.Save(data);
    }

    public void setFullScreen(bool isFullScreen){
        GameData data = s.Load();
        data.fullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        s.Save(data);
    }


}
