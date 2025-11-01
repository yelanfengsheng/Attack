using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaShark : MonoBehaviour
{
    private Animator cameraAnim;
    // Start is called before the first frame update
    
    void Start()
    {
        cameraAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayCameraShake()
    {
        cameraAnim.SetTrigger("shark");
    }
}
