using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float LerpSpeed = 2f;
    [SerializeField] private Player Player;

    public Transform StartRunPoint;
    public Transform SpawnPoint;

    private Animator enemyAnimator;
    private RockThrower thrower;
    private const string EndOfGameB = "EndOfGameB";
    // session manager instance initialization takes place in awake,
    // so need to subscribe in start
    // not another awake
    private void Start()
    {
        SessionManager.Instance.OnGameOver += FinishTask;
        SessionManager.Instance.OnGameStarted += StartIntro;
        enemyAnimator = GetComponent<Animator>();
        thrower = GetComponent<RockThrower>();
    }    

    public void StartIntro()
    {
        transform.position = SpawnPoint.position;
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        Vector3 startPos = gameObject.transform.position;
        Vector3 endPos = StartRunPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * LerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * LerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos,
                fractionOfJourney);
            yield return null;
        }
        StartThrowing();
    }

    private void StartThrowing()
    {
        thrower.StartThrowing();
    }

    private void StopThrowing()
    {
        thrower.StopThrowing();
    }

    public void FinishTask()
    {
        StopThrowing();
        enemyAnimator.SetBool(nameof(EndOfGameB), true);
    }

    private void OnDisable()
    {
        SessionManager.Instance.OnGameOver -= FinishTask;
        SessionManager.Instance.OnGameStarted -= StartIntro;
    }
}
