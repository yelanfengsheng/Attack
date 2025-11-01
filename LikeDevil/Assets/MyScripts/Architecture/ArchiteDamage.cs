using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArchiteDamage : MonoBehaviour
{
    public DamageController.E_DamageType damageType;

    [Tooltip("伤害值")]
    public float istantDamage = 5f;
    public float continuousDamage = 2f;
    


    [Tooltip("持续伤害的间隔时间（秒）")]
    public float continuousDamageInterval = 1f;

    [Tooltip("持续伤害的总持续时间（秒）")]
    public float continuousDamageDuration = 5f;
    // 持续伤害相关
    private bool isContinuousDamageActive = false;
    private float continuousDamageTimer = 0f;
   

    // 玩家检测
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        // 查找玩家
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {

            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (damageType)
        {
            case DamageController.E_DamageType.InstantDamage:
                this.GetComponent<Renderer>().material.color = Color.red;
                //ApplyContinuousDamage();// 直接伤害

                break;
            case DamageController.E_DamageType.ContinuousDamage:
                this.GetComponent<Renderer>().material.color = Color.blue;
                //StartContinuousDamageToPlayer();// 持续伤害
                break;
            //case DamageController.E_DamageType.Earth:
            //    this.GetComponent<Renderer>().material.color = Color.green;
            //    break;
            //case DamageController.E_DamageType.Air:
            //    this.GetComponent<Renderer>().material.color = Color.white;
            //    break;
            default:
                break;
        }
        // 持续伤害计时
        if (isContinuousDamageActive)
        {
            continuousDamageTimer += Time.deltaTime;
            if (continuousDamageTimer >= continuousDamageDuration)
            {
                StopContinuousDamage();
            }
        }
    }

   

    public void  ApplyInstantDamageToPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.PlayerTakeDamage((int)istantDamage);
            Debug.Log(playerHealth.currentHealth);
        }
    }
    public void StartContinuousDamageToPlayer()
    {
        if (!isContinuousDamageActive && playerHealth != null)
        {
            isContinuousDamageActive = true;
            continuousDamageTimer = 0f;
            StartCoroutine(ApplyContinuousDamage());
            Debug.Log("开始持续伤害");
        }
    }

    IEnumerator ApplyContinuousDamage()
    {
       yield return new WaitForSeconds(continuousDamageInterval);
        while (isContinuousDamageActive && continuousDamageTimer < continuousDamageDuration)
        {
            if (playerHealth != null)
            {
               playerHealth.PlayerTakeDamage((int)continuousDamage);
                Debug.Log("持续伤害，当前血量：" + playerHealth.currentHealth);
            }
            yield return new WaitForSeconds(continuousDamageInterval);
        }
        isContinuousDamageActive = false;
        StopContinuousDamage();
    }
    private void StopContinuousDamage()
    {
      
            
            continuousDamageTimer = 0f;
            StopAllCoroutines();
            Debug.Log("停止持续伤害");
        
    }
    
  // 玩家进入范围
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
         
            Debug.Log("玩家进入伤害范围");

            // 如果是直接伤害类型，立即造成伤害
            if (damageType == DamageController.E_DamageType.InstantDamage)
            {
                ApplyInstantDamageToPlayer();
            }
            if(damageType == DamageController.E_DamageType.ContinuousDamage)
            {
                StartContinuousDamageToPlayer();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isContinuousDamageActive = false;
            Debug.Log("玩家离开伤害范围");
            if (damageType == DamageController.E_DamageType.ContinuousDamage)
            {
                StopContinuousDamage();
            }
        }
    }


}

