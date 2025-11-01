using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAreaVisualizer : MonoBehaviour
{
    public Vector2 canMoveArea;
    public Color areaColor = new Color(0,1,0,0.3f);

    private void OnDrawGizmos()
    {
        // 设置 Gizmos 颜色
        Gizmos.color = areaColor;

        // 绘制矩形区域
        Vector3 center = transform.position;
        Vector3 size = new Vector3(canMoveArea.x, canMoveArea.y, 0.1f);

        Gizmos.DrawCube(center, size);

        // 绘制边框
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }

    private void OnDrawGizmosSelected()
    {
        // 选中时绘制更明显的边框
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position;
        Vector3 size = new Vector3(canMoveArea.x, canMoveArea.y, 0.1f);
        Gizmos.DrawWireCube(center, size);
    }
}
