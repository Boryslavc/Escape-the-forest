using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField] private float[] FeathersSpawnHeights = new float[] { 5, 0, -4f };
    [SerializeField] private GameObject TreePreafb;
    [SerializeField] private Vector3 TreeSpawnPosition;

    private const float probabilityToSpawnTree = 0.7f;


    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        SessionManager.Instance.OnGameStarted += StartSpawning;
        SessionManager.Instance.OnGameOver += FinishTask;
    }
    private void SpawnTree()
    {
        if(ShouldSpawnTree())
        {
            var tree = ObjectPooler.Instance.GetTree();
            tree.SetActive(true);
            tree.transform.position = TreeSpawnPosition;
        }
    }
    private bool ShouldSpawnTree()
    {
        float chance = Random.value;
        return chance < probabilityToSpawnTree;

    }
    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnTree), 5f, 5f);
        InvokeRepeating(nameof(SpawnFeather), 3f, 8f);
    }
    private void StopSpawning()
    {
        CancelInvoke(nameof(SpawnFeather));
        CancelInvoke(nameof(SpawnTree));
    }
    private void SpawnFeather()
    {
        var feather = ObjectPooler.Instance.GetFeather();
        int height = SelectSpawnHeight();
        Vector3 position = new Vector3(30, FeathersSpawnHeights[height], 0);
        feather.gameObject.transform.position = position;
        feather.gameObject.SetActive(true);
    }

    private int SelectSpawnHeight()
    {
        int height = Random.Range(0, 5);

        if (height == 0)
            return 0;
        else if (height == 1)
            return 1;
        else return 2;
    }

    public void FinishTask()
    {
        StopSpawning();
    }
    private void OnDisable()
    {
        SessionManager.Instance.OnGameStarted -= StartSpawning;
        SessionManager.Instance.OnGameOver -= FinishTask;
    }
}
