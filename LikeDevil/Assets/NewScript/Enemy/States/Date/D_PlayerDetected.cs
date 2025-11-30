using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实体数据类 通过ScriptableObject创建实例
[CreateAssetMenu(fileName = "newPlayerDetectedData", menuName = "Data/State Data/Player Detected Data")]

public class D_PlayerDetected : ScriptableObject
{
    public float longRangeActionTime =1.5f;//检测到玩家后动作持续时间
}
