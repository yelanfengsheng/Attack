using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTreasures : MonoBehaviour
{
    [Header("宝藏预制体")]
    public GameObject[] treasurePrefab;
    public Vector3 offset;

    [Header("生成设置")]
    public bool autoGenerateOnStart = false;
    public int minGenerateCount = 1;
    public int maxGenerateCount = 3;

    [Header("生成权重（总和最好为100）")]
    [Range(0, 100)] public int coinWeight = 60;      // 金币权重
    [Range(0, 100)] public int healthWeight = 30;    // 生命权重
    [Range(0, 100)] public int strengthWeight = 10;  // 力量权重

    void Start()
    {
        if (autoGenerateOnStart)
        {
            GenerateRandomTreasure();
        }
    }

    // 公开方法：随机生成宝藏
    public void GenerateRandomTreasure()
    {
        int generateCount = Random.Range(minGenerateCount, maxGenerateCount + 1);

        for (int i = 0; i < generateCount; i++)
        {
            int treasureIndex = GetRandomTreasureIndex();
            CreateSingleTreasure(treasureIndex);
        }

        Debug.Log($"随机生成了 {generateCount} 个宝藏");
    }

    // 根据权重随机选择宝藏类型
    private int GetRandomTreasureIndex()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue < coinWeight)
        {
            Debug.Log("随机到: 金币");
            return 0; // 假设索引0是金币
        }
        else if (randomValue < coinWeight + healthWeight)
        {
            Debug.Log("随机到: 生命值");
            return 1; // 假设索引1是生命值
        }
        else
        {
            Debug.Log("随机到: 力量道具");
            return 2; // 假设索引2是力量道具
        }
    }

    // 创建单个宝藏
    private void CreateSingleTreasure(int index)
    {
        if (index < 0 || index >= treasurePrefab.Length)
        {
            Debug.LogWarning($"宝藏索引超出范围: {index}");
            return;
        }

        // 在随机位置生成
        Vector3 randomOffset = new Vector3(
            Random.Range(-1.5f, 1.5f),
            Random.Range(-1.5f, 1.5f),
            0
        );

        Instantiate(treasurePrefab[index], transform.position + offset + randomOffset, Quaternion.identity);
    }

    // 编辑器调试可视化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + offset, 2f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, 0.2f);
    }
}