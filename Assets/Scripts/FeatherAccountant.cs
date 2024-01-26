using UnityEngine;
using TMPro;
using System;


public class FeatherAccountant : MonoBehaviour
{
    [SerializeField] private const int featherMaxCount = 4;

    public event Action OnFeatherZero;

    public FlySimulator FlySimulator;
    public TMP_Text FeatherDisplay;
    public float TimeToMoveToLoseFeather { get; private set; } = 3f;

    private int featherCurrentCount = 4;

    private void Update()
    {
        DisplayFeather();
    }

    private void DisplayFeather()
    {
        FeatherDisplay.text = featherCurrentCount.ToString();
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
                UpdateFeatherCount(1);
                collision.gameObject.SetActive(false);
                FlySimulator.RaiseFlyHeight(featherCurrentCount - 1);
            }
        }
    }
    public void UpdateFeatherCount(int amountToAdd)
    {
        featherCurrentCount += amountToAdd;
        Debug.Log(featherCurrentCount.ToString());
        if (featherCurrentCount == 0)
             OnFeatherZero?.Invoke();
    }
}
