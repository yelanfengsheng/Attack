using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
       Moving,
       Knockback,
       Dead
    }
    private State currentState;//记录当前状态

    [SerializeField]
    private float groundCheckDistance = 0.1f;//地面检测距离
    [SerializeField]
    private float wallCheckDistance = 0.1f;//墙壁检测距离
    [SerializeField]
    private float walkSpeed = 2.0f;//行走速度
    [SerializeField]
    private float maxHealth = 100.0f;//最大生命值
    [SerializeField]
    private float knockbackDuration = 0.5f;//击退持续时间
    [SerializeField]
    private Vector2 knockbackSpeed;//击退速度

    
    
    [SerializeField]
    private float touchDamageCooldown = 1.0f;//接触伤害冷却时间
    [SerializeField]
    private float touchDamageAmount = 10.0f;//接触伤害数值
    [SerializeField]
    private float  touchDamageHeight = 1.0f,
                   touchDamageWeigth = 1.0f;//接触伤害高度,宽度


    [SerializeField]
    private Transform groundCheck,
                      touchDamageCheck;//地面检测点
    [SerializeField]
    private Transform wallCheck;//墙壁检测点
    [SerializeField]
    private LayerMask whatIsGround,
                      whatIsPlayer;//地面图层,玩家图层

    [SerializeField]
    private GameObject hitPartical;//受击粒子效果
    [SerializeField]
    private GameObject deathChunkPartical;//死亡碎片粒子效果
    [SerializeField]
    private GameObject deadBloodPartical;//死亡血液粒子效果

    private float lastTouchDamageTime;//上次接触伤害的时间
    private bool groundDetected;//是否检测到地面
    private bool wallDetected;//是否检测到墙壁
    private int facingDirection ;//敌人面朝方向，1为右，-1为左
    private int damageDirection;//伤害来源方向，1为右，-1为左
    private float currentHealth;//当前生命值
    private float knockbackStartTime;//击退持续时间
    private float[] attachDetails = new float[2];//攻击细节 数组0为伤害 数组1为攻击来源X坐标 用来作为sendmessage的参数传递给玩家

    private Vector2 movement,
                    touchDamageBotLeft,
                    touchDamageTopRight;//敌人移动向量,接触伤害检测区域左下角,接触伤害检测区域右上角

    private GameObject aliveGameObject;//存活时的游戏对象
    private Rigidbody2D rbAlive;//刚体组件
    private Animator anim;//动画组件


    private void Start()
    {
       
        aliveGameObject = transform.Find("Alive").gameObject;//获取存活时的游戏对象
        rbAlive = aliveGameObject.GetComponent<Rigidbody2D>();//获取刚体组件
        anim = aliveGameObject.GetComponent<Animator>();//获取动画组件
        facingDirection = 1;//初始面朝右侧
        currentHealth = maxHealth;//初始化当前生命值
    }



    private void Update()
    {
        switch (currentState)//根据当前状态调用不同的处理函数
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }
    #region 行走状态处理
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        //检测地面和墙壁
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        //检查接触伤害
        CheckTouchDamage();

        if (!groundDetected||wallDetected)
        {
            //翻转敌人方向
            Flip();
        }
        else
        {
            //行走逻辑
            movement.Set(walkSpeed * facingDirection, rbAlive.velocity.y);//设置移动向量
            rbAlive.velocity = movement;//应用移动向量
        }
    }

    private void ExitMovingState()
    {

    }
    #endregion

    #region 击退状态处理
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;//记录击退开始时间
        //应用击退力
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);//设置击退向量
        rbAlive.velocity = movement;
        anim.SetBool("knockback", true);//播放击退动画
    }
    private void UpdateKnockbackState()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration)//检查击退持续时间是否结束
        {
            ChangeState(State.Moving);//切换回行走状态
        }
    }

    private void ExitKnockbackState()
    {
        anim.SetBool("knockback", false);//播放击退动画
    }
    #endregion

    #region 死亡状态处理
    private void EnterDeadState()
    {
        //生成死亡粒子效果
        Instantiate(deathChunkPartical, aliveGameObject.transform.position, deathChunkPartical.transform.rotation);//生成死亡碎片粒子效果
        Instantiate(deadBloodPartical, aliveGameObject.transform.position, deadBloodPartical.transform.rotation);//生成死亡血液粒子效果
        //销毁敌人游戏对象
        Destroy(this.gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }
    #endregion


    //-----------其他功能函数------------------------------------------------
    private void CheckTouchDamage()
    {
        if(Time.time>touchDamageCooldown+lastTouchDamageTime)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWeigth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));//计算接触伤害检测区域左下角坐标
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWeigth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));//计算接触伤害检测区域右上角坐标
            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);//检测玩家是否在接触伤害区域内
            if(hit != null)
            {
                lastTouchDamageTime = Time.time;//更新上次接触伤害时间
                attachDetails[0] = touchDamageAmount;//设置攻击细节数组的伤害值
                attachDetails[1] = aliveGameObject.transform.position.x;//设置攻击细节数组的攻击来源X坐标 将信息传递给玩家
                hit.SendMessage("Damage", attachDetails);//发送受伤消息给玩家
            }
        }
    }

    public void Damage(AttackDetails attackDetails) //受伤函数 使用攻击细节数组作为参数 能够传递多种信息包括伤害值和攻击方向
    {
        currentHealth -= attackDetails.damageAmount;//减少生命值 将攻击细节数组的第一个元素作为伤害值
        //生成受击粒子效果
        Instantiate(hitPartical, aliveGameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f,360.0f)));//在敌人位置生成受击粒子效果 并随机旋转粒子效果

        if (attackDetails.position.x > aliveGameObject.transform.position.x)//判断伤害来源方向
        {
            damageDirection = -1;//伤害来源在敌人右侧，敌人受到向左的攻击
        }
        else
        {
            damageDirection = 1;//伤害来源在敌人左侧，敌人受到向左的攻击
        }
        //粒子效果

        //检查是否死亡
        if(currentHealth>0)
        {
            ChangeState(State.Knockback);//切换到击退状态
        }
        else
        {
            ChangeState(State.Dead);//切换到死亡状态
        }
    }

    private void Flip()//翻转敌人方向函数
    {
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);//绕Y轴旋转180度
    }


    private void ChangeState(State newState)//状态切换函数
    {
        //退出当前状态
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        //进入想要进入的新状态
        switch (newState)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
        currentState = newState;//更新当前状态
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWeigth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWeigth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2)); 
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x + (touchDamageWeigth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2)) ;
        Vector2 topRight = new Vector2(touchDamageCheck.position.x -  (touchDamageWeigth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(botLeft, botRight);//绘制接触伤害检测区域
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(botLeft, topRight);
        Gizmos.DrawLine(botRight, topLeft);
    }
}

