using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;// 开始状态机
    protected Entity entity;// 开始实体
    public float startTime { get; protected set; }// 状态开始的时刻
    protected string animBoolName;// 动画布尔名称

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName) // 构造函数 用于初始化状态机 后续再其他的子类中都可重写此构造函数
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter() // 进入状态 并初始化开始时间
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);//实体动画 播放  根据名称准确 播放
        DoChecks();//执行用于检查的方法
    }
    public virtual void Exit() // 退出状态
    {
        entity.anim.SetBool(animBoolName, false);//实体动画 停止
    }
    public virtual void LogicUpdate() // 逻辑更新
    {

    }
    public virtual void PhysicsUpdate() // 物理更新
    {
        DoChecks();//执行用于检查的方法
    }
    public virtual void DoChecks() // 执行用于检查的方法
    {

    }
}
