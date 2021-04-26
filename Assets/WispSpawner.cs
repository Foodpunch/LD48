using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSpawner : MonoBehaviour
{
    public GameObject Wisp;
    public float spawnInterval = 5f;
    public AudioClip[] wispSounds;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=spawnInterval)
        {
            timer = 0;
            spawnInterval = Random.Range(5f, 8f);
            Vector2 randomPos = new Vector2(transform.position.x + Random.Range(-2.1f, 2.1f), transform.position.y);
            Instantiate(Wisp, randomPos, Quaternion.identity);

            AudioManager.instance.PlaySoundAtLocation(wispSounds[Random.Range(0, wispSounds.Length)], 0.15f, transform.position);
        }
    }
}
