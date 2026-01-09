using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySceneChange : MonoBehaviour
{
    public float changeDelay = 2.5f; // 延迟时间，单位为秒
    public string sceneName; // 要切换到的场景名称`
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(ChangeSceneAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(changeDelay);
       
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
