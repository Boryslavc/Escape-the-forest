using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float RotatingSpeed = 1;

    private bool shouldRoll;

    private Material material;
    // session manager instance initialization takes place in awake,
    // so need to subscribe in start
    // not another awake
    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        SessionManager.Instance.OnGameOver += FinishTask;
        SessionManager.Instance.OnGameStarted += StartScrolling;
    }
    private void StartScrolling()
    {
        shouldRoll = true;
    }
    private void Update()
    {
        if(shouldRoll)
        {
            Vector2 offset = new Vector2(RotatingSpeed * Time.deltaTime, 0);
            material.mainTextureOffset += offset;
        }
    }

    public void FinishTask()
    {
        shouldRoll = false;
    }
    private void OnDisable()
    {
        SessionManager.Instance.OnGameStarted -= StartScrolling;
        SessionManager.Instance.OnGameOver -= FinishTask;
    }
}
