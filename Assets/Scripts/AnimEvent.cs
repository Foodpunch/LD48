using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
   public void PlaySound(AudioClip audioClip)
    {
        AudioManager.instance.PlaySoundAtLocation(audioClip, transform.position);
    }
}
