using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    private Text textComponent;
    private string fullText;
    public float typingSpeed = 0.05f;

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.LogError("未找到 Text 组件！");
            return;
        }

        fullText = textComponent.text;
        textComponent.text = "";

        float totalTime = fullText.Length * typingSpeed;


        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            if (i < fullText.Length) // 最后一次不等待
                yield return new WaitForSeconds(typingSpeed);
        }

        Debug.Log($"耗时 {fullText.Length * typingSpeed:F2} 秒");
    }
}