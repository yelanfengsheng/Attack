using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge State")]
public class D_DodgeState : ScriptableObject
{
    public float dodgeSpeed = 10f;//闪避速度
    public float dodgeTime = 0.2f;//闪避时间
    public float dodgeCooldown = 2f;//闪避冷却时间
    public Vector2 dodgeAngle;//闪避角度
}
