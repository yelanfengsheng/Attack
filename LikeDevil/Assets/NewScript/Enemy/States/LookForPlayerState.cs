using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayer stateData;

    protected bool turnImmediately;//立即转向
    protected bool isPlayerInMinAgroRange;//玩家在最小攻击范围内
    protected bool isAllTurnsDone;//所有转向完成
    protected bool isAllTrunsTimeDone;//所有转向时间完成

    protected float lastTurnTime;//上次转向时间
    protected int amountOfTurnsDone;//已转向次数

    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_LookForPlayer stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAggroRange();//检测玩家在最小攻击范围内
    }

    public override void Enter()
    {
        base.Enter();
        isAllTurnsDone = false;
        isAllTrunsTimeDone = false;
        lastTurnTime = Time.time;//记录进入状态的时间为上次转向时间
        amountOfTurnsDone = 0;//已转向次数归零
        lastTurnTime = startTime;
        entity.SetVelocity(0f);//进入状态时将实体速度设为0
    }

    public override void Exit()
    {
        base.Exit();
    }

    // （仅显示修改后的方法片段，替换原文件中相应部分）
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;//将这次转向时间记录为上次转向时间
            amountOfTurnsDone++;//已转向次数加一
            turnImmediately = false;
        }
        // 只有在还没完成所有转向时才继续自动转向
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTrunsTimeDone && !isAllTurnsDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;//将这次转向时间记录为上次转向时间
            amountOfTurnsDone++;//已转向次数加一
            Debug.Log("转向了一次");
        }
        if (amountOfTurnsDone >= stateData.amountOfTurns)//如果已转向次数达到设定的转向次数
        {
            if (!isAllTurnsDone)
            {
                isAllTurnsDone = true;//所有转向完成
              
                // 从此刻开始计时，等待 timeBetweenTurns 后算“所有转向时间完成”
                lastTurnTime = Time.time;
            }
        }
        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)//如果距离上次转向时间超过了设定的转向间隔时间 且所有转向完成
        {
            isAllTrunsTimeDone = true;//所有转向时间完成
            Debug.Log("所有转向时间完成");
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetTurnImmediately(bool flipImmediately)//设置是否立即转向  公开  供外部调用
    {
        this.turnImmediately = flipImmediately;
    }
}
