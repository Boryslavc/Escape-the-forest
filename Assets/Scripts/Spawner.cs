using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float[] feathersSpawnHeight = new float[] { 5, 0, -4f };

    public static Spawner Instance;

    private void OnDisable()
    {
        SessionManager.Instance.OnGameOver -= FinishTask;
    }

    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        SessionManager.Instance.OnGameOver += FinishTask;
    }
    public Rock SpawnRock()
    {
        var rock = ObjectPooler.Instance.GetRock();
        rock.gameObject.SetActive(true);
        return rock;
    }

    public void StartSpawningFeathers()
    {
        InvokeRepeating(nameof(SpawnFeather), 3f, 8f);
    }
    private void StopSpawningFeathers()
    {
        CancelInvoke(nameof(SpawnFeather));
    }
    private void SpawnFeather()
    {
        var feather = ObjectPooler.Instance.GetFeather();
        int height = SelectSpawnHeight();
        Vector3 position = new Vector3(30, feathersSpawnHeight[height], 0);
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
        StopSpawningFeathers();
    }
}
