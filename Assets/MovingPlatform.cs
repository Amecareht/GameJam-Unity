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
    public PlayerMove player;
    public Rigidbody2D rb2d;
    public Vector3 moveDirection;
    Vector3 targetPos;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        DirectionCalculate();
    }

    private void Update()
    {
        Vector2 target = currentMoveTarget();
     //   platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

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
            targetPos = startPoint.position;
            DirectionCalculate();
            return startPoint.position;
        }

      
            targetPos = endPoint.position;
            DirectionCalculate();
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
            player = other.gameObject.GetComponent<PlayerMove>();
            player.onPlateform = true;
            player.platformRb = rb2d;

        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = moveDirection * speed;
    }

    void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            player.onPlateform = false;
        }
    }
}
