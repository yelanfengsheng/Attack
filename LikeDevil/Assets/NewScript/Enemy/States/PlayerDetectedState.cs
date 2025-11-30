
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinAgroRange;//玩家是否在最小攻击范围内
    protected bool isPlayerInMaxAgroRange;//玩家是否在最大攻击范围内
    protected bool performLongRangeAction;//执行远程动作
    protected bool performCloseRangeAction;//执行近战动作


    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)//初始化基类
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAggroRange();//检测玩家是否在最小攻击范围内
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAggroRange();//检测玩家是否在最大攻击范围内
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();//检测玩家是否在近战范围内
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        entity.SetVelocity(0f);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.longRangeActionTime)//达到远程动作时间
        {
            performLongRangeAction = true;//执行远程动作
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
