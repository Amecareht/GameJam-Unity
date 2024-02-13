using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("PlayerMoves")] private float _inputValueX; // Horizontal input

    // Player movement speed
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private Vector3 direction;

    // Player position variables
    private float _posX;

    private Rigidbody2D rb;
  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _inputValueX = Input.GetAxis("Horizontal");
        if (_inputValueX != 0 || _inputValueX != 0)
            Move(); // If moving, call the Move() function
        else
        {
            // If not moving, set direction to zero and update animator
            direction = Vector3.zero;
        }


        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        
    }

    void Move()
    {
        // Get the current position of the player
        var position = transform.position;
        _posX = (position.x + (_inputValueX * Time.deltaTime * speed));
        // Set the new direction vector
        direction = new Vector2(_posX,transform.position.y);

        // Move the player to the new position
        transform.position = direction;
    }
}
