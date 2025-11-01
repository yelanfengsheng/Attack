using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("玩家血量")]
    public int maxHealth = 5;
    public int currentHealth;
    private Animator animator;
    public GameObject hurtTx;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        HealthBarUI.nowHealth = currentHealth;
        HealthBarUI.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void PlayerTakeDamage(int damageAmount)
    {

        FlashWhiteEffect flashWhiteEffect = GetComponent<FlashWhiteEffect>();
        if (flashWhiteEffect == null)
        {
            Debug.LogError("FlashWhiteEffect component not found on the player!");
        }
        flashWhiteEffect.PlayFlashFX();// 播放闪白特效

        currentHealth -= damageAmount;
        // 显示伤害文本
        hurtTx.GetComponent<TextMesh>().text = damageAmount.ToString();
        GameObject hurtTextInstance = Instantiate(hurtTx, this.transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity);
        
        Destroy(hurtTextInstance, 0.5f); // 1秒后销毁伤害文本

        HealthBarUI.nowHealth = currentHealth;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    private void Die()
    {
        // 玩家死亡逻辑
        Debug.Log("Player Died!");
        animator.SetBool("isDead", true);
        //MyPlayerController playerController = GetComponent<MyPlayerController>();
        //if (playerController != null)
        //{
        //    playerController.enabled = false; // 禁用玩家控制脚本
        //}
        DisableAllPlayerScripts();
      
    
    }
    private void DisableAllPlayerScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this) // 不禁用当前脚本
            {
                script.enabled = false;
            }
        }
        MonoBehaviour[] childScripts = GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in childScripts)
        {
            script.enabled = false;
        }
    }
    public void OnableAllPlayerScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this) // 不禁用当前脚本
            {
                script.enabled = true;
            }
        }
        MonoBehaviour[] childScripts = GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in childScripts)
        {
            script.enabled = true;
        }
    }

}
