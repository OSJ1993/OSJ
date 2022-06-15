using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameMovementObject : MonoBehaviour
{
    [SerializeField] private ScrollGameJoyStick virtualJoystick;
    private float moveSpeed = 10;


    void Start()
    {
        
    }

    
    void Update()
    {
        float x = virtualJoystick.horizontal; // Left & Right
        float y = virtualJoystick.vertical; // Up & Down

        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
        }
    }
}
