using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;//移动状态数据
    protected bool isDetectingLedge;//是否检测到悬崖
    protected bool isDetectingWall;//是否检测到墙
    protected bool isPlayerInMinAgroRange;//玩家是否在最小攻击范围内
   
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()//检测 是否检测到悬崖和墙壁和玩家是否在最小攻击范围内
    {
        base.DoChecks();
        isDetectingLedge = entity.CheckLedge();//检测悬崖
        isDetectingWall = entity.CheckWall();//检测墙壁
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);

       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();//基于类的物理更新 原来的DoChecks方法实际上也是在这里调用的

    }
}
