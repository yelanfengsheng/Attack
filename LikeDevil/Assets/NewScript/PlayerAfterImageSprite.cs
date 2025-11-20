using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;
    [SerializeField]
    private float activeTime = 0.1f;//残影应该存在的持续时间（0.1秒）
    private float timeActived;//残影被激活的时刻（创建时刻）
    [SerializeField]
    private float alpha;
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f; // 透明度递减系数

    private Color color;
    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActived = Time.time;// 记录当前时刻的时间 
    }

    

    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMultiplier;// 透明度递减
        color = new Color(1, 1, 1, alpha);
        SR.color = color;

        if(Time.time>(timeActived+activeTime))// 超出持续时间就会被返回对象池
        {
            //放进对象池
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }

    }
}
