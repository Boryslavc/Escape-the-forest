using UnityEngine;

public class RockThrower : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Timer timer;
    
    private AudioSource throwEffect;
    private Vector3 spawnOffset = new Vector3(0.15f, 0, 0);
    private float throwTimeSpan = 1.5f;
    private int cyclesCount = 0;

    public float ThrowForce { get; private set; } = 24;

    private void Start()
    {
        throwEffect = GetComponent<AudioSource>();
    }
    public void StartThrowing()
    {
        InvokeRepeating(nameof(Throw), 5f, throwTimeSpan);
    }
    public void StopThrowing()
    {
        CancelInvoke(nameof(Throw));
    }
    public void Throw()
    {
        var rock = Spawner.Instance.SpawnRock();
        rock.gameObject.transform.position = transform.position + spawnOffset;
        Vector3 throwDir = (target.transform.position - transform.position).normalized;
        rock.SetMoveDirection(throwDir);
        rock.SetSpeed = ThrowForce;
        throwEffect.Play();

        if(ShouldThrowHarder())
            ThrowHarder();
    }
    private bool ShouldThrowHarder()
    {
        return (timer.Score % 20) < 4 && throwTimeSpan > 1;             
    }
    public void ThrowHarder()
    {
        throwTimeSpan -= 0.1f;
        ThrowForce += 4;

        CancelInvoke(nameof(Throw));
        StartThrowing();
    }
}
