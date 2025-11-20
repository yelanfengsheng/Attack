using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    
    public GameObject afterImagePrefab;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static PlayerAfterImagePool Instance { get; private set; } //单例对象池 
    private void Awake()
    { 
        Instance = this;
        GrowPool();
    }
    private void GrowPool() //为image池扩容
    {
        for (int i = 0; i <10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);//实例化残影预制体
            instanceToAdd.transform.SetParent(transform);//设置父物体
            AddToPool(instanceToAdd);
        }
    }
    public void AddToPool(GameObject instance)//添加实例到image池
    {
        instance.SetActive(false);//将实例设为不激活
        availableObjects.Enqueue(instance);//添加实例到image池
    }
    public GameObject GetFromPool()//从image池获取实例
    {
        if (availableObjects.Count == 0)//如果image池为空
        {
            GrowPool();
        }
        var instance = availableObjects.Dequeue();//从image池获取一个实例
        instance.SetActive(true);//将实例设为激活
        return instance;
    }
}
