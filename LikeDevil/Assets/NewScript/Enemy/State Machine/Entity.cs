using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public D_Entity entityData;//实体数据 不再需要手动赋值 通过脚本ableObject创建实例
    public FiniteStateMachine stateMachinel;
    public int facingDirection { get; private set; }//表示当前方向
    public Rigidbody2D rb { get;private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGo { get; private set; }
    public AnimToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }//最后一次受伤方向

    private Vector2 velocityWorkspace;//速度工作区 作用是为了减少GC回收
                                      //velocityWorkspace（“速度工作区”）是一个可复用的 Vector2 字段，用来临时存放计算出的速度并一次性赋值给 rb.velocity。
                                      //主要目的不是功能上的必须，而是为了减少临时对象/副本的创建、提高可读性和微小的性能优化。

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;



   

    private float currentHealth;//当前生命值
    private float currentStunResistance;//当前眩晕抗性    
    private float lastDamageTime;//最后受伤时间

    protected bool isStunned;//是否眩晕
    protected bool isDead;//是否死亡


    public virtual void Start()
    { 
        facingDirection = 1;//初始方向向右
        currentStunResistance = entityData.stunResistance;//初始化眩晕抗性

        aliveGo = transform.Find("Alive").gameObject;
        rb = aliveGo.GetComponent<Rigidbody2D>();
        anim = aliveGo.GetComponent<Animator>();
        stateMachinel = new FiniteStateMachine();//每个实体都将拥有自己的状态机
        atsm = aliveGo.GetComponent<AnimToStateMachine>();
        currentHealth = entityData.macHealth;//初始化当前生命值
    }
    public virtual void Update()
    {
        stateMachinel.currentState.LogicUpdate();//状态机逻辑更新
        anim.SetFloat("yVelocity", rb.velocity.y);//设置y轴速度参数  用于跳跃动画播放
        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)//如果距离最后受伤时间超过眩晕恢复时间  所有敌人自动执行重置眩晕抗性操作
        {
            ResetStunResistance();//重置眩晕抗性
        }

    }
    public virtual void FixedUpdate()
    {
        stateMachinel.currentState.PhysicsUpdate();//状态机物理更新
    }
    public virtual void SetVelocity(float velocity)//设置移动速度
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);//设置速度工作区
        rb.velocity = velocityWorkspace;//应用速度工作区
    }
    public virtual void SetVelocity(float velocity,Vector2 angle,int direction)//新的设置移动速度方法  主要用于击退效果
    {
        angle.Normalize();//归一化方向向量
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);//设置速度工作区
        rb.velocity = velocityWorkspace;//应用速度工作区
    }
    public virtual bool CheckWall()//检测墙壁
    {
        return Physics2D.Raycast(wallCheck.position, aliveGo.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()//检测边缘
    {
     
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);

    }
    public virtual bool CheckPlayerInMinAggroRange()//检测玩家是否在最小仇恨范围内
    {
        return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAggroRange()//检测玩家是否在最大仇恨范围内
    {
        return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    } 
    public virtual bool CheckPlayerInCloseRangeAction()//检测玩家是否在近战范围内
    {
       return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckGround()//检测地面
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }


   



    public virtual void DamageHop(float velocity)//受伤跳跃
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);//设置速度工作区
        rb.velocity = velocityWorkspace;//应用速度工作区
    }
    public virtual void ResetStunResistance()//重置眩晕抗性
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
      
    }
    public virtual void Damage(AttackDetails attackDetails)
    {

        lastDamageTime = Time.time;//记录最后受伤时间
        currentHealth -= attackDetails.damageAmount;//减少生命值
        currentStunResistance -= attackDetails.stunDamageAmount;//减少眩晕抗性

        DamageHop(entityData.damageHopSpeed);//受伤跳跃

        Instantiate(entityData.hitPartical, aliveGo.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
      

        if (attackDetails.position.x > aliveGo.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
        if (currentStunResistance <= 0)//如果眩晕抗性小于等于0
        {
            isStunned = true;//进入眩晕状态
        }
        if(currentHealth <= 0)
        {
            isDead = true;//进入死亡状态
        }
    }
    public virtual void Flip()//翻转实体方向
    {
        facingDirection *= -1;
        aliveGo.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public virtual  void OnDrawGizmos()//绘制检测射线
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position+(Vector3)(Vector2.right*facingDirection*entityData.wallCheckDistance));//墙壁检测射线
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position+(Vector3)(Vector2.down)*entityData.ledgeCheckDistance);//边缘检测射线

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);//近战范围检测射线
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);//最小仇恨范围检测射线
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);//最大仇恨范围检测射线

    }
}

#region  此FSM的代码结构说明
// Notes:
//•	State（基类）
//•	抽象所有“状态”的通用部分：保存 entity、stateMachine、animBoolName；提供 Enter/Exit/LogicUpdate/PhysicsUpdate 虚方法。
//•	Enter 会设置 startTime 并把对应动画布尔位设为 true，Exit 则设为 false。
//•	MoveState（中间层，继承自 State）
//•	针对移动行为扩展：持有 D_MoveState（配置数据）、检测标志（isDetectingWall/isDetectingLedge/isPlayerInMinAgroRange 等）。
//•	在 Enter 中根据 stateData 设置初始速度；在 PhysicsUpdate 中刷新射线检测结果（调用 Entity 的 CheckXxx 方法），把检测结果保存为字段供 LogicUpdate 使用。
//•	E1_MoveState（具体实现，继承自 MoveState）
//•	敌人 Enemy1 专用的移动逻辑：在 LogicUpdate 中使用从 MoveState 继承的检测字段（如 isDetectingWall、isPlayerInMinAgroRange）做状态切换（切换到 idleState、playerDetectedState 等），并可访问 enemy 特有成员（比如保存的其他状态实例）。
#endregion