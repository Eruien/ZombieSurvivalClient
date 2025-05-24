using UnityEngine;

// �Ѱ� �Ŵ��� Ŭ���� �̱����� ���� ��� �Ŵ����� �� �Ŵ��� Ŭ������ ���� ��ȯ
public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    private FSMManager _FSM = new FSMManager();
    private ResourceManager _resource = new ResourceManager();
    private DataManager _data = new DataManager();
    private MemoryManager _memory = new MemoryManager();
    private ItemManager _Item = new ItemManager();
    private AnimationManager _Animation = new AnimationManager();
    private SoundManager _Sound = new SoundManager();
    
    public static FSMManager FSM { get { return Instance._FSM; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager Data { get { return Instance._data; } }
    public static MemoryManager Memory { get { return Instance._memory; } }
    public static ItemManager Item { get { return Instance._Item; } }
    public static AnimationManager Animation { get { return Instance._Animation; } }
    public static SoundManager Sound { get { return Instance._Sound; } }

    public static bool IsClear = false;
    
    private static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            // �Ŵ����� �ʱ�ȭ �۾�
            _instance._FSM.Init();
            _instance._data.Init();
            _instance._memory.Init();
            _instance._Item.Init();
            _instance._Animation.Init();
            _instance._Sound.Init();
        }
        else
        {
            if (IsClear)
            {
                Clear();
            }
        }
    }

    // ���� �ٲ� �� �Ŵ����� Ŭ���� �۾�
    public static void Clear()
    {
        IsClear = false;
        _instance._Animation.Clear();
        _instance._data.Clear();
        _instance._Item.Clear();
        _instance._memory.Clear();
    }

    // �ڽ� ���� ������Ʈ�� ã�� �� ���
    public static GameObject FindChildObject(GameObject selfObj, string childName)
    {
        Transform[] allObjects = selfObj.transform.GetComponentsInChildren<Transform>();

        foreach (Transform obj in allObjects)
        {
            if (obj.name == childName)
            {
                return obj.gameObject;
            }
        }

        return null;
    }
}
