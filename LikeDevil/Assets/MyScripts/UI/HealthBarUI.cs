using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Text healthTx;
    private Image healthBar;
    public static int nowHealth;
    public static int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        if(healthBar == null)
        {
            Debug.LogError("HealthBar Image component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();  
    }
    public void UpdateHealthBar()
    {   healthBar.fillAmount = (float)nowHealth / (float)maxHealth;
        healthTx.text = nowHealth.ToString() + "/" + maxHealth.ToString();
    }
}
