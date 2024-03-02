using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;

    private Vector3 moveDirecion;
    private bool shouldMove = false;
    public float SetSpeed
    {
        set
        {
            MoveSpeed = value;
        }
    }
    private bool IsInTheBounds => transform.position.x < 30 && transform.position.y < 30;

    void Update()
    {
        if (shouldMove)
            Move();
    }
    private void Move()
    {
        if (IsInTheBounds)
        {
            transform.Translate(moveDirecion * MoveSpeed * Time.deltaTime);
        }
        else
            gameObject.SetActive(false);
    }

    public void SetObjectMoving()
    {
        shouldMove = true;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirecion = direction;
    }
    private void OnDisable()
    {
        shouldMove = false;
    }
}
