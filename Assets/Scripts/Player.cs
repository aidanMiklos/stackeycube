using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject explosion;
    public GameObject pillar;
    private Rigidbody2D rb;
    public float layer = 0.0f;

    public Animator squashStretch;
    SMScript sceneManager;
    public UpdateSlider updateSlider;
    SaveSystem s = new SaveSystem();
    GameData data;
    int maxPercent;

    public SpriteRenderer skin;

    bool star1 = false;
    bool star2 = false;
    bool star3 = false;

    public bool swipePortal = false;

    SkinDB db;
    void Awake()
    {
        GameData data = s.Load();
        try{
            db = GameObject.Find("SkinDB").GetComponent<SkinDB>();
            skin.sprite = db.FindSkin(data.skin).artwork;
        }catch{
            
        }
        
        
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        Scene scene = SceneManager.GetActiveScene(); 
        sceneManager = GameObject.Find("SceneManager").GetComponent<SMScript>();
        data = s.Load();
        maxPercent = data.Levels.Find(x => x.name == scene.name).maxPercent;
    }

    void Update()
    {
        if((int)(updateSlider.percent*100) > maxPercent){
            data = s.Load();
            Scene scene = SceneManager.GetActiveScene(); 
            data.Levels.Find(x => x.name == scene.name).maxPercent = (int)(updateSlider.percent*100);
            maxPercent = (int)(updateSlider.percent*100);
            s.Save(data);
        }

        if((updateSlider.percent*100) >= 100){
            GameData data = s.Load();
            Scene scene = SceneManager.GetActiveScene(); 
            level levelData = data.Levels.FindLast(x => x.name == scene.name);
            data.Levels.Find(x => x.name == scene.name).maxPercent = 100;
            data.Levels.Find(x => x.name == scene.name).isCompleted = true;
            if(star1 == true){
                levelData.star1 = true;
            }if(star2 == true){
                levelData.star2 = true;
            }if(star3 == true){
                levelData.star3 = true;
            }
            s.Save(data);
            transform.GetChild(0).gameObject.SetActive(false);
            sceneManager.Win();
        }
        float y = transform.position.y;
        layer = (int)((2.6 + y+1));


        if(Input.GetKeyDown(KeyCode.UpArrow) && swipePortal){
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("jump");
            layer += 1;
            gameObject.transform.position = new Vector3(transform.position.x, layer-3.5f, transform.position.z);
        }

        
        if(Input.GetKeyDown(KeyCode.DownArrow) && swipePortal && layer!= 0){
            try{
                gameObject.transform.position = new Vector3(transform.position.x, layer-4.5f, transform.position.z);
                GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("jump");
            }catch{

            }
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && !swipePortal){
            squashStretch.SetTrigger("Up");
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("jump");
            layer += 1;
            gameObject.transform.position = new Vector3(transform.position.x, layer-3.5f, transform.position.z);

            GameObject clone = Instantiate(pillar, new Vector3(transform.position.x, layer-4.5f, transform.position.z), transform.rotation);

            //clone.transform.position = new Vector3(transform.position.x, layer-4.5f, transform.position.z);
        }

    }



    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Obstacle"){
            GameObject[] pillars = GameObject.FindGameObjectsWithTag("Pillar");
            foreach (GameObject pillar in pillars){
                pillar.GetComponent<Pillar>().Die();
            }
            Instantiate(explosion, new Vector3(transform.position.x,transform.position.y, transform.position.z), transform.rotation);
            // save level progress 
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("pillar_explosion");
            GameObject.Find("Main Camera").GetComponent<CameraMovement>().cameraSpeed = 0;
            GameObject.Find("SceneManager").GetComponent<SMScript>().ReloadScene();
            Destroy(gameObject);
            
        }

        if(collision.gameObject.tag == "Portal"){
            gameObject.transform.position = new Vector3(transform.position.x, layer-3.5f, transform.position.z);
            GameObject[] pillars = GameObject.FindGameObjectsWithTag("Pillar");
            foreach (GameObject pillar in pillars){
                pillar.GetComponent<Pillar>().Die();
            }
            rb.gravityScale = 0;
            swipePortal = true;
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("portal");
        }

        if(collision.gameObject.tag == "Portal End"){
            GameObject[] pillars = GameObject.FindGameObjectsWithTag("Pillar");
            foreach (GameObject pillar in pillars){
                pillar.GetComponent<Pillar>().Die();
            }
            rb.gravityScale = 5;
            swipePortal = false;
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("portal");
        }

        if(collision.gameObject.tag == "Star"){
            int starNum = collision.gameObject.GetComponent<Star>().number;
            collision.gameObject.transform.parent.gameObject.SetActive(false);
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("star");
            GameData data = s.Load();
            Scene scene = SceneManager.GetActiveScene(); 
            level levelData = data.Levels.FindLast(x => x.name == scene.name);

            if(starNum == 1){
                star1 = true;
            }else if(starNum == 2){
                star2 = true;
            }else{
                star3 = true;
            }

            int index = data.Levels.FindLastIndex(x => x.name == scene.name);
            data.Levels.RemoveAt(index);
            data.Levels.Add(levelData);
            s.Save(data);

        }
    }

    public void Fall(){
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }
}
