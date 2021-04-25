using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Vector3 mouseInput;
    Vector3 mouseDir;
    public GameObject gun;

    Rigidbody2D _rb;

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
    }
    void SetDirectionToMouse()
    {
        mouseInput = Input.mousePosition;       //mouse pos in pixel
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mouseInput);         //convert to world pos
        mousePosWorld.z = 0;                                                        //make sure that z is 0 cos 2D
        mouseDir = mousePosWorld - transform.position;                              //get the direction, pos to player
        gun.transform.right = mouseDir;                                             
    }
}
