using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Experimental.Rendering.Universal;
public class LevelManager : MonoBehaviour
{
    //Character slowly descends and level spawns from below

    public float speed;
    public GameObject RopeObject; //contains player and camera lol

    public bool gameStart;

    public AudioClip Gameoversong;
    public Animator ropeAnim;

    public Light2D gunLight;
    public Light2D globalLight;
    bool fadeLights;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeLights)
        {
            gunLight.enabled = false;
            globalLight.intensity -= Time.deltaTime;
        }

    }
    [Button]
    public void GameOver()
    {
        ropeAnim.SetBool("isGameOver",true);
        AudioManager.instance.isGameOver=true;
        fadeLights = true;
    }

}
