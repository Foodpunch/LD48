using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour,IDamageable
{
    public float health;
    public bool isDestructible;
    public int blockChance;
    public bool isStatic;

    public void OnTakeDamage(float damage)
    {
        health -= damage;
        if(health<=0)
        {
            DespawnBlock();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DespawnBlock()
    {
        gameObject.SetActive(false);
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
