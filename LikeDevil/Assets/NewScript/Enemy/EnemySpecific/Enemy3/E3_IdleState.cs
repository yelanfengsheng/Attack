using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_IdleState : IdleState
{
    private Enemy3 enemy;
    public E3_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        
        if (isPlayerInMinAgroRange)//如果玩家进入最小仇恨范围
        {
            stateMachine.ChangeState(enemy.playerDetectedState);//切换到玩家检测状态
        }
        else if (isIdleTimeOver)//如果空闲时间结束
        {
            stateMachine.ChangeState(enemy.moveState);//切换到移动状态
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
