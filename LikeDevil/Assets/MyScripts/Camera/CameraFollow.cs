using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollow : MonoBehaviour
{
    public  Transform target; // The target the camera will follow
    public Vector3 offset; // Offset from the target position
    public float smoothSpeed = 0.2f; // Smoothing speed
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame  
  void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smoothSpeed);
    }
}
