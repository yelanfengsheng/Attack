using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhiteEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("闪光特效")]
    [SerializeField] private float flashDuration = 0.3f; // 闪光持续时间
    [SerializeField] private Material hitMat; // 受击时的材质
    private Material originalMat; // 原始的材质

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMat = sr.material; // 记录原始的材质
    }

    //执行闪光特效
    public void PlayFlashFX()
    {
        StartCoroutine(FlashFX());
    }

    // 执行闪光特效的协程
    private IEnumerator FlashFX()
    {
        sr.material = hitMat; // 切换到受击材质
        yield return new WaitForSeconds(flashDuration); // 等待指定的闪光持续时间
        sr.material = originalMat; // 恢复原始材质
    }
}
