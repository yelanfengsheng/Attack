using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerInRange;//玩家是否在范围内
    [TextArea(1,3)]
    public string[] lines;//对话内容

    [SerializeField]
    private bool hasName =false;//是否有名字
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
           
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.F)&&DialogueManager.instance.dialogueBox.activeInHierarchy==false) //玩家在范围内并按下F键且对话框未激活
        {
            DialogueManager.instance.ShowDialogue(lines,hasName);
        }
    }
}
