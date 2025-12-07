using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MoveState : MoveState
{
    private Enemy3 enemy;
    public E3_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if(isPlayerInMinAgroRange)//如果玩家进入最小仇恨范围
        {
            stateMachine.ChangeState(enemy.playerDetectedState);//切换到玩家检测状态
        }
        else if (isDetectingWall || !isDetectingLedge)
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
