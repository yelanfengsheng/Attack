using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作用是 将动画事件传递到状态机中 因为动画事件只能调用挂载在物体上的脚本方法 不能直接调用状态机中的方法
public class AnimToStateMachine : MonoBehaviour
{
    public AttackState attackState;//攻击状态引用
    private void TriggerAttack()
    {
        attackState.TriggerAttack();

    }
    private void FinishAttack()
    {
        attackState.FinishAttack();

    }
}
