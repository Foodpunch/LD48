using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public Vector2Int roomSize;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector2.up *RoomManager.RoomScrollSpeed* Time.deltaTime);
    }
}
