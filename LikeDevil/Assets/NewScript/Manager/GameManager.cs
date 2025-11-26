using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respownPoint;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private float respownTime;

    private float respownStartTime;

    private bool isRespown;

    private CinemachineVirtualCamera CVCamera;

    private void Start()
    {
        CVCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        if(CVCamera==null)
        {
            Debug.LogError("无法找到虚拟相机");
        }
    }
    private void Update()
    {
        CheckRespown();//检查重生
    }

    public void RespownPlayer()//
    {
        isRespown = true;
        respownStartTime = Time.time;//记录开始重生的时间
    }
    private void CheckRespown()
    {
        if(Time.time> respownStartTime+ respownTime&&isRespown)
        {
          var playerTemp  =  Instantiate(playerPrefab, respownPoint.position,Quaternion.identity);
          CVCamera.m_Follow = playerTemp.transform;
          isRespown = false;
        }
    }
}
