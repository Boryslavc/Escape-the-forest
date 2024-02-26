using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RockThrower : MonoBehaviour
{
    enum ThrowType
    {
        SimpleSingleRock,
        ThreeRockAtOnce,
        WaveOfRocks,
    }
    [SerializeField] GameObject target;
    [SerializeField] Timer timer;
    [SerializeField] private float throwTimeSpan;

    private AudioSource throwEffect;
    private Vector3 spawnOffset = new Vector3(0.15f, 0, 0);
    private Dictionary<ThrowType, UnityAction> throws = new Dictionary<ThrowType, UnityAction>();
    private bool isThrowingRightNow = false;

    public float ThrowForce { get; private set; } = 30;

    private void Start()
    {
        throwEffect = GetComponent<AudioSource>();
        InitializeDictionary();
    }
    private void InitializeDictionary()
    {
        throws.Add(ThrowType.SimpleSingleRock, SimpleThrow);
        throws.Add(ThrowType.ThreeRockAtOnce, ThrowThreeRocksAtOnce);
        throws.Add(ThrowType.WaveOfRocks, StartRockWaveCoroutine);
    }
    public void StartThrowing()
    {
        InvokeRepeating(nameof(ThrowingSequence), 5f, throwTimeSpan);
    }
    public void StopThrowing()
    {
        CancelInvoke(nameof(ThrowingSequence));
    }
    public void ThrowingSequence()
    {
        if(!isThrowingRightNow)
        {
            ThrowType throwType = ChooseThrowType();

            throws[throwType].Invoke();
        }

        //if(ShouldThrowHarder())
        //    ThrowHarder();
    }
    private ThrowType ChooseThrowType()
    {
        int number = Random.Range(0, 3);
        return (ThrowType)number;
    }
    private void SimpleThrow()
    {
        isThrowingRightNow = true;
        var rock = ObjectPooler.Instance.GetRock();
        rock.gameObject.transform.position = transform.position + spawnOffset;
        Vector3 throwDir = (target.transform.position - transform.position).normalized;
        rock.SetMoveDirection(throwDir);
        rock.SetSpeed = ThrowForce;
        rock.gameObject.SetActive(true);
        rock.SetObjectMoving();
        throwEffect.Play();
        isThrowingRightNow = false;
    }
    private void ThrowThreeRocksAtOnce()
    {
        isThrowingRightNow = true;
        Rock[] rocksToThrow = new Rock[3];
        for(int i = 0; i < 3; i++)
        {
            rocksToThrow[i] = ObjectPooler.Instance.GetRock();
            rocksToThrow[i].gameObject.transform.position = transform.position + spawnOffset;
            SetRandomDirection(rocksToThrow[i]);
            rocksToThrow[i].SetSpeed = ThrowForce;
            //if rock is not active, object pooler will return same game object in the next loop 
            rocksToThrow[i].gameObject.SetActive(true);
        }
        foreach (var rock in rocksToThrow)
            rock.SetObjectMoving();
        isThrowingRightNow=false;
    }
    private void SetRandomDirection(Rock rock)
    {
        float x = Random.RandomRange(0.1f,1);
        float y = Random.RandomRange(0.1f, 1);
        Vector2 direction = new Vector2 (x, y);
        rock.SetMoveDirection (direction);
    }
    private bool ShouldThrowHarder()
    {
        return (timer.Score % 20) < 4 && throwTimeSpan > 1;             
    }
    private void StartRockWaveCoroutine()
    {
        StartCoroutine(SetRockWave());
    }
    private IEnumerator SetRockWave()
    {
        isThrowingRightNow = true;
        Vector2[] directions = GetWaveDirections();
        var wait = new WaitForSeconds(0.5f);
        foreach (var direction in directions) 
        {
            var rock = ObjectPooler.Instance.GetRock();
            rock.SetMoveDirection(direction);
            rock.gameObject.transform.position = transform.position + spawnOffset;
            rock.SetSpeed = ThrowForce;
            rock.gameObject.SetActive(true);
            rock.SetObjectMoving();
            yield return wait;
        }
        isThrowingRightNow=false;
    }
    private Vector2[] GetWaveDirections()
    {
        return new Vector2[]{
            new Vector2(0,1),
            new Vector2(0.4f,1),
            new Vector2(0.8f,1),
            new Vector2(1f,1f),
            new Vector2(1f,0.7f),
            new Vector2(1f,0.3f),
            new Vector2(1,0.2f),
            new Vector2(1,0)
        };
    }
    public void ThrowHarder()
    {
        throwTimeSpan -= 0.1f;
        ThrowForce += 4;

        CancelInvoke(nameof(ThrowingSequence));
        StartThrowing();
    }
}
