using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Music[] music;
    public string musicPlaying;

    public float volumeLevel;
    public float sfxLevel;
    string prevScene;
    SaveSystem s = new SaveSystem();

    void Awake()
    {

        musicPlaying = "LobbyMusic";
        DontDestroyOnLoad(this.gameObject);

        try{
            s.Load();
        }catch{
            GameData data = new GameData();
            level l = new level() { 
                name = "Run", 
                isCompleted = false, 
                maxPercent = 0, 
                attempts = 0,
                star1 = false,
                star2 = false,
                star3 = false
            };

            data.Levels.Add(l);

            l = new level() { 
                name = "Dreamland", 
                isCompleted = false, 
                maxPercent = 0, 
                attempts = 0,
                star1 = false,
                star2 = false,
                star3 = false
            };

            data.Levels.Add(l);

            l = new level() { 
                name = "Misty Heights", 
                isCompleted = false, 
                maxPercent = 0, 
                attempts = 0,
                star1 = false,
                star2 = false,
                star3 = false
            };

            data.Levels.Add(l);

            s.Save(data);
        }
        
        GameData newData = s.Load();

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = false;
            s.source.volume = newData.sfxLevel;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
        }
    }

    public void playSFX(string name){
        sounds = GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().sounds;
        foreach (Sound s in sounds){
            if(s.name == name){
                s.source.Play();
            }
        }
    }

    public bool CheckPlaying(string currentMusic){
        return GameObject.Find("AudioManager(Clone)/"+currentMusic).GetComponent<AudioSource>().isPlaying;
    }
    public void SwitchMusic(string currentMusic, string nextMusic)
    {
           GameObject.Find("AudioManager(Clone)/"+currentMusic).GetComponent<AudioSource>().Stop();
           GameObject.Find("AudioManager(Clone)/"+nextMusic).GetComponent<AudioSource>().Play();
           musicPlaying = nextMusic;
    }

    public void updateSFX(float volume){
        sounds = GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().sounds;
            foreach (Sound s in sounds){
                s.source.volume = volume;
        }
    }

    public void Reload(){
        //musicPlaying variable is returning null contrary to whats seen in inspector??? so I have to reassign it again here
        foreach( Music m in music){
            if(SceneManager.GetActiveScene().name == m.sceneName){
                musicPlaying = m.audioName;
            }
        }
        GameObject.Find("AudioManager(Clone)/"+musicPlaying).GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioManager(Clone)/"+musicPlaying).GetComponent<AudioSource>().Play();
    }

    public void Pause(){
        GameObject.Find("AudioManager(Clone)/"+musicPlaying).GetComponent<AudioSource>().Pause();
    }

    public void Resume(){
        GameObject.Find("AudioManager(Clone)/"+musicPlaying).GetComponent<AudioSource>().Play();
    }

    public void Update(){
        UpdateSound();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void UpdateSound(){
        //DataPersistanceManager.instance.LoadGame();'
        GameData data = s.Load();
        volumeLevel = data.volumeLevel;
        GameObject.Find("AudioManager(Clone)/"+musicPlaying).GetComponent<AudioSource>().volume = volumeLevel*0.25f;
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode model){
        foreach( Music m in music){
            if(SceneManager.GetActiveScene().name == m.sceneName && !CheckPlaying(m.audioName)){
                SwitchMusic(musicPlaying, m.audioName);
            }
        }
    }
    
}
