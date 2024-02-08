using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float LerpSpeed = 2f;

    public Transform StartRunPoint;
    public Transform SpawnPoint;

    public Player Player;

    private Animator enemyAnimator;
    private ObjectThrower thrower;
    private const string EndOfGameB = "EndOfGameB";

    private void OnDisable()
    {
        SessionManager.Instance.OnGameOver -= FinishTask;
    }
    // session manager instance initialization takes place in awake,
    // so need to subscribe in start
    // not another awake
    private void Start()
    {
        SessionManager.Instance.OnGameOver += FinishTask;
        enemyAnimator = GetComponent<Animator>();
        thrower = GetComponent<ObjectThrower>();
    }

    private void StartShooting()
    {
        InvokeRepeating(nameof(ThrowRock), 3.5f, 1.2f);
    }
    private void ThrowRock()
    {
        thrower.Throw();
    }

    private void StopShooting()
    {
        CancelInvoke(nameof(ThrowRock));
    }
    
    public void StartIntro()
    {
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
        StartShooting();
    }
    public void FinishTask()
    {
        StopShooting();
        enemyAnimator.SetBool(nameof(EndOfGameB), true);
    }
}
