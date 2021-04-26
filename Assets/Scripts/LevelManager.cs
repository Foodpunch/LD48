using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    //Character slowly descends and level spawns from below

    public float speed;
    public GameObject RopeObject; //contains player and camera lol
    public GameObject wispspawner;
    public bool gameStart;

    public Animator ropeAnim;
    public Animator gameoverText;

    public Light2D gunLight;
    public Light2D globalLight;
    bool fadeLights;

    public static LevelManager instance;
    public CanvasGroup LivesUI;
    public CanvasGroup endscoregroup;
    public Text EndScore;

    public int ghostsKilled = 0;
    public Text ghostsKilledText;
    public Text endGhostsKilledText;

    public float endTimer = 0;
    float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //ghostsKilledText.GetComponent<Text>().text = "0";
        //   Screen.SetResolution(1080, 1920, false,60);
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if(fadeLights)
        {
            endTimer += Time.deltaTime;
            gunLight.enabled = false;
            globalLight.intensity -= Time.deltaTime;
            LivesUI.alpha -= Time.deltaTime;
        }
        if(endTimer >9f)
        {
            string text = ((int)RoomManager.instance.roomParent.transform.position.y + 69).ToString();
            EndScore.text = "Lowest Depth: " + text + "m";
            endGhostsKilledText.text = "Ghosts Banished : "+ghostsKilled.ToString();
            endscoregroup.alpha += Time.deltaTime;
        }
       
    }
    [Button]
    public void GameOver()      //very hacky and very messy. this is bad!
    {
        ropeAnim.SetBool("isGameOver",true);
        gameoverText.SetTrigger("isGameOver");
        RoomManager.instance.stopGame = true;
        AudioManager.instance.isGameOver=true;
        PlayerScript.instance.GameOver();
        wispspawner.SetActive(false);
        fadeLights = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player" && gameTime >=5f)       //this is especially bad! but whatever
        {
            GameOver();
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    public void AddCount()
    {
        ghostsKilled++;
        ghostsKilledText.text = ghostsKilled.ToString();
    }
}
