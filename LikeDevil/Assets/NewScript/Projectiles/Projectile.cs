using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;//子弹飞行距离
    private float xStartPos;//子弹发射位置

    [SerializeField]
    private float gravity;//重力影响力度
    [SerializeField]
    private float damageRadius;//伤害范围

    private bool isGravityOn;
    private bool hasHitGround;

    private Rigidbody2D rb;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;//取消重力影响
        rb.velocity = transform.right * speed;//子弹沿着物体的右方向发射

        xStartPos = transform.position.x;//记录子弹发射位置
        isGravityOn = false; //初始状态下重力影响关闭
    }
    private void Update()
    {
        if(!hasHitGround)
        {
            attackDetails.position = transform.position;//更新攻击细节中的位置为子弹当前位置
            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;//使用三角函数来计算子弹的旋转角度 通过反正切函数计算y轴和x轴的夹角 同时将弧度转换为角度
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // 设定旋转角度
            }
        }
    }
    private void FixedUpdate()
    {
        if(!hasHitGround)//如果子弹没有击中地面
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);//检测子弹是否击中玩家
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);//检测子弹是否击中地面
            //子弹射击到玩家
            if(damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }
            if(groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;

            }

            if (Mathf.Abs(xStartPos - transform.position.x) > travelDistance && !isGravityOn)//如果子弹飞行距离超过设定值且重力影响未开启
            {
                isGravityOn = true;//开启重力影响
                rb.gravityScale = gravity;//应用重力影响
            }

        }
       
    }
    public void FireProjectile(float speed,float travelDistance,float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
