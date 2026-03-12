using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class Pillar : MonoBehaviour
{
    GameObject player;
    public GameObject explosion;
    public GameObject blockPrefab;
    private CinemachineImpulseSource source;

    SaveSystem s = new SaveSystem();


    void Awake()
    {
        GameData data = s.Load();
        source = GetComponent<CinemachineImpulseSource>();
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Pillar");
        try{
            SkinDB db = GameObject.Find("SkinDB").GetComponent<SkinDB>();
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = db.FindSkin(data.block).artwork;
        }catch{

        }

        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Obstacle"){
                    player.GetComponent<Player>().Fall();
                    Die();
        }
    }

    public void Die(){
        GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().playSFX("pillar_explosion");
        Instantiate(explosion, new Vector3(transform.position.x,transform.position.y, transform.position.z), transform.rotation);
        Destroy(gameObject);
        //source.GenerateImpulse();
    }
}
