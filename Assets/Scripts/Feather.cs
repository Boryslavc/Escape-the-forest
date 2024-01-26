using UnityEngine;


public class Feather : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.left;
    [SerializeField] private float moveSpeed = 2f;

    private void Update()
    {
        Fly();
    }

    private void Fly()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (gameObject.transform.position.x < -20)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
