using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTimeLine : MonoBehaviour
{
    public string sceneName;
    public string timelineObjectName = "TimeLine"; // 非激活物体的名字

    private GameObject timelineObject;

    void Start()
    {
        // 查找非激活物体（按名字）
        timelineObject = FindInactiveObjectInScene(timelineObjectName);
        if (timelineObject == null)
        {
            Debug.LogError($"❌ 找不到名字为 '{timelineObjectName}' 的物体（包括非激活的）");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && timelineObject != null)
        {
            timelineObject.SetActive(true);
            StartCoroutine(DelaySceneChange());
        }
    }

    // 查找当前场景中（包括非激活）指定名字的物体
    private GameObject FindInactiveObjectInScene(string name)
    {
        var rootObjects = SceneManager.GetSceneAt(0).GetRootGameObjects();
        foreach (var root in rootObjects)
        {
            var found = root.transform.FindDeepChild(name);
            if (found != null)
                return found.gameObject;
        }
        return null;
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}

// 扩展方法：深度查找子物体（包括非激活）
public static class TransformExtensions
{
    public static Transform FindDeepChild(this Transform parent, string name)
    {
        if (parent.name == name)
            return parent;

        foreach (Transform child in parent)
        {
            var result = child.FindDeepChild(name);
            if (result != null)
                return result;
        }
        return null;
    }
}