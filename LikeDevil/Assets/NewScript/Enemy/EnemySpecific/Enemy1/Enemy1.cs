using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }//玩家检测状态
    public E1_ChargeState chargeState { get; private set; }//冲锋状态
    public E1_LookForPlayerState lookForPlayerState { get; private set; }//寻找玩家状态 
    public E1_MeleeAttackState meleeAttackState { get; private set; }//近战攻击状态

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;//玩家检测状态数据
    [SerializeField]
    private D_ChargeState chargeStateData;//冲锋状态数据
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;//寻找玩家状态数据
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;//近战攻击状态数据
    [SerializeField]
    private Transform meleeAttackPosition;//近战攻击位置

    public override void Start()
    {
        base.Start();
        moveState = new E1_MoveState(this, stateMachinel, "move", moveStateData, this);//创建移动状态实例
        idleState = new E1_IdleState(this, stateMachinel, "idle", idleStateData, this);//创建空闲状态实例
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachinel, "playerDetected", playerDetectedStateData, this);
        chargeState = new E1_ChargeState(this, stateMachinel, "charge", chargeStateData, this);//创建冲锋状态实例
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachinel, "lookForPlayer", lookForPlayerStateData, this);//创建寻找玩家状态实例
        meleeAttackState= new E1_MeleeAttackState(this, stateMachinel, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);//创建近战攻击状态实例


        stateMachinel.Initialize(moveState);
        

    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
