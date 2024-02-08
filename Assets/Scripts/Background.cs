using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed = 1;

    private bool shouldRoll;

    Material material;

    private void OnDisable()
    {
        SessionManager.Instance.OnGameOver -= FinishTask;
    }
    // session manager instance initialization takes place in awake,
    // so need to subscribe in start
    // not another awake
    private void Start()
    {
        shouldRoll = true;
        material = GetComponent<MeshRenderer>().material;
        SessionManager.Instance.OnGameOver += FinishTask;
    }

    private void Update()
    {
        if(shouldRoll)
        {
            Vector2 offset = new Vector2(rotatingSpeed * Time.deltaTime, 0);
            material.mainTextureOffset += offset;
        }
    }

    public void FinishTask()
    {
        shouldRoll = false;
    }
}
