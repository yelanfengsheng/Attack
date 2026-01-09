using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在固定的 Y 高度上，沿 X 轴在指定区间内随机生成预制体。
/// 用法：
/// - 将脚本挂到某对象（例如 GameManager）。
/// - 在 Inspector 设置 prefab、数量、X区间与固定Y（相对于世界坐标或玩家位置偏移）。
/// - 运行时调用 SpawnAroundPlayerXAxis() 或 SpawnAlongXAtPosition(...) 触发生成。
/// - 编辑器下按 P 可测试生成（运行时）。
/// </summary>
public class SkilProjectile : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Spawn settings")]
    public int count = 5;                    // 生成数量
    public float minXOffset = -5f;           // 相对于中心的最小 X 偏移
    public float maxXOffset = 5f;            // 相对于中心的最大 X 偏移
    public float fixedY = 0f;                // 固定的 Y 世界坐标（如果 usePlayerAsCenter = true 则为相对玩家的 Y 偏移）
    public bool usePlayerAsCenter = true;    // 是否以玩家位置为中心
    public Vector2 centerOffset = Vector2.zero; // 相对于玩家或指定位置的偏移

    [Header("Spawn options")]
   
    public bool randomizeOrder = true;       // 是否随机生成顺序（可选）
    public string playerTag = "Player";      // 查找玩家的 Tag（当 usePlayerAsCenter == true）

    /// <summary>
    /// 在玩家中心（或 FindWithTag 找到的对象）固定 Y 处沿 X 轴生成。
    /// </summary>
    public void SpawnAroundPlayerXAxis()
    {
        Vector2 center;
        if (usePlayerAsCenter)
        {
            var player = GameObject.FindGameObjectWithTag(playerTag);
            Debug.Log(player.gameObject.transform.position);
            if (player == null)
            {
                Debug.LogWarning("SpawnAlongX: 未找到玩家（Tag=" + playerTag + "）");
                return;
            }
            center = (Vector2)player.transform.position + centerOffset;
            // 如果 fixedY 被视为世界 Y，则不改；若想 fixedY 为相对 Y（偏移），请把上面注释调整
            center.y = player.transform.position.y + centerOffset.y;
        }
        else
        {
            center = centerOffset;
            center.y = fixedY;
        }

        SpawnAlongXAtPosition(center);
    }

    /// <summary>
    /// 在指定世界坐标 center 的固定 Y 处沿 X 轴生成预制体。
    /// </summary>
    public void SpawnAlongXAtPosition(Vector2 center)
    {
        if (prefab == null)
        {
            Debug.LogWarning("SpawnAlongX: prefab 未设置");
            return;
        }
        if (count <= 0) return;

        // 预先生成一组随机 X 值
        List<float> xs = new List<float>(count);
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(minXOffset, maxXOffset) + center.x;
            xs.Add(x);
        }

        if (!randomizeOrder) xs.Sort();

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = new Vector3(xs[i], fixedY != 0f && !usePlayerAsCenter ? fixedY : center.y, 0f);
            GameObject go = Instantiate(prefab, spawnPos, Quaternion.identity);
            
        }
      
    }
    public void DestoryAll()
    {
        

        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Skill");
        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
    }


}