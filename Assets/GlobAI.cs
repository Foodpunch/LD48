using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobAI : MonoBehaviour,IDamageable
{
    Rigidbody2D _rb;

    float speed = 1.3f;
    float DistanceToPlayer;
    //Bezier curve stuff lol
    Vector2 midPoint;
    Vector2 startPos;
    float t;
    bool set;

    Vector2 targetPos;
    Animator _anim;

    bool pounce;
    bool dead;
    public enum State
    {
        IDLE,
        READY,
        POUNCE,
        DEAD
    }
    public State GlobState = State.IDLE;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = transform.position.y - PlayerScript.instance.transform.position.y;
        switch (GlobState)
        {
            case State.IDLE:
                if (DistanceToPlayer >= -10f)
                {
                    GlobState = State.READY;
                    AudioManager.instance.PlaySoundAtLocation(AudioManager.instance.EnemySounds[6], 0.2f, transform.position);
                }
                break;
            case State.READY:
                _anim.SetTrigger("globready");
                if (DistanceToPlayer >= Random.Range(-3f,-7f))
                {
                    GlobState = State.POUNCE;
                    targetPos = PlayerScript.instance.transform.position;
                    startPos = transform.position;
                }
                break;
            case State.POUNCE:
                _anim.SetTrigger("globpounce");
                if(pounce)
                {
                    if (!set)
                    {
                        midPoint = CalculateMidPointByAngle(45f) + startPos;
                        AudioManager.instance.PlaySoundAtLocation(AudioManager.instance.EnemySounds[2], 0.2f, transform.position);
                        set = true;
                    }
                    t += Time.deltaTime * speed;
                    if (t >= 0.8f) Die();
                    transform.position = DoBezier(startPos, midPoint, targetPos, t);
                    //transform.Translate(DoBezier(startPos, midPoint, targetPos, t));
                }

                break;
            case State.DEAD:
                break;
        }
    }
    public void PounceHack()
    {
        pounce = true;
    }
    public void OnTakeDamage(float damage)
    {
        Die();      
    }
    Vector2 CalculateMidPointByAngle(float angle)
    {
        Vector2 dir = (Vector2)PlayerScript.instance.transform.position - startPos;
       
        float a = angle * Mathf.Deg2Rad;
        float distance = dir.magnitude;
        //some math to calulate the midpoint between target and AI's higest point by 45deg angle
        //or at least I think that's what it does
        dir.y = distance * Mathf.Tan(a);
        dir.Normalize();
        dir *= distance / 2f;
        //Debug.DrawRay(transform.position, dir, Color.red, 3f);
        return dir;
    }
    Vector2 DoBezier(Vector2 start, Vector2 mid, Vector2 end, float t)
    {
        Vector2 Line1 = Vector2.Lerp(start, mid, t);
        Vector2 Line2 = Vector2.Lerp(mid, end, t);
        Vector2 Final = Vector2.Lerp(Line1, Line2, t);
        return Final;
    }
    void Die()
    {
        if(!dead)
        {
            dead = true;
            gameObject.SetActive(false);
            GenericEffect explosionFX = new GenericEffect(1, transform.position);
            VFXManager.instance.SpawnEffect(explosionFX);
            VFXManager.instance.Poof(transform.position);
            AudioManager.instance.PlaySoundAtLocation(AudioManager.instance.EnemySounds[0], 0.2f, transform.position);
            CameraManager.instance.Shake(0.2f);
            LevelManager.instance.AddCount();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.collider.GetComponent<IDamageable>() != null && collision.collider.gameObject.tag =="Player")
            {
                collision.collider.GetComponent<IDamageable>().OnTakeDamage(1);
                Die();
            }
        }
    }
}
