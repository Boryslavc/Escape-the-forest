using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class FlySimulator : MonoBehaviour
{
    [SerializeField] private float[] FlyingHeits = {-4f, 0, 5.45f,12};
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] FeatherAccountant featherAccountant;
    //[SerializeField] private BoxCollider2D ceiling;

    // probably better to update fields, than to create new variables each call
    private float xDir = 0;
    private float yDir = 0;

    private float maxHeight;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";

    //private float startMoveTime = 0;
    //private float endMoveTime = 0;

    private float toDescendInterval;
    private float timeSpendMoving = 0f;

    //private bool isCurrentlyMoving;

    private void Start()
    {
        toDescendInterval = featherAccountant.TimeToMoveToLoseFeather;
    }
    private void Update()
    {
        Move();

        CheckInputDuration();

        LowerFlyHeight();
    }
    private void Move()
    {
        xDir = Input.GetAxis(horizontalAxis);
        yDir = Input.GetAxis(verticalAxis);

        Vector3 moveDir = new Vector3(xDir, yDir) * movementSpeed;

        transform.Translate(moveDir * Time.deltaTime);
    }
    private void CheckInputDuration()
    {
        bool moveIsHappening = new Vector2(xDir, yDir).magnitude > 0.01f;

        if (moveIsHappening)
        {
            timeSpendMoving += Time.deltaTime;
        }
        //if(!isCurrentlyMoving && moveIsHappening)
        //    startMoveTime = Time.time;
        //else if(isCurrentlyMoving && !moveIsHappening)
        //    endMoveTime = Time.time;

        //float duration = endMoveTime - startMoveTime;
        //isCurrentlyMoving = moveIsHappening;
    }
    private void LowerFlyHeight()
    {
        if (timeSpendMoving >= toDescendInterval)
        {
            featherAccountant.UpdateFeatherCount(-1);
            int currentMaxHeight = featherAccountant.GetFeatherCount() - 1;
            maxHeight = FlyingHeits[currentMaxHeight];
            if (transform.position.y > maxHeight)
                transform.DOMoveY(maxHeight, 0.5f);            
            timeSpendMoving = 0;
            Debug.Log("lowerd to " +  maxHeight);
        }
    }
    public void RaiseFlyHeight(int currentMaxHeight)
    {
        maxHeight = FlyingHeits[currentMaxHeight];
        timeSpendMoving = 0;
        Debug.Log("Height raised to " +  maxHeight);
    }
}
