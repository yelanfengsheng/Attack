using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LookForPlayerData  通过ScriptableObject创建实例
[CreateAssetMenu(fileName = "newLookForPlayerData", menuName = "Data/State Data/Look For Player Data")]
public class D_LookForPlayer : ScriptableObject
{
    public int amountOfTurns = 2;//转身次数
    public float timeBetweenTurns = 0.7f;//转身间隔时间
}
