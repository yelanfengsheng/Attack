using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_MoveState moveState { get; private set; }//移动状态
    public E3_IdleState idleState { get; private set; }//空闲状态
    public E3_PlayerDetectedState playerDetectedState { get; private set; }//玩家检测状态
    public E3_RangedAttackState rangedAttackState { get; private set; }//远程攻击状态s
    public E3_LookForPlayerState lookForPlayerState { get; private set; }//寻找玩家状态
    public E3_DeadState deadState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;//移动状态数据
    [SerializeField]
    private D_IdleState idleStateData;//空闲状态数据
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;//玩家检测状态数据
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;//远程攻击状态数据
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;//寻找玩家状态数据
    [SerializeField]
    private D_DeadState deadStateData;//死亡状态数据


    [SerializeField]
    private Transform rangedAttackPosition;//远程攻击位置



    public override void Start()
    {
        base.Start();
        moveState = new E3_MoveState(this, stateMachinel, "move", moveStateData, this);//创建移动状态实例
        idleState = new E3_IdleState(this, stateMachinel, "idle", idleStateData, this);//创建空闲状态实例
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachinel, "playerDetected", playerDetectedStateData, this);//创建玩家检测状态实例
        rangedAttackState = new E3_RangedAttackState(this, stateMachinel, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        lookForPlayerState = new E3_LookForPlayerState(this, stateMachinel, "lookForPlayer", lookForPlayerStateData, this);//创建寻找玩家状态实例
        deadState = new E3_DeadState(this, stateMachinel, "dead", deadStateData, this);//创建死亡状态实例


        stateMachinel.Initialize(moveState);//初始化状态机 设置初始状态为移动状态
    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachinel.ChangeState(deadState);//切换到死亡状态
        }
       
        else if (CheckPlayerInMinAggroRange())//如果受到伤害后玩家在最小攻击范围内
        {
            stateMachinel.ChangeState(rangedAttackState);//切换到玩家检测状态
        }
        else if (!CheckPlayerInMinAggroRange())//如果受到伤害后玩家不在最小攻击范围内 也就是在敌人的后面
        {
            lookForPlayerState.SetTurnImmediately(true);//设置立即转向
            stateMachinel.ChangeState(lookForPlayerState);//切换到寻找玩家状态
        }



    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
