using UnityEngine;

public class MoveAside : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float xBounds;

    private void Update()
    {
        if (gameObject.transform.position.x > xBounds)
            Move();
        else
            gameObject.SetActive(false);
    }
    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
