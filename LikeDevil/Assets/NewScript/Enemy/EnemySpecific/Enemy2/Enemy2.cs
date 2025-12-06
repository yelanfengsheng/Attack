using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }//移动状态
    public E2_IdleState idleState { get; private set; } //空闲状态
    public E2_PlayerDetectedState playerDetectedState { get; private set; }//玩家检测状态
    public E2_MeleeAttackState meleeAttackState { get; private set; }//近战攻击状态
    public E2_LookForPlayerState lookForPlayerState { get; private set; }//寻找玩家状态
    public E2_StunState stunState { get; private set; }//眩晕状态
    public E2_DeadState deadState { get; private set; }//死亡状态
    public E2_DodgeState dodgeState { get; private set; }//闪避状态
    public E2_RangedAttackState rangedAttackState { get; private set; }//远程攻击状态

    [SerializeField]
    private D_MoveState moveStateData;//移动状态数据
    [SerializeField]
    private D_IdleState idleStateData;//空闲状态数据
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;//玩家检测状态数据
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;//近战攻击状态数据

    [SerializeField]
    private Transform meleeAttackPosition;//近战攻击位置
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;//寻找玩家状态数据
    [SerializeField]
    private D_StunState stunStateData;//眩晕状态数据
    [SerializeField]
    private D_DeadState deadStateData;//死亡状态数据
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;//远程攻击状态数据



    [SerializeField]
    private Transform rangedAttackPosition;

    public D_DodgeState dodgeStateData;//闪避状态数据
    public override void Start()
    {
        base.Start();
        moveState = new E2_MoveState(this, stateMachinel, "move", moveStateData, this);//创建移动状态实例
        idleState = new E2_IdleState(this, stateMachinel, "idle", idleStateData, this);//创建空闲状态实例
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachinel, "playerDetected", playerDetectedStateData, this);//创建玩家检测状态实例
        meleeAttackState = new E2_MeleeAttackState(this, stateMachinel, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);//创建近战攻击状态实例
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachinel, "lookForPlayer", lookForPlayerStateData, this);//创建寻找玩家状态实例
        stunState = new E2_StunState(this, stateMachinel, "stun", stunStateData, this);//创建眩晕状态实例
        deadState = new E2_DeadState(this, stateMachinel, "dead", deadStateData, this);//创建死亡状态实例
        dodgeState = new E2_DodgeState(this, stateMachinel, "dodge", dodgeStateData, this);//创建闪避状态实例
        rangedAttackState = new E2_RangedAttackState(this, stateMachinel, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);//创建远程攻击状态实例

        stateMachinel.Initialize(moveState);//初始化状态机 设置初始状态为移动状态
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if(isDead)
        {
            stateMachinel.ChangeState(deadState);//切换到死亡状态
        }
        else if(isStunned&&stateMachinel.currentState!=stunState)//如果眩晕且当前状态不是眩晕状态
        {
            stateMachinel.ChangeState(stunState);//切换到眩晕状态
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
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
