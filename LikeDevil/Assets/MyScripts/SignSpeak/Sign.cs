using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject speakPlane; // 关联的UI平面
    public Text speakText;
    public string nowMessage;//想要显示的文本信息

    [Header("文本文件相关")]
    public TextAsset textFile;//文本文件
    private int currentLineIndex = 0;//当前行索引
    public string[] messages; // 多条文本列表

    public bool playerInRange = false; // 玩家是否在触发区域内
    public float typingSpeed = 0.05f; // 打字机效果的速度

    private bool isDisplaying = false; // 是否正在显示对话
    private string lastDisplayedMessage = ""; // 最后显示的消息

    void Start()
    {
        // 初始化时隐藏面板
        if (speakPlane != null)
            speakPlane.SetActive(false);

        // 预加载文本
        LoadTextFromFile();
    }

    void Update()
    {
        // 简化条件判断，移除重复的 playerInRange
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDisplaying)
            {
                // 开始显示对话
                StartDialogue();
            }
            else
            {
                // 显示下一句对话
                ShowNextMessage();
            }
        }
    }

    private void StartDialogue()
    {
        isDisplaying = true;
        speakPlane.SetActive(true);
        speakText.text = ""; // 清空文本

        // 显示当前消息
        ShowCurrentMessage();
    }

    private void ShowNextMessage()
    {
        if (messages != null && messages.Length > 0)
        {
            // 移动到下一行（循环）
            currentLineIndex = (currentLineIndex + 1) % messages.Length;
            
            ShowCurrentMessage();
        }
    }

    private void ShowCurrentMessage()
    {
        if (messages != null && messages.Length > 0)
        {
            nowMessage = messages[currentLineIndex].Trim(); // 移除首尾空白
            lastDisplayedMessage = nowMessage; // 保存最后显示的消息

            // 停止之前的动画（如果有）
            speakText.DOKill();

            // 开始打字机效果
            speakText.text = "";
            speakText.DOText(nowMessage, nowMessage.Length * typingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            playerInRange = true;
            Debug.Log("玩家进入sign区域");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            playerInRange = false;
            isDisplaying = false;
            speakPlane.SetActive(false);

            Debug.Log("玩家离开，但保留文本内容");

            // 不隐藏面板，保留最后显示的内容
            // speakPlane.SetActive(false); // 注释掉这行，不隐藏面板

            // 保留文本内容，不清空
            // speakText.text = ""; // 注释掉这行，不清空文本
        }
    }

    private void LoadTextFromFile()
    {
        if (textFile != null)
        {
            // 使用更安全的分割方式
            messages = textFile.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            // 清理每行文本（移除\r和空白）
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = messages[i].Replace("\r", "").Trim();
            }

            Debug.Log($"成功加载 {messages.Length} 条消息");
        }
        else
        {
            Debug.LogError("Text file is not assigned in the inspector.");
        }
    }
}