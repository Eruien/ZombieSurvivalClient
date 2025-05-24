using UnityEngine;

// 총괄 매니저 클래스 싱글턴을 통해 모든 매니저는 이 매니저 클래스를 통해 반환
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

            // 매니저들 초기화 작업
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

    // 씬이 바뀔 때 매니저들 클리어 작업
    public static void Clear()
    {
        IsClear = false;
        _instance._Animation.Clear();
        _instance._data.Clear();
        _instance._Item.Clear();
        _instance._memory.Clear();
    }

    // 자식 게임 오브젝트를 찾을 때 사용
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
