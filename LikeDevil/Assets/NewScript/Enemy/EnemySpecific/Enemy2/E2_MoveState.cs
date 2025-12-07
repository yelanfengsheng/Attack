using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 enemy;
    public E2_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        //如果玩家进入最小仇恨范围 切换到玩家检测状态
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);//切换到玩家检测状态
           
        }
        //如果检测到墙或者没有检测到台阶
       else if (isDetectingWall||!isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);//设置空闲时间后翻转
            stateMachine.ChangeState(enemy.idleState);//切换到空闲状态
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
