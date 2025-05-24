using UnityEngine;

// 리소스 관리자 프리팹과 리소를 로드 할 때
public class ResourceManager
{
    // 리소스 로드
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    // 프리팹 로드
    public GameObject Instantiate(string path, Vector3 pos)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(original, pos, Quaternion.identity);

        return go;
    }

    // 리소스 파괴
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Object.Destroy(go);
    }
}
