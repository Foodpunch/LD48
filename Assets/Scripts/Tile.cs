using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour,IDamageable
{
    public float health;
    public bool isDestructible;
    public int blockChance;
    public bool isStatic;
    public AudioClip DestroySound;
    public GameObject gibs;
    bool isDestroyed;
    public void OnTakeDamage(float damage)
    {
        if (!isDestructible) return;
        health -= damage;
        if(health<=0 && !isDestroyed)
        {
            DespawnBlock();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RollTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DespawnBlock()
    {
        gameObject.SetActive(false);
        if (DestroySound == null) return;
        AudioManager.instance.PlayCachedSound(AudioManager.instance.WoodBreakSounds, transform.position, 0.3f);
        VFXManager.instance.Poof(transform.position);
        isDestroyed = true;

        //spawn whatever effects here
    }
    public void RollTile()
    {
        if(!isStatic)
        {
            int rand = Random.Range(1, 100);
            gameObject.SetActive(rand <= blockChance);  //if player rolls chance, setactive false
        }
    }
}
