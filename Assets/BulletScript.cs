using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletDamage = 1f;
    float bulletSpeed =18f ;
    float timeToDisappear = 1.5f;

    float bulletAirTime;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletAirTime += Time.deltaTime;
        if (bulletAirTime >= timeToDisappear)
        {
            Destroy(gameObject);
        }
        _rb.velocity = transform.right * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            foreach(ContactPoint2D contact in collision.contacts)
            {
                if(contact.collider.gameObject.GetComponent<IDamageable>() != null)
                {
                    contact.collider.gameObject.GetComponent<IDamageable>().OnTakeDamage(bulletDamage);
                    //spawn bullet effect
                }
                //spawn environment effect
            }
           // Destroy(gameObject);
            return;
        }
    }
}
