using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] private int poolSize = 20;
    [SerializeField] private Rock rockPrefab;
    [SerializeField] private Feather featherPrefab;

    private List<Rock> rocksPool = new List<Rock>();
    private List<Feather> featherPool = new List<Feather>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        InitializeLists();        
    }
    private void InitializeLists()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var rock = Instantiate(rockPrefab);
            rock.gameObject.SetActive(false);
            rocksPool.Add(rock);
        }

        for (int j = 0;j < poolSize; j++)
        {
            var feather = Instantiate(featherPrefab);
            feather.gameObject.SetActive(false);
            featherPool.Add(feather);
        }
    }

    public Rock GetRock()
    {
        for(int i = 0;i < poolSize; i++)
        {
            if (!rocksPool[i].gameObject.activeInHierarchy)
                return rocksPool[i];
        }
        return null;
    }

    public Feather GetFeather()
    {
        for(var i = 0;i <poolSize;i++)
        {
            if (!featherPool[i].gameObject.activeInHierarchy)
                return featherPool[i];
        }
        return null;
    }
}
