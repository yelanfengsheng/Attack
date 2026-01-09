using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    public GameObject dialogueBox;
    public Text dialogueText, nameText;

    [TextArea(1,3)]// 多行文本区域
    public string[] dialogueLines;
    [SerializeField]
    private int currentLine;

    // 打字机相关
    [SerializeField]
    private float charInterval = 0.03f; // 每个字符间隔（秒），可在 Inspector 调整
    private Tween typingTween;

    // Start is called before the first frame update
    void Start()
    {
        if (dialogueLines != null && dialogueLines.Length > 0)
            dialogueText.text = dialogueLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // 如果正在逐字显示，则立即完成当前 Tween（显示整行）
                if (typingTween != null && typingTween.IsActive() && typingTween.IsPlaying())
                {
                    typingTween.Complete();
                }
                else
                {
                    // 向下一行
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        StartTypingCurrentLine();
                    }
                    else
                    {
                        // 对话结束
                        dialogueBox.SetActive(false);
                        dialogueText.DOKill();
                        typingTween = null;
                    }
                }
            }
        }
    
    }

    public void ShowDialogue(string[] newLines, bool hasName)
    {
        dialogueLines = newLines;//设置新的对话内容
        currentLine = 0;//重置当前行索引

        CheckName();//判断人物的名字（可能会增加 currentLine）
        nameText.gameObject.SetActive(hasName);//根据是否有名字来显示或隐藏名称文本

        dialogueText.DOKill();
        StartTypingCurrentLine();

        dialogueBox.SetActive(true);//显示对话框
    }

    private void CheckName()
    {
        // 约定：以 "n-" 开头的行是名字行，紧接着的下一行才是真正的台词
        if (currentLine < dialogueLines.Length && dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");//设置名称文本，去掉前缀
            currentLine++;//跳过名称行，下一行将作为台词
        }
    }

    private void StartTypingCurrentLine()
    {
        // 保护：当前索引越界时退出
        if (currentLine < 0 || currentLine >= dialogueLines.Length)
        {
            dialogueText.text = "";
            return;
        }

        // 结束上一个 Tween（如果还在）
        dialogueText.DOKill();

        string line = dialogueLines[currentLine];
        float duration = Mathf.Max(0.01f, line.Length * charInterval);

        // 使用 DOText 逐字显示
        typingTween = dialogueText.DOText(line, duration).SetEase(Ease.Linear);
    }
}
