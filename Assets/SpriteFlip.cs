using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    public SpriteRenderer spriteToFlip;
    public Transform transformToTrack;
    public float  minAngle, maxAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transformToTrack.rotation.eulerAngles.z;
        spriteToFlip.flipY = (angle > maxAngle && angle < minAngle);
    }
}
