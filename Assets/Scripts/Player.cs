using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float LerpSpeed = 2f;
    [SerializeField] private FeatherAccountant featherAccountant;
    [SerializeField] private ParticleSystem flyingEffect;
    [SerializeField] private ParticleSystem getHitEffect;

    public event UnityAction OnPlayerDied;

    public Transform PlayerSpawnPoint;
    public Transform StartRunPoint;

    private const string EndOfGameB = "EndOfGameB";
    private Animator playerAnimator;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        featherAccountant.OnFeathersEqualsZero += OnDie;
        playerAnimator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        flyingEffect.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsHurtable(collision))
        {
            getHitEffect.Play();
            featherAccountant.ChangeFeatherCountBy(-1);
        }
    }
    private bool IsHurtable(Collider2D collidedWith)
    {
        return collidedWith.gameObject.CompareTag("Rock") || collidedWith.gameObject.CompareTag("Tree");
    }
    public void StartIntro()
    {
        rigidbody2D.gravityScale = 0;
        playerAnimator.SetBool(nameof(EndOfGameB), false);
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
    }
    public void OnDie()
    {
        playerAnimator.SetBool(EndOfGameB, true);
        rigidbody2D.gravityScale = 1.5f;
        OnPlayerDied?.Invoke();
        flyingEffect.Stop();
    }
    private void OnDestroy()
    {
        featherAccountant.OnFeathersEqualsZero -= OnDie;
    }
}
