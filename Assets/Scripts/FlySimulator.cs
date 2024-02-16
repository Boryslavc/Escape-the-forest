using UnityEngine;
using DG.Tweening;

public class FlySimulator : MonoBehaviour
{
    [SerializeField] private float[] FlyingHeits = {-4f, 0, 5.45f, 12};
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private FeatherAccountant featherAccountant;
    [SerializeField] private ParticleSystem fallingEffect;
    [SerializeField] private float SecondsToMoveToLoseFeather = 5f;

    private bool isAtMaxYValue => Mathf.Abs(transform.position.y - maxHeight) < 0.1f;

    private float xDir = 0;
    private float yDir = 0;

    private float maxHeight;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";

    private float timeSpendMoving = 0f;


    private void Start()
    {
        maxHeight = FlyingHeits[FlyingHeits.Length - 1];
        featherAccountant.OnFeathersAmountChangedAndWentUp += ChangeMaxFlyHeight;
        SessionManager.Instance.OnGameOver += EndTask;
    }
    private void Update()
    {
        Move();

        CheckInputDuration();
    }
    private void Move()
    {
        Vector3 moveDir;

        yDir = SpecifyYValue();

        xDir = Input.GetAxis(horizontalAxis);
        moveDir = new Vector3(xDir, yDir) * movementSpeed;
        transform.Translate(moveDir * Time.deltaTime);
    }
    private float SpecifyYValue()
    {
        float inputValue = Input.GetAxis(verticalAxis);

        //no delay
        if (isAtMaxYValue && inputValue > 0)
            return 0;
        else if (isAtMaxYValue && inputValue <= 0)
            return inputValue;
        else return inputValue;



        // works but there's a delay if player is at highest Y levels
        // caused by DoMove
        //if (transform.position.y > maxHeight)
        //{
        //    transform.DOMoveY(maxHeight - 0.2f, 0.3f);
        //    return 0;
        //}
        //else 
        //    return inputValue;
    }
    private void CheckInputDuration()
    {
        bool moveIsHappening = new Vector2(xDir, yDir).magnitude > 0.01f;

        if (moveIsHappening)
            timeSpendMoving += Time.deltaTime;

        if (timeSpendMoving >= SecondsToMoveToLoseFeather)
        {
            //bad method call, contraddicts this method's meaning 
            featherAccountant.ChangeFeatherCountBy(-1);
            // now i got a problem here, bool param doesnt do anything in this script
            // but vital for feathers dispaly
            ChangeMaxFlyHeight(true);
        }
    }
    private void ChangeMaxFlyHeight(bool toRaise)
    {
        timeSpendMoving = 0;
        int currentMaxHeight = featherAccountant.GetFeatherCount() - 1;
        if (currentMaxHeight < 0)
               return;
        maxHeight = FlyingHeits[currentMaxHeight];


        bool isTooHeigh = (transform.position.y - maxHeight) > 0.6f;
        if (transform.position.y > maxHeight)
            PlayFallSequence(isTooHeigh);
    }
    private void PlayFallSequence(bool isLongFall)
    {
        transform.DOMoveY(maxHeight - 0.2f, 0.4f);
        if (isLongFall)
            fallingEffect.Play();
    }
    // difference with raise method in one command
    // porly combine in one method (what to do with bool param from event then?

    //private void LowerMaxFlyHeight()
    //{
    //    timeSpendMoving = 0;
    //    featherAccountant.ChangeFeatherCountBy(-1);
    //    int currentMaxHeight = featherAccountant.GetFeatherCount() - 1;
    //    if (currentMaxHeight < 0)
    //        return;
    //    maxHeight = FlyingHeits[currentMaxHeight];
    //}
    //public void RaiseFlyHeight()
    //{
    //    int currentMaxHeight = featherAccountant.GetFeatherCount() - 1;
    //    maxHeight = FlyingHeits[currentMaxHeight];
    //    timeSpendMoving = 0;
    //}
    private void EndTask()
    {
        GetComponent<FlySimulator>().enabled = false;
    }
    private void OnDisable()
    {
        SessionManager.Instance.OnGameOver -= EndTask;
    }
}
