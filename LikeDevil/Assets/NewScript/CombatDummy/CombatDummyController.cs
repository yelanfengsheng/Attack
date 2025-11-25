using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth,knockbackSpeedX, knockbackSpeedY, knockbackDuration;// 最大生命值 击退速度X 击退速度Y（应用在刚体上的速度）,击退持续时间
    [SerializeField]
    private float knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque; // 击退死亡速度X 击退死亡速度Y（应用在刚体上的速度）,击退死亡旋转 在死亡时候是这种新的效果
    [SerializeField]
    private bool applyKnockback;// 是否应用击退
    [SerializeField]
    private GameObject hitPartical;

    private float currentHealth;
    private float knockbackStart;// 击退开始时间

    private int playerFacingDirection;
    private bool playerOnleft;
    private bool knockback;

    private NewPlayerController playerController;
    private GameObject aliveGo,brokenTopGo,brokenBottomGo;
    private Rigidbody2D rbAlive,rbBrokenTop,rbBrokenBottom;
    private Animator aliveAnim;
    private void Start()
    {
        currentHealth = maxHealth;

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerController>();
        if (playerController == null) { Debug.Log("mei找到玩家控制器");}
        aliveGo = transform.Find("Alive").gameObject;
        brokenTopGo = transform.Find("BrokenTop").gameObject;
        brokenBottomGo = transform.Find("BrokenBottom").gameObject;

        aliveAnim =aliveGo.GetComponent<Animator>();
        rbAlive = aliveGo.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGo.GetComponent<Rigidbody2D>();
        rbBrokenBottom = brokenBottomGo.GetComponent<Rigidbody2D>();

        aliveGo.SetActive(true);
        brokenTopGo.SetActive(false);
        brokenBottomGo.SetActive(false); // 初始状态 破碎状态不可见
    }
    private void Update()
    {
        CheckKnockback();// 检查击退
    }
    private void Damage(float[] details)// 假人接受伤害
    {
        currentHealth -= details[0];
        if (details[1]<aliveGo.transform.position.x)// 攻击来源在假人左边
        {
            playerFacingDirection = 1;
        }
        else// 攻击来源在假人右边
        {
            playerFacingDirection = -1;
        }

        Instantiate(hitPartical, aliveAnim.transform.position, Quaternion.Euler(0f,0f,Random.Range(0f,360f)));
        if (playerFacingDirection == 1)// 玩家朝向为1时，玩家在左边s
        {
            playerOnleft = true;
        }
        else// 玩家朝向为-1时，玩家在右边 更改动画的播放方向
        {
            playerOnleft = false;
        }
        aliveAnim.SetBool("playerOnleft", playerOnleft);// 设置动画的播放方向
        aliveAnim.SetTrigger("damage");// 触发动画

        if (applyKnockback && currentHealth > 0.0f)// 如果应用击退且假人生命值小于等于0
        {
            // 应用击退
            Knockback();
        }
        if (currentHealth <= 0.0f)
        {
            // 假人死亡
            Die();
        }
    }
    private void Knockback()// 击退
    {
        knockback = true;
        knockbackStart = Time.time;// 记录击退开始时间
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }
    private void CheckKnockback()// 检查击退
    { 
        if(Time.time>knockbackStart+knockbackDuration&&knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }
    private void Die()
    {
        //启用破碎状态 禁用存活状态
        aliveGo.SetActive(false);
        brokenTopGo.SetActive(true);
        brokenBottomGo.SetActive(true);

        //确保死亡时候的最开始位置对齐
        brokenTopGo.transform.position = aliveGo.transform.position;
        brokenBottomGo.transform.position = aliveGo.transform.position;

        //应用击退死亡效果  两个不同的击退效果
        //击退速度X 击退速度Y（应用在刚体上的速度）
        rbBrokenBottom.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        //击退死亡速度Y（应用在刚体上的速度）
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        //顶部碎片在死亡时候旋转+
        rbBrokenTop.AddTorque(deathTorque*-playerFacingDirection,ForceMode2D.Impulse);
    }

}
