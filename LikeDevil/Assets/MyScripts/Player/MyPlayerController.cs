using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MyPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;    
    public float jumpForce = 5f;
   
    private Animator animator;
    private bool isJump = false;
    [SerializeField]
    private bool isGrounded = false;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public int jumpCount = 0;
    private GameObject myFeet;
    private BoxCollider2D myBoxFeet;
    private bool isOneWayPlatform = false;
    public float fallTime = 0.5f;

    [Header("残影设置")]
    public float dashSpeed;
    public float dashTime;
    float startDashTimer;
    bool isDashing;
    public GameObject dashObj;

    [Header("重力系数")]
    [SerializeField]
    private float jumpGravity = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        animator = GetComponent<Animator>();
        myFeet = transform.Find("Foot").gameObject;
        myBoxFeet = myFeet.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool wantToJump = Input.GetKeyDown(KeyCode.Space);
        bool wantToPassThrough = Input.GetKey(KeyCode.S) && Input.GetButton("Jump");
        

        if (wantToJump)//正常可以跳跃 不考虑单向平台，如果站在平台上 也可以跳跃
        {
            isJump = true;
            Jump();
        }
        
       
        if (wantToPassThrough&&isOneWayPlatform)//玩家站在单向平台上 并且按下S键和跳跃键 
        {
            
            CapsuleCollider2D playerCollider = this.GetComponent<CapsuleCollider2D>();
            if (playerCollider != null)
            {
                playerCollider.isTrigger = true;
                StartCoroutine(ResetPlayerCollider(playerCollider));
                print("允许穿过平台");
            }
        }

       
       
       if(Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

       //冲刺设计
       if(!isDashing)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                //开始dash
                Debug.Log("dash将要开始");
                dashObj.SetActive(true);
                Debug.Log("dash开始");
                isDashing = true;
                startDashTimer = dashTime;

            }
        }
       else
        {
            startDashTimer -= Time.deltaTime;
            if(startDashTimer < 0)
            {
                isDashing = false;
                dashObj.SetActive(false);
            }
            else
            {
                rb.velocity = transform.right * dashSpeed;
            }
        }
       //
       JumpGravity();
    }

  

    void FixedUpdate()
    {
        isGrounded =myBoxFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))||
                    myBoxFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));

        isOneWayPlatform = myBoxFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));

        Move();
        SwitchAnimition();
    }
    public void Move()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInputX *  speed, rb.velocity.y);
        if(moveInputX != 0)
        {
            animator.SetBool("isRun", true);
            transform.localScale = new Vector3(moveInputX, 1, 1);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
    private void Jump()
    {
        if(isGrounded)
        {
          jumpCount = 2;
        }
       if(isJump&&isGrounded)
       {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//使用刚体的速度
            jumpCount--;
            isJump = false;
       }
       //按下跳跃键在空中不在地面上进行第二次跳跃
       else if(isJump&&jumpCount>0&&!isGrounded)
       {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            isJump = false;
        }
    }
    private void Attack()
    {
        animator.SetTrigger("Atk");
    }
     //重力调整
     public void JumpGravity()
     {
       if(rb.velocity.y < 0)
       {
         rb.velocity +=Vector2.up*Physics2D.gravity.y*jumpGravity*Time.deltaTime;//重力每秒来进行增加
       }
       else if(rb.velocity.y > 0&&!Input.GetKey(KeyCode.Space)) //玩家实现短跳跃的效果
       {
           rb.velocity+= Vector2.up * Physics2D.gravity.y * jumpGravity * Time.deltaTime;//重力每秒来进行增加
        }
     }

    public void SwitchAnimition()
    {
        //当处于下落状态时 并不在地面上
       if( rb.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("isFall", true);
            animator.SetBool("isJump", false);
        }
        //如果是跳跃状态 并且不在地面上
        if (rb.velocity.y>0&&!isGrounded)
        {
            animator.SetBool("isJump", true);
        }
        //如果在地面上
        else if (myBoxFeet.IsTouchingLayers(groundLayer))
        {
            animator.SetBool("isFall", false);
        }

    }
       IEnumerator ResetPlayerCollider(CapsuleCollider2D playerCollider)
    {
        yield return new WaitForSeconds(fallTime);
        if (playerCollider != null)
        {
            playerCollider.isTrigger = false;
            print("恢复碰撞");
        }
    }
    //public void  OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.CompareTag("Enemy"))
    //     {
    //         Camera.main.GetComponent<CamreaShark>().PlayCameraShake();
    //         print("玩家受伤");  
    //     }

    // }
}
