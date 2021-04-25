using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class PlayerScript : MonoBehaviour
{
    Vector3 mouseInput;
    Vector3 mouseDir;
    public GameObject gun;

    Rigidbody2D _rb;

    public SpriteRenderer[] playerSpriteRenderers;
    public Animator playerAnim;
    public int health = 9;

    bool isHurt;
    bool isDead;
    float flashTime;
    float iFrameDuration =2f;
    public static PlayerScript instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDirectionToMouse();
        if (isHurt && !isDead) SpriteFlash();
    }
    void SetDirectionToMouse()
    {
        mouseInput = Input.mousePosition;       //mouse pos in pixel
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mouseInput);         //convert to world pos
        mousePosWorld.z = 0;                                                        //make sure that z is 0 cos 2D
        mouseDir = mousePosWorld - transform.position;                              //get the direction, pos to player
        gun.transform.right = mouseDir;                                             
    }
    [Button]
    public void TakeDamage()
    {
        isHurt = true;
        playerAnim.SetTrigger("isHurt");
        AudioManager.instance.PlayCachedSound(AudioManager.instance.HurtSounds, transform.position, 0.3f, true);
    }
   public void SpriteFlash()
    {
        flashTime += Time.deltaTime;
        if (flashTime < iFrameDuration)
        {
            float x = flashTime * 10f;
            Color tempColor = new Color(1f, 1f, 1f, Mathf.RoundToInt(x) % 2);
            ChangeSpriteAlpha(tempColor);
        }
        else
        {
            flashTime = 0;
            isHurt = false;
            ChangeSpriteAlpha(new Color(1, 1, 1, 1));
            
        }
    }
    void ChangeSpriteAlpha(Color color)
    {
        if(playerSpriteRenderers!=null)
        {
            for (int i = 0; i < playerSpriteRenderers.Length; i++)
            {
                playerSpriteRenderers[i].color = color;
            }
        }
       
    }
}
