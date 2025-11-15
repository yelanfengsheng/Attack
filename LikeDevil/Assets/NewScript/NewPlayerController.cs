using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private float movementInputDirection;
    private int amountOfJumpsLeft; // 记录剩余的跳跃次数

    private bool isFaceingRight = true;
    private bool isWalking = false;
    private bool isGrounded=false;
    private bool isTouchingWall=false;
    private bool isWallSliding=false;
    private bool canJump=false;


    public float movementSpeed = 10f;
    public float jumpForce = 10f;
    public int amountOfJumps = 1;
    public float cicleRadius = 0.5f;
    public float wallCheckDistance = 0.5f;
    public float wallSlideSpeed = 2f;
    

    public LayerMask whatIsGround;
    public Transform wallCheck;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckInputDirection();
        UpdateAnimtions();
        CheckIfCanJump();
        CheckIfWallSliding();
      
    }

  

    void FixedUpdate()
    { 
         ApplyMovement();
        CheckSurroundings();
    }
    private void UpdateAnimtions()//更新动画
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetFloat("Blend", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding); 

    }
    private void CheckIfWallSliding()
    {
       if(isTouchingWall&&!isGrounded&&rb.velocity.y<0f)
        {
            isWallSliding = true;
        }
       else
        {
            isWallSliding = false;
        }
    }
    private void CheckInputDirection() //检测移动方向 
    {
        if(isFaceingRight&&movementInputDirection < 0f)
        {
            Flip();
        }
        else if(!isFaceingRight&&movementInputDirection > 0f)
        {
            Flip();
        }
        if(rb.velocity.x != 0f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        
    }
   public void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, cicleRadius, whatIsGround);
        isTouchingWall =Physics2D.Raycast(wallCheck.position,transform.right,wallCheckDistance,whatIsGround);
    }
    private void CheckIfCanJump()
    {
       if(isGrounded && rb.velocity.y<=0.1f)
        {
          amountOfJumpsLeft = amountOfJumps;
        }
       if(amountOfJumpsLeft<=0)
        {
            canJump = false;
        }
       else
        {
            canJump = true;
        }
               
    }


    private void CheckInput() //检测输入
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
   

    private void ApplyMovement()//应用移动
    {
        rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
        if(isWallSliding)
        {
            if(rb.velocity.y<-wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x,-wallSlideSpeed);
            }
        }
    }
    private void Flip()//翻转
    {
        isFaceingRight = !isFaceingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void Jump()//跳跃
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft-- ;
        }
       
    }
   private void OnDrawGizmos()
{
    // 绘制检测地面的圆形区域
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(groundCheck.position, cicleRadius);
   // 绘制检测墙的射线
    Gizmos.color = Color.red;
    Gizmos.DrawRay(wallCheck.position, wallCheck.right * wallCheckDistance);
}
}
