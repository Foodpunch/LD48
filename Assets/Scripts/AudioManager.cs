using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<AudioSource> ListofAudioSources = new List<AudioSource>();

    [SerializeField]
    AudioSource source;


    //hard code babeyyy~
    public AudioClip[] CasingSounds;
    public AudioClip[] EnemySounds;
    public AudioClip[] ExplosionSounds;
    public AudioClip[] ImpactSounds;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //default volume is 0.3f
    public void PlaySoundAtLocation(AudioClip clip, Vector2 pos)
    {
        AudioSource temp = Instantiate(source, pos, Quaternion.identity);
        ListofAudioSources.Add(temp);
        temp.clip = clip;
        temp.Play();
        RemoveUnusedAudioSource();
    }
    public void PlaySoundAtLocation(AudioClip clip,float volume, Vector2 pos)
    {
        AudioSource temp = Instantiate(source, pos, Quaternion.identity);
        ListofAudioSources.Add(temp);
        temp.volume = volume;
        temp.clip = clip;
        temp.Play();
        RemoveUnusedAudioSource();
    }
    public void PlaySoundAtLocation(AudioClip clip, float volume, Vector2 pos,bool randPitch = false)
    {
        AudioSource temp = Instantiate(source, pos, Quaternion.identity);
        ListofAudioSources.Add(temp);
        if(randPitch) temp.pitch *= Random.Range(0.8f, 2.5f);   //below 0 can't hear anything, 0.5f sounds slowww 2.5f sounds fast
        temp.volume = volume;
        temp.clip = clip;
        temp.Play();
        RemoveUnusedAudioSource();
    }


    void RemoveUnusedAudioSource()
    {
        if(ListofAudioSources!=null)
        {
            for(int i=0; i<ListofAudioSources.Count; i++)
            {
                if(!ListofAudioSources[i].isPlaying)
                {
                    Destroy(ListofAudioSources[i].gameObject);
                    ListofAudioSources.RemoveAt(i);
                }
            }    
        }
    }    


}
