using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State Data")]
public class D_ChargeState : ScriptableObject
{
    public float chargeSpeed = 6f;//冲锋速度

    public float chargeTime = 2f;//冲锋时间
}
