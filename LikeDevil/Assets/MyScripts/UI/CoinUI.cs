using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Text nowCoinTx;
    public static int nowCoinCount;
    public static int coinCount;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nowCoinTx.text = nowCoinCount.ToString();

    }
}
