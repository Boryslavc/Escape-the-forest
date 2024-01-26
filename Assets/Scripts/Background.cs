using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed = 1;

    public bool shouldRoll = false;

    Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if(shouldRoll)
        {
            Vector2 offset = new Vector2(rotatingSpeed * Time.deltaTime, 0);
            material.mainTextureOffset += offset;
        }
    }
}
