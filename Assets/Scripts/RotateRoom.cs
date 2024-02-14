using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoom : MonoBehaviour
{
    public GameObject Room;
    private float rotation = 0f;
    
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            rotation = 90f;
            Room.transform.rotation = Quaternion.Euler(0,0,rotation);
        }
    }
}
