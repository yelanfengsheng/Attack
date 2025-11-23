using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private float movementInputDirection;
    private float turnTimer;


    private int amountOfJumpsLeft; // 记录剩余的跳跃次数
    private int facingDirection = 1;//记录玩家当前的面向方向
    private float jumpTimer;
    private float dashTimeLeft;//技能持续时间
    private float lastImageXpos;//记录上一次创建的图片的X坐标
    private float lastDashTime = -100;//记录最后一次的冲刺时刻 并用于检测是否可以进行冲刺的冷却时间

    private bool isFaceingRight = true;
    private bool isWalking = false;
    private bool isGrounded = false;
    private bool isTouchingWall = false;
    private bool isWallSliding = false;
    private bool canNormalJump = false;
    private bool canWallJump = false;
    private bool isAttemptingToJump = false;
    private bool isDash;
    //private bool isTouchingLedge;
    private bool checkJumpMultiplier;//检测跳跃系数
    private bool canMove;
    private bool canFlip;



    public float movementSpeed = 10f;
    public float jumpForce = 10f;
    public float movementForceInAir = 10f;
    public float ariDragFactor = 0.95f;
    public float jumpHeightFactor = 1f;
    public int amountOfJumps = 1;
    public float cicleRadius = 0.5f;
    public float wallCheckDistance = 0.5f;
    public float wallSlideSpeed = 2f;
    public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;



    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown; //技能冷却时间
    public Transform ledgeCheck;



    public Vector2 wallHopDirection; //定义角色进行垂直蹬墙跳（Wall Hop）时的跳跃方向
    public Vector2 wallJumpDirection;//定义角色进行斜向蹬墙跳（Wall Jump）时的跳跃方向

    public LayerMask whatIsGround;


    public Transform wallCheck;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();//单位向量
        wallJumpDirection.Normalize();//单位向量 确保方向准确而不会因向量长度影响力度。

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckInputDirection();
        UpdateAnimtions();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckDash();
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
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0f)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void CheckInputDirection()
    {
        // 冲刺期间禁止翻转和行走输入
        if (isDash)
            return;

        if (isFaceingRight && movementInputDirection < 0f)
        {
            Flip();
        }
        else if (!isFaceingRight && movementInputDirection > 0f)
        {
            Flip();
        }

        isWalking = Mathf.Abs(movementInputDirection) > 0.01f;
    }
    public void CheckSurroundings()//检查周围地形
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, cicleRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }
    private void CheckIfCanJump()//检测是否可以跳跃
    {
        if ((isGrounded && rb.velocity.y <= 0.01f))
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (isTouchingWall)
        {
            canWallJump = true;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }

    }


    private void CheckInput() //检测输入
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))//在地面或在墙上但是还有跳跃次数时，可以跳跃
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;//设置跳起的时间
                isAttemptingToJump = true;//尝试跳跃
            }
        }
        if (Input.GetButtonDown("Horizontal") && isTouchingWall) //触碰墙壁也有跳跃
        {
            if (!isGrounded && movementInputDirection != facingDirection)//没有在地面时同时面向方向和输入方向不一致时候
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }
        if (!canMove)//移动被禁止时 使用计数器来将移动和反转重新可以使用
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }
        if (checkJumpMultiplier && !Input.GetButton("Jump"))//没有按下跳跃键时但是跳跃系数为真时，将跳跃高度乘以跳跃系数
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeightFactor);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {

            if (Time.time > (lastDashTime + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }
    private void CheckDash()
    {
        if (isDash)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;

                rb.velocity = new Vector2(facingDirection * dashSpeed, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime; // <-- 修复：每帧减少经过时间

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }

            }
            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDash = false;
                canMove = true;
                canFlip = true;


            }

        }
    }
    private void AttemptToDash()
    {
        isDash = true;
        dashTimeLeft = dashTime;
        lastDashTime = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
        // 不再设置 canMove = false
    }
   

    private void ApplyMovement()
    {
        // 如果正在冲刺，不处理任何输入移动（但保留垂直物理）
        if (isDash)
        {
            // 注意：这里不要修改 rb.velocity！
            // 因为 CheckDash() 会负责设置冲刺速度
            // 你只需要确保其他逻辑（如空气阻力）不干扰它
            return;
        }

        // 非冲刺时才处理正常移动
        if (!isGrounded && !isWallSliding && movementInputDirection == 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x * ariDragFactor, rb.velocity.y);
        }
        else if (canMove) // 这里 canMove 可以保留用于攻击等其他状态
        {
            rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
        }

        // 墙滑逻辑（可保留）
        if (isWallSliding && rb.velocity.y < -wallSlideSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }
    private void Flip()//翻转
    {
        if (!isWallSliding && canFlip)//不是墙滑动 翻转
        {
            facingDirection = -facingDirection;
            isFaceingRight = !isFaceingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }
    private void CheckJump()//检测跳跃
    {

        if (jumpTimer > 0f)
        {
            //如果是想进行墙跳
            if (!isGrounded && isTouchingWall && movementInputDirection != 0f && movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }
        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }


    }
    private void NormalJump()
    {
        // 普通跳跃：当可以跳跃且没有在墙上滑行时执行
        if (canNormalJump)
        {
            // 设置垂直速度实现跳跃
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // 减少可用跳跃次数
            amountOfJumpsLeft--;
            // 重置跳跃计时器
            jumpTimer = 0f;
            // 停止尝试跳跃
            isAttemptingToJump = false;
            checkJumpMultiplier = true;//检测跳跃乘数
        }
    }
    private void WallJump()
    {
        // 斜向蹬墙跳：当接触墙壁(滑墙或触摸墙)且有水平输入时执行
        if (canWallJump)
        {

            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            // 结束滑墙状态
            isWallSliding = false;
            // 重置跳跃次数
            amountOfJumpsLeft = amountOfJumps;
            // 减少可用跳跃次数
            amountOfJumpsLeft--;
            // 计算蹬墙跳的力：使用wallJumpForce和wallJumpDirection，并考虑输入方向
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection,
                                            wallJumpForce * wallJumpDirection.y);
            // 应用瞬时力实现斜向蹬墙跳
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            // 重置跳跃计时器
            jumpTimer = 0f;
            // 停止尝试跳跃
            isAttemptingToJump = false;
            checkJumpMultiplier = true;//检测跳跃乘数
            turnTimer = 0;//转向计数器归零
        }
    }
    private void DisableFlip()
    {
        canFlip = false;
    }
    private void EnableFlip()
    {
        canFlip = true;
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
    public int GetFacingDirection()
    {
        return facingDirection;
    }
}