using UnityEngine;
using System;


public class FeatherAccountant : MonoBehaviour
{
    [SerializeField] private const int featherMaxCount = 4;
    [SerializeField] private FeathersDisplay feathersDisplay;

    public event Action OnFeathersEqualsZero;
    public event Action OnFeathersAmountChanged;

    public FlySimulator FlySimulator;

    private int featherCurrentCount;

    private void Start()
    {
        featherCurrentCount = featherMaxCount;
    }
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
            }
        }
    }
    public void ChangeFeatherCountBy(int amountToAdd)
    {
        featherCurrentCount += amountToAdd;
        OnFeathersAmountChanged?.Invoke() ;
        if (featherCurrentCount == 0)
             OnFeathersEqualsZero?.Invoke();
    }
}
