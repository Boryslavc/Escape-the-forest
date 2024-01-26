using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private readonly Vector3 moveDirection = Vector3.left;


    void Update()
    {
        if(gameObject.activeSelf)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (gameObject.transform.position.x < -10)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
