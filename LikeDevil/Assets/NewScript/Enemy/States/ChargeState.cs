using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;//冲锋状态数据

    protected bool isPlayerInMinAgroRange;//玩家是否在最小攻击范围内

    protected bool isDetectingWall;//是否检测到墙壁
    protected bool isDetectingLedge;//是否检测到悬崖
    protected bool isChargeTimeOver;//冲锋时间是否结束
    protected bool performCloseRangeAction;//执行近战动作

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)//初始化基类
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAggroRange();//检测玩家是否在最小攻击范围内
        isDetectingWall = entity.CheckWall();//检测墙壁    避免玩家冲锋时敌人撞墙
        isDetectingLedge = entity.CheckLedge();//检测悬崖  避免玩家冲锋时敌人掉下悬崖
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();//检测玩家是否在近战范围内
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver= false;
        entity.SetVelocity(stateData.chargeSpeed);//设置实体速度为冲锋速度
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.chargeTime)//如果当前时间大于等于开始时间加上冲锋时间
        {
            isChargeTimeOver = true;//冲锋时间结束
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
      
    }
}
