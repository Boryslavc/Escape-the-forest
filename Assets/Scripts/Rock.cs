using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float SetSpeed 
    { 
        set
        {
            moveSpeed = value;
        }
    }
    private Vector3 moveDirecion;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(moveDirecion * moveSpeed * Time.deltaTime);

            if (transform.position.x > 20 || transform.position.y > 20)
                gameObject.SetActive(false);
        }
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirecion = direction;
    }
}
