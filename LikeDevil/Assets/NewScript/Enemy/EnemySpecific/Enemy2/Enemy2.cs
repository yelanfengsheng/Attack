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
    public override void Start()
    {
        base.Start();
        moveState = new E2_MoveState(this, stateMachinel, "move", moveStateData, this);//创建移动状态实例
        idleState = new E2_IdleState(this, stateMachinel, "idle", idleStateData, this);//创建空闲状态实例
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachinel, "playerDetected", playerDetectedStateData, this);//创建玩家检测状态实例
        meleeAttackState = new E2_MeleeAttackState(this, stateMachinel, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);//创建近战攻击状态实例
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachinel, "lookForPlayer", lookForPlayerStateData, this);//创建寻找玩家状态实例

        stateMachinel.Initialize(moveState);//初始化状态机 设置初始状态为移动状态
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
