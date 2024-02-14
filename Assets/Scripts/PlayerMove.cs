using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("PlayerMoves")] private float _inputValueX; // Horizontal input
    private float _inputValueY; // Vertical input

    // Player movement speed
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float baseSpeed;
    [SerializeField]private float sprint;
    private float raycastDistance = 4f;
    public Vector2 rightOffset, leftOffset;
    public float collisionRadius = 0.25f;
    [SerializeField] private bool onWall;
    [SerializeField] private bool RightWall,LeftWall;
    [SerializeField] private int wallLeftOrRight;
    private bool wallJumping;
    
    

    private Vector3 direction;

    // Player position variables
    private float _posX;

    private Rigidbody2D rb;
    [SerializeField] private bool IsSprinting;


    public Transform GroundCheck;
    public LayerMask groundLayer;
    [SerializeField] private int MaxJumps;
    [SerializeField] private int JumpLeft;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpLeft = MaxJumps;
        sprint = speed * 1.4f;


    }

    // Update is called once per frame
    void Update()
    {
        _inputValueX = Input.GetAxis("Horizontal");
        _inputValueY =  Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(_inputValueX, _inputValueY);
        Move(dir);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsSprinting = false;
        }

        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpPower;
        }

        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) ||
                 Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        RightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        LeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        if (RightWall) wallLeftOrRight = -1;
        if (LeftWall) wallLeftOrRight = 1;

        if (Input.GetKeyDown("space") && onWall && !isGrounded()) wallJumping = true;

        if (wallJumping)
        {
            Invoke(nameof(JumpFalse),0.2f);
            rb.velocity = new Vector2(speed * wallLeftOrRight, jumpPower);
        }
        





    }

    void JumpFalse()
    {
        wallJumping = false;
    }

    void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpPower;
    }
    void Move(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
        speed = IsSprinting ? sprint : baseSpeed;
        
    }

    private bool isGrounded()
    {
      return  transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    
}
