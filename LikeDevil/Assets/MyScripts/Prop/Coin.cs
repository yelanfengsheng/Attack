using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin :MonoBehaviour
{
    // 静态实例，全局可访问
    public static Coin Instance;
    public static int CoinValue = 1;

    [Header("UI组件")]
    public Text coinText;  // 拖拽你的UI Text到这里

    private int currentCoins = 0;

    void Awake()
    {
        // 单例模式设置
        if (Instance == null)
        {
            Instance = this;
            // 如果需要在场景切换时保留，取消下面注释
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 初始化UI显示
        UpdateCoinUI();
    }

    // 公共方法：增加金币（其他脚本可以调用）
    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinUI();
        Debug.Log($"获得 {amount} 金币，当前金币: {currentCoins}");
    }

    // 公共方法：减少金币
    public void SubtractCoins(int amount)
    {
        currentCoins = Mathf.Max(0, currentCoins - amount);
        UpdateCoinUI();
    }

    // 公共方法：获取当前金币数量
    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    // 更新UI显示
    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = currentCoins.ToString();

           
        }
    }


}
