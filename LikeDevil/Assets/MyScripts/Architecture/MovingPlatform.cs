using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;  
    // Start is called before the first frame update
    void Start()
    {
       this.transform.position = startPos;
        this.transform.DOMove(endPos, 3).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
