using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] GameObject objectToThrow;
    [SerializeField] GameObject target;

    private AudioSource throwEffect;
    private Vector3 spawnOffset = new Vector3(0.15f, 0, 0);

    private void Start()
    {
        throwEffect = GetComponent<AudioSource>();
    }

    public void Throw()
    {
        var rock = Spawner.Instance.SpawnRock();
        rock.gameObject.transform.position = transform.position + spawnOffset;
        Vector3 throwDir = (target.transform.position - transform.position).normalized;
        Debug.Log(throwDir);
        rock.SetMoveDirection(throwDir);
        throwEffect.Play();
    }
}
