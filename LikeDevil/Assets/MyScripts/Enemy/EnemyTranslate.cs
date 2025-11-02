using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTranslate : MonoBehaviour
{
    public float speed = 3f;
    private float maxSpeed=>speed + 1f;
    private float currentSpeed;
    private Rigidbody2D rb;
    public float rayTranslateLength = 1f;
    public float rayAttackLength = 5f;
    public bool isRayRange;
    private Vector3 front = new Vector3(-1f, 0f, 0f);
    private float x = 0.5f;
    WormControl worm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        worm = GetComponent<WormControl>();
    }

    private void FixedUpdate()
    {
      
        AttackRay();
        ControlSpeed();



    }
    // Update is called once per frame
    void Update()
    {
        TranslateRay();
    }
    // 计划（伪代码）：
    // 1. 根据 transform.localScale.x 的符号确定朝向（front）：如果 localScale.x > 0 则朝向右(Vector3.right)，否则朝向左(Vector3.left)。
    // 2. 使用 front 和固定的横向偏移 x 来计算射线起点：startRay = position + new Vector3(front.x * x, -0.1f, 0)。
    //    这样起点会自动根据朝向切换到角色前方，无需单独翻转 x 的符号。
    // 3. 计算向下前方的归一化方向 dir = (front + down).normalized，并用 Debug.DrawLine 可视化。
    // 4. 用 Physics2D.Raycast 检测地面（传入 Vector2），如果没有地面则翻转角色 localScale.x（乘 -1）。
    //    翻转时不再依赖翻转 x 的符号，只保证 x 为正值以维持偏移大小一致。
    // 5. 最后用 rb.velocity 为刚体设置水平速度，方向由 front.x 决定。
    // 注意：所有 2D 射线调用使用 Vector2 参数以避免类型不匹配问题（从 Vector3 明确转换为 Vector2）。

    private void TranslateRay()
    {
        // 1. 根据 localScale 确定朝向
        front = transform.localScale.x > 0f ? Vector3.right : Vector3.left;

        // 2. 计算射线起点（使用 front.x 保证为角色面向一侧）
        Vector3 startRay = transform.position + new Vector3(front.x * Mathf.Abs(x), -0.1f, 0f);

        // 3. 向下前方的方向并可视化
        Vector3 down = Vector3.down;
        Vector3 dir3 = (front + down).normalized;
        Debug.DrawLine(startRay, startRay + dir3 * rayTranslateLength, Color.red);

        // 4. 用 Physics2D.Raycast 检测地面（转换为 Vector2）
        if (!Physics2D.Raycast((Vector2)startRay, (Vector2)dir3, rayTranslateLength, LayerMask.GetMask("Ground")))
        {
            // 翻转角色
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;

            // 保证偏移量为正（起点计算使用 front.x * Mathf.Abs(x)）
            x = Mathf.Abs(x);

            print("需要转弯");
        }

        // 5. 设置水平速度
        rb.velocity = new Vector2(front.x * speed, rb.velocity.y);
    }
    private void AttackRay()
    {
       
        Vector3 startRay = transform.position; //开始发射点
        Debug.DrawLine(startRay, startRay + front * rayAttackLength, Color.blue);
       if( Physics2D.Raycast(startRay,front,rayAttackLength,LayerMask.GetMask("Player")))
        {
            print("射线检测到玩家");
            isRayRange = true;
            worm.WormAttack();
         
            
        }
       else
        {
          isRayRange= false;
        }
          

    }
    private void ControlSpeed()
    {
        if (isRayRange)
        {
            // 逐渐加速到 maxSpeed
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, 0.1f * Time.fixedDeltaTime * 50f); // 调整加速度
        }
        else
        {
            // 恢复基础速度
            currentSpeed = Mathf.MoveTowards(currentSpeed, speed, 0.1f * Time.fixedDeltaTime * 50f);
        }

    }
}
