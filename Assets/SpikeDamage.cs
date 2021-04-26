using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    BoxCollider2D _col;
    public AudioClip goreStabSound;
    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision!=null)
        {
            SendDamage(collision.gameObject.transform.position);
            AudioManager.instance.PlaySoundAtLocation(goreStabSound, 0.3f, transform.position);
        }
    }
    void SendDamage(Vector2 pos)
    {
       Collider2D[] col = Physics2D.OverlapBoxAll(pos, _col.size,0);
        if(col!=null)
        {
            for(int i=0; i< col.Length;i++)
            {
                if(col[i].GetComponent<IDamageable>()!=null)
                    col[i].GetComponent<IDamageable>().OnTakeDamage(1);
            }
        }
    }
}
