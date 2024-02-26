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
    private bool shouldMove = false;
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
            transform.Translate(moveDirecion * moveSpeed * Time.deltaTime);
        }
        else
            gameObject.SetActive(false);
    }
    // single resposibility, one method to set active, other to set moving
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
