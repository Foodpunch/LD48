using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class RoomManager : MonoBehaviour
{
    public Queue<GameObject> SpawnedRooms = new Queue<GameObject>();
    public List<RoomScript> ListOfRooms;

    public static RoomManager instance;
    public float RoomScrollSpeed = 0.5f;
    public Transform roomParent;

   public float difficulty = 1;

    float lastYPos = -9f;

    public Text depthsText;
    public bool stopGame;
    //keep count of X number of rooms, i.e 8 rooms
    //always maintain that there are 8, any that exceed the level will be removed.
    //Remember!! Space to speed up!! probably static float for speed or somethign

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            RoomScrollSpeed = 2.5f*difficulty;
        }
        else
        {
            RoomScrollSpeed = 0.5f*difficulty;
        }
        if(!stopGame)
        roomParent.Translate(Vector2.up * RoomScrollSpeed * Time.deltaTime);
        difficulty =Mathf.Clamp(difficulty, 1f, 30f);
        depthsText.text = (69 + (int)roomParent.transform.position.y).ToString() + "m";
    }
    [Button]
    public void SpawnNextRoom()
    {
        RoomScript roomToSpawn = RandomRoom();
        Vector2 RoomPos = new Vector2(0,lastYPos-roomToSpawn.roomSize.y);
        GameObject roomClone = Instantiate(roomToSpawn.gameObject, roomParent);
        roomClone.transform.localPosition = RoomPos;
        lastYPos = RoomPos.y;
        difficulty += 0.2f;
        //roomClone.transform.SetParent(roomParent);
    }
    public void Init()
    {
        for(int i =0; i<4;i++)
        {
            SpawnNextRoom();
        }
    }

    RoomScript RandomRoom()
    {
        int rand = Random.Range(0, ListOfRooms.Count);
        return (ListOfRooms[rand]);
    }
}
