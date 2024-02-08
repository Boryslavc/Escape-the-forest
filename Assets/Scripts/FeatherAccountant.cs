using UnityEngine;
using System;


public class FeatherAccountant : MonoBehaviour
{
    [SerializeField] private const int featherMaxCount = 4;
    [SerializeField] private FeathersDisplay feathersDisplay;

    public event Action OnFeathersEqualsZero;

    public FlySimulator FlySimulator;

    public float TimeToMoveToLoseFeather { get; private set; } = 4f;

    private int featherCurrentCount = 4;


    public int GetFeatherCount()
    {
        return featherCurrentCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feather"))
        {
            if (featherCurrentCount == featherMaxCount)
                return;
            else
            {
                ChangeFeatherCountBy(1);
                collision.gameObject.SetActive(false);
                FlySimulator.RaiseFlyHeight(featherCurrentCount - 1);
            }
        }
    }
    public void ChangeFeatherCountBy(int amountToAdd)
    {
        featherCurrentCount += amountToAdd;
        feathersDisplay.DisplayCurrentImage(amountToAdd == 1 ? true : false) ;
        if (featherCurrentCount == 0)
             OnFeathersEqualsZero?.Invoke();
    }
}
