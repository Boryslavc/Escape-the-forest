using UnityEngine;
using DG.Tweening;

public class FlySimulator : MonoBehaviour
{
    [SerializeField] private float[] FlyingHeits = {-4f, 0, 5.45f, 12};
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] FeatherAccountant featherAccountant;
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem fallingEffect;

    // probably better to update fields, than to create new variables each call
    private float xDir = 0;
    private float yDir = 0;

    private float maxHeight;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";

    private float toDescendInterval;
    private float timeSpendMoving = 0f;

    //private bool isCurrentlyMoving;

    private void Start()
    {
        toDescendInterval = featherAccountant.TimeToMoveToLoseFeather;
        maxHeight = FlyingHeits[3];
    }
    private void Update()
    {
        Move();

        CheckInputDuration();

        LowerFlyHeight();
    }

    private void Move()
    {
        Vector3 moveDir;

        yDir = SpecifyYDir();

        xDir = Input.GetAxis(horizontalAxis);
        moveDir = new Vector3(xDir, yDir) * movementSpeed;
        transform.Translate(moveDir * Time.deltaTime);
    }

    private float SpecifyYDir()
    {
        float inputValue = Input.GetAxis(verticalAxis);
        if (transform.position.y < maxHeight)
            return inputValue;
        else if(Mathf.Abs(transform.position.y - maxHeight) < 0.05f && inputValue <= 0)
            return inputValue;
        else
            return 0f;
    }
    private void CheckInputDuration()
    {
        bool moveIsHappening = new Vector2(xDir, yDir).magnitude > 0.01f;

        if (moveIsHappening)
        {
            timeSpendMoving += Time.deltaTime;
        }
    }
    private void LowerFlyHeight()
    {
        if (timeSpendMoving >= toDescendInterval)
        {
            timeSpendMoving = 0;
            featherAccountant.ChangeFeatherCountBy(-1);
            int currentMaxHeight = featherAccountant.GetFeatherCount() - 1;
            if (currentMaxHeight < 0)
                return;
            maxHeight = FlyingHeits[currentMaxHeight];
            if (transform.position.y > maxHeight)
            {
                transform.DOMoveY(maxHeight, 0.5f);
                fallingEffect.Play();
            }
        }
    }
    public void RaiseFlyHeight(int currentMaxHeight)
    {
        maxHeight = FlyingHeits[currentMaxHeight];
        timeSpendMoving = 0;
    }
}
