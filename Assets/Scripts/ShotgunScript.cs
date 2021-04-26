using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{

    public GameObject bullet;
    public GameObject shell;

    bool isFiring;
    float fireRate =1.5f;
    float recoil = 10f;
    int pelletCount = 5;
    float spreadAngle = 45f;
    float gunTime;
    float nextTimeToFire;

    public Transform shootPoint;
    [SerializeField]
    Rigidbody2D playerRB;
    [SerializeField]
    Animator _gunAnim;
    [SerializeField]
    Animator _muzzleFlashAnim;
    [SerializeField]
    AudioClip shotgunSound;
    // Start is called before the first frame update
    void Start()
    {
        _gunAnim.GetComponent<Animator>();
        _muzzleFlashAnim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            gunTime += Time.deltaTime;
            if (Time.time >= nextTimeToFire)
            {
             //   _muzzleFlashAnim.SetTrigger("Flash");
                AudioManager.instance.PlaySoundAtLocation(shotgunSound,0.25f, transform.position,true);
                CameraManager.instance.Shake(.25f, false, true);
                SpawnBullet();
                GunRecoil();
                nextTimeToFire = Time.time + (1f / fireRate);
                _gunAnim.SetTrigger("shotgunFired");
            }
        }
        else
        {
            gunTime = 0;
           
        }
    }

    private void SpawnBullet()
    {
        for(int i=0; i< pelletCount; i++)
        {
            float spreadRange = Random.Range(-spreadAngle / 2, spreadAngle/2);
            Quaternion randomArc = Quaternion.Euler(0, 0, spreadRange);
            GameObject bulletClone = Instantiate(bullet, shootPoint.position, transform.rotation * randomArc);
        }
    }

    void GunRecoil()
    {
        Vector2 recoilDir = PlayerScript.instance.transform.position - shootPoint.position;
        recoilDir.Normalize();
        playerRB.AddForce(recoilDir * recoil,ForceMode2D.Impulse);
    }
}
