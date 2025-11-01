using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouldBeEat : MonoBehaviour
{
    public PropType.E_PropType propType;
    // Start is called before the first frame update
    private Collider2D itemCollider;
    [Header("爆炸效果")]
    public float minForce = 5f; // 最小爆炸力
    public float maxForce = 10f; // 最大爆炸力
    public static  bool isCollected = false;


    void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        if (itemCollider == null)
        {
            Debug.LogError("Collider2D component not found on the item!");
        }
     ApplyRandomExplosionForce();
    
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 处理所有子物体的触发事件

            switch (propType)
            {
                case PropType.E_PropType.HealthProp:
                    // 增加玩家生命值
                    Debug.Log("Player health increased!");
                    CollectHpLiquid();
                    break;
                //case PropType.E_PropType.Speed:
                //    // 增加玩家速度
                //    Debug.Log("Player speed increased!");
                //    break;
                //case PropType.E_PropType.Strength:
                //    // 增加玩家力量
                //Debug.Log("Player strength increased!");
                //break;
                case PropType.E_PropType.CoinProp:
                    // 增加玩家金币
                    Debug.Log("Player coins increased!");
                    CollectCoin();
                    break;
                default:
                    Debug.Log("Unknown prop type!");
                    
                    break;
            }
          Destroy(gameObject);
        }
    }
    void ApplyRandomExplosionForce()
    {
        // 获取物体的Rigidbody2D组件
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the item!");
            return;
        }
        // 生成一个随机方向
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        // 生成一个随机力的大小
        float randomForceMagnitude = Random.Range(minForce, maxForce);
        // 计算最终的力向量
        Vector2 force = randomDirection * randomForceMagnitude;
        // 应用力到物体上
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    // 收集金币
    void CollectCoin()
    {
        // 使用静态事件系统：直接调用CoinManager的实例
        if (Coin.Instance != null)
        {
            Coin.Instance.AddCoins(Coin.CoinValue);
        }
        else
        {
            Debug.LogError("CoinManager实例不存在！请确保场景中有CoinManager");
        }

        

        // 销毁金币对象
        Destroy(gameObject);
    }
    void CollectHpLiquid()
    {
        if(HpLiquid.Instance!=null)
        {
            HpLiquid.Instance.AddHealth(HpLiquid.HpValue);
        }
        else
        {
            Debug.LogError("HpLiquid实例不存在！请确保场景中有HpLiquid");
        }
    }
}
