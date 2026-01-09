using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    [SerializeField]
    private bool inX;//
    [SerializeField]
    private bool inY;//

    public Vector2 parallaxSpeed;//视差速度
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;//计算纹理单位大小
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 needCameraMovement = cameraTransform.position - lastCameraPosition;
        
        transform.position += new Vector3(needCameraMovement.x * parallaxSpeed.x, needCameraMovement.y * parallaxSpeed.y);//根据相机移动调整背景位置
        lastCameraPosition = cameraTransform.position;
        //无限滚动背景
        if(inX)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;//计算偏移位置
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);//更新背景位置
            }
        }
       if(inY)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;//计算偏移位置
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);//更新背景位置
            }
        }
      
    }
}
