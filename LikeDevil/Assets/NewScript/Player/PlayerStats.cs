using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private GameObject deathChunkPartical, deathBloodPartical;// 死亡时的粒子效果

    private float currentHealth;//玩家当前生命值

    private GameManager Gm;

    private void Start()
    {
        currentHealth = maxHealth;// 初始化当前生命值为最大生命值
        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        // 播放死亡粒子效果
        if (deathChunkPartical != null)
        {
            Instantiate(deathChunkPartical, transform.position, deathChunkPartical.transform.rotation);
        }
        if (deathBloodPartical != null)
        {
            Instantiate(deathBloodPartical, transform.position, deathBloodPartical.transform.rotation);
        }
        Gm.RespownPlayer();//通知游戏管理器进行重生
        Destroy(gameObject);// 销毁玩家对象
       
    }
}
