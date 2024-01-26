using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector3 moveDirecion;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(moveDirecion * speed * Time.deltaTime);

            if (transform.position.x > 20 || transform.position.y > 20)
                gameObject.SetActive(false);
        }
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirecion = direction;
    }
}
