using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine //有限状态机 用于跟踪当前状态
{
    public State currentState { get; private set;}//当前状态 别人读取 但是不能修改

    public void Initialize(State startingState)
    {
        currentState = startingState;//当前状态等于初始状态
        currentState.Enter();
       
    }
    public void ChangeState(State newState)//改变状态
    {
        currentState.Exit();//先退出当前状态
        currentState = newState;//当前状态等于新状态
        currentState.Enter();//进入新状态
    }

    
}
