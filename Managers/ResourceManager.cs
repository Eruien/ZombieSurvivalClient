using UnityEngine;

// ���ҽ� ������ �����հ� ���Ҹ� �ε� �� ��
public class ResourceManager
{
    // ���ҽ� �ε�
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    // ������ �ε�
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

    // ���ҽ� �ı�
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Object.Destroy(go);
    }
}
