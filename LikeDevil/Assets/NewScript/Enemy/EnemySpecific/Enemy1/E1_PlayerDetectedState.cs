using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        if(performCloseRangeAction)//执行近程动作
        {
            stateMachine.ChangeState(enemy.meleeAttackState);//切换到近战攻击状态
        }


        else if (performLongRangeAction)//执行远程动作
        {
            
            stateMachine.ChangeState(enemy.chargeState);//切换到空闲状态
        }
        else if(!isPlayerInMaxAgroRange)//如果玩家不在最大攻击范围内
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);//切换到寻找玩家状态
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
