using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// StunStateData  通过ScriptableObject创建实例
[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State Data")]
public class D_StunState : ScriptableObject
{
   public float stunTime = 3f; // 眩晕时间
   public float stunKnockbackTime = 0.2f; // 眩晕击退时间
   public float stunKnockbackSpeed = 20f; // 眩晕击退速度
   public Vector2 stunKnockbackAngle;
}
