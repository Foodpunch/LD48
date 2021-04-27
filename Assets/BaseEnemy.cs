using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour,IDamageable
{
    public float health = 1f;

    Vector2 DirToPlayer;
    Rigidbody2D _rb;
    protected float moveSpeed = 2.5f;
    float timer;
    bool isDead;
    public void OnTakeDamage(float damage)
    {
        health -= damage;
        if(health<=0&&!isDead)
        {
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb=GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(0.8f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        DirToPlayer = PlayerScript.instance.transform.position - transform.position;

    }
    void FixedUpdate()
    {
        MoveToPlayer();
    }


    void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        GenericEffect explosionFX = new GenericEffect(1, transform.position);
        VFXManager.instance.SpawnEffect(explosionFX);
        VFXManager.instance.Poof(transform.position);
        AudioManager.instance.PlayCachedSound(AudioManager.instance.ExplosionSounds, transform.position, 0.2f);
        CameraManager.instance.Shake(0.2f);
        LevelManager.instance.AddCount();
    }
    void MoveToPlayer()
    {
        _rb.velocity = transform.up * RoomManager.instance.RoomScrollSpeed*moveSpeed;
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            float randAngle = Random.Range(-45f / 2, 45f / 2);
            Quaternion randomArc = Quaternion.Euler(0, 0, randAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, randomArc, 15f);
            timer = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision!=null)
        {
            if(collision.collider.GetComponent<IDamageable>()!=null)
            {
                collision.collider.GetComponent<IDamageable>().OnTakeDamage(1);
            }
        }
    }
}
