using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoom : MonoBehaviour
{
    public GameObject Room;
    public float rotation;
    private bool outside;

    public void Start()
    {
        rotation = 270f;
    }

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
        {
            outside = false;
            rotation += 90f;
            Room.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}

    
