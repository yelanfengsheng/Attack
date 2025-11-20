using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour  
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private float attack1HitBoxRadius;
    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    private float attack1Damage;
    private bool gotInput;//是否获取了输入
    private bool isAttacking;
    private bool isFirstAttack;

    private float lastInputTime;//上次尝试攻击的时间

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }
    private void Update()
    {
        CheckCombatInput();//检查战斗输入
        CheckAttacks();//检查攻击
    }
    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(combatEnabled)//战斗开启
            {
                gotInput = true;//获取输入
                lastInputTime = Time.time;//记录输入时间
            }
        }
    }
    private void CheckAttacks()//检查攻击
    {
        if(gotInput)//获取了输入
        {
            //攻击逻辑
            if(!isAttacking)//没有攻击
            { 
                gotInput = false;//释放输入
                isAttacking = true;//开始攻击
                isFirstAttack = !isFirstAttack;//两个攻击动画进行切换
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }
        if(Time.time>=lastInputTime+inputTimer)//可以再攻击
        {
            gotInput=false;//释放输入
        }
    }
    private void CheckAttackHitBox()//检测范围内左右的可攻击的box并对其造成伤害 用于打击的时候被调用
    {
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attack1HitBoxRadius,whatIsDamageable);

        foreach (Collider2D collider in detectedColliders)
        {
            collider.transform.parent.SendMessage("Damage", attack1Damage);//发送伤害
        }
    }
    private void FinishAttack1()//攻击1结束
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attack1HitBoxPos.position,attack1HitBoxRadius);
    }
}
