using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实现的眩晕效果是继承自StunState类的Enemy1专用眩晕状态类  当Enemy1进入眩晕状态时 会执行该类中的逻辑
//眩晕结束之后 如果玩家在敌人攻击范围内 则切换到近战攻击状态 否则切换到冲刺状态 如果玩家不在敌人视野内 则切换到寻找玩家状态
public class E1_StunState : StunState
{
    private Enemy1 enemy;
    public E1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isStunTimeOver)
        {
            if (performCloseRangeAction)//如果执行近战攻击动作
            {
                stateMachine.ChangeState(enemy.meleeAttackState);//切换到近战攻击状态
            }
            else if (isPlayerInMinAgroRange)//如果玩家在最小攻击范围内
            {
                stateMachine.ChangeState(enemy.chargeState);//切换到冲刺状态
            }
            else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);//设置立即转向
                stateMachine.ChangeState(enemy.lookForPlayerState);//切换到寻找玩家状态
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
