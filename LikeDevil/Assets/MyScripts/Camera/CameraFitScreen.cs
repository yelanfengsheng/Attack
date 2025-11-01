using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitScreen : MonoBehaviour
{
    public new Camera camera;

    // Update is called once per frame
    void Update()
    {
        float designWidth = 1600f;//开发中分辨率的宽度
        float designHeight = 1000f;//开发中分辨率的高度
        float designOrthographicSize = 5f;//开发时正交摄像机的大小，3.2*100*2=640；×100是因为Unity中的pixels per unit是100，×2是因为想设置成屏幕的一半
        float designScale = designWidth / designHeight;
        float scaleRate = (float)Screen.width / (float)Screen.height;
        if (scaleRate < designScale)//判断我们设计的比例跟实际比例是否一致，若我们设置的大则进入自适应设置，小的话他会自动自适应
        {
            float scale = scaleRate / designScale;
            camera.orthographicSize = 5f / scale;
        }
        else
        {
            camera.orthographicSize = 5f;
        }
    }
}
