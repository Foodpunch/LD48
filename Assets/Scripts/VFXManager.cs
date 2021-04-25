using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    public List<GameObject> EffectsList;
    List<GameObject> spawnedEffects = new List<GameObject>();
    // Start is called before the first frame update
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEffect(Effect data)
    {
        Quaternion lookRot = Quaternion.FromToRotation(data.originDirection, data.contact.normal);
        GameObject FXClone = Instantiate(EffectsList[data.index], data.contact.point, lookRot);
        FXClone.transform.SetParent(transform);
        spawnedEffects.Add(FXClone);

    }
}

public class Effect
{
    public int index;
    public Vector3 originDirection;
    public ContactPoint2D contact;

    public Effect(int _index, Vector3 _origDir, ContactPoint2D _contact)
    {
        index = _index;
        originDirection = _origDir;
        contact = _contact;
    }
}
