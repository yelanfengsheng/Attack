using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable  // 添加接口实现
{
    public int health = 3;
    public int damage = 1;

    protected SpriteRenderer sr;
    protected Color originalColor;
    public float flashDuration = 0.15f;

    public ParticleSystem bloodEffect;
    protected PlayerHealth playerHealth;
    public GameObject coin;
    public GameObject hurtTx;

    protected virtual void Start()
    {
        // 查找SpriteRenderer
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            originalColor = sr.color;
        }
        else if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
            originalColor = sr.color;
            if (originalColor != null)
            {
                Debug.Log("找到了 originalColor");
            }
        }

        else
        {
            Debug.LogError("SpriteRenderer not found on enemy: " + gameObject.name);
        }

        // 安全地查找玩家健康组件
        FindPlayerHealth();
    }

    protected void FindPlayerHealth()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogWarning("PlayerHealth component not found on player: " + player.name);
            }
        }
        else
        {
            Debug.LogWarning("Player object not found in scene!");
        }
    }

    // 这个 TakeDamage 方法现在实现了 IDamageable 接口
    public virtual void TakeDamage(int damageAmount)
    {
        StartCoroutine(FlashRedCoroutine());

        if (bloodEffect != null)
        {

            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Instantiate(coin, transform.position, Quaternion.identity);
        }

        health -= damageAmount;

        if (health <= 0)
        {
            Die();

        }
        if (hurtTx != null)
        {
            hurtTx.GetComponent<TextMesh>().text = damageAmount.ToString();
            GameObject hurtTextInstance = Instantiate(hurtTx, transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity);
            Destroy(hurtTextInstance, 0.5f); // 1秒后销毁伤害文本

        }

    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected IEnumerator FlashRedCoroutine()
    {
        if (sr == null) yield break;

        // 闪红效果
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }

            // 添加Trigger检测
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.PlayerTakeDamage(damage);
                Debug.Log("Enemy damaged player via trigger! Damage: " + damage);
                Camera.main.GetComponent<CamreaShark>().PlayCameraShake();
            }
            else
            {
                Debug.LogWarning("player没找到");
            }
        }
    }
}