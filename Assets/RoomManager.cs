using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Queue<GameObject> SpawnedRooms;
    public List<RoomScript> ListOfRooms;

    public static float RoomScrollSpeed = 0.5f;

    //keep count of X number of rooms, i.e 8 rooms
    //always maintain that there are 8, any that exceed the level will be removed.
    //Remember!! Space to speed up!! probably static float for speed or somethign

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            RoomScrollSpeed = 2f;
        }
        else
        {
            RoomScrollSpeed = 0.5f;
        }
    }
}
