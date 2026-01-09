using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
  
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_DeadState state) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = state;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Instantiate(stateData.deadBloodParticle, entity.aliveGo.transform.position, stateData.deadBloodParticle.transform.rotation);//实例化流血粒子特效
        GameObject.Instantiate(stateData.deadChunkParticle, entity.aliveGo.transform.position, stateData.deadChunkParticle.transform.rotation);//实例化死亡粒子特效
        Debug.Log("已经创建了死亡粒子,坐标为"+entity.transform.position);

        GameObject.Instantiate(stateData.targetObj, entity.aliveGo.transform.position, stateData.deadBloodParticle.transform.rotation);//实例化目标物体



        entity.gameObject.SetActive(false);//禁用实体游戏对象
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
        base.PhysicsUpdate();
    }
}
