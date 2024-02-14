using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    private int direction = 1;
    public float speed;


    private void Update()
    {
        Vector2 target = currentMoveTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;
        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 currentMoveTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        return endPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            Debug.Log("debug");
            
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
