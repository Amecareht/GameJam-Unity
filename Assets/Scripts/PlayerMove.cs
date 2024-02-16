using System;
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
    public Vector2 rightOffset, leftOffset, topOffset;
    public float collisionRadius = 0.25f;
    [SerializeField] private bool onWall, Onwalls;
    [SerializeField] private bool RightWall,LeftWall,TopWall;
    [SerializeField] private int wallLeftOrRight;
    private bool wallJumping;
    public float BumpPower;
    public float slideSpeed;
    public bool isMoving;
    public bool onPlateform;
    private Vector2 relativeTransfom;
    public Rigidbody2D platformRb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
     
    

    private Vector3 direction;

    // Player position variables
    private float _posX;

    private Rigidbody2D rb;
    [SerializeField] private bool IsSprinting;


    public Transform GroundCheck;
    public LayerMask Glued;
    public LayerMask groundLayer;
    [SerializeField] private int MaxJumps;
    [SerializeField] private int JumpLeft;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpLeft = MaxJumps;
        sprint = speed * 1.4f;
        slideSpeed = 9f;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        _inputValueX = Input.GetAxis("Horizontal");
        _inputValueY =  Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(_inputValueX, _inputValueY);
      
        if (_inputValueY != 0 || _inputValueX != 0)
        {
            Move(dir);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

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

            if (rb.velocity.x != 0)

                   isMoving = true;
        }

        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, Glued) ||
                 Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, Glued);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) ||
                 Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        RightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, Glued);
        LeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, Glued);
        TopWall = Physics2D.OverlapCircle((Vector2)transform.position + topOffset, collisionRadius, Glued);
        

        if (RightWall) wallLeftOrRight = -1;
        if (LeftWall) wallLeftOrRight = 1;

        if (Input.GetKeyDown("space") && onWall && !isGrounded()) wallJumping = true;

        if (wallJumping)
        {
            Invoke(nameof(JumpFalse),0.2f);
            rb.velocity = new Vector2(speed * wallLeftOrRight, jumpPower);
        }

        if (RightWall || LeftWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, dir.y * speed);
        }

        if (TopWall)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }

        if (isBumbed())
        {
            rb.velocity = Vector2.up * BumpPower;
        }
        Debug.Log(isBumbed());
        if ((onWall || Onwalls ) && !isGrounded())
        {
            WallSlide();
        }
        float targetSpeed = speed * relativeTransfom.x;

        if (onPlateform)
        {
          
            rb.velocity = new Vector2(dir.x * speed + platformRb.velocity.x , rb.velocity.y);
            Debug.Log(targetSpeed);
            
        }

    }

    void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        Debug.Log("here");
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
        _animator.SetBool("IsWalking" ,true);
        if (_inputValueX < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        
    }

    private bool isGrounded()
    {
      return  transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    private bool isBumbed()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().Bump;
    }

   
}
