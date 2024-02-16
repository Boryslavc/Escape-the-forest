using UnityEngine;
using UnityEngine.UI;

public class FeathersDisplay : MonoBehaviour
{
    [SerializeField] private FeatherAccountant featherAccountant;

    public Image[] featherImages = new Image[4];
    private int currentImage;

    private void OnEnable()
    {
        currentImage = featherImages.Length - 1;
        featherAccountant.OnFeathersAmountChangedAndWentUp += DisplayCurrentImage;
    }

    public void DisplayCurrentImage(bool shouldBeActive)
    {
        if(shouldBeActive)
        {
            currentImage += 1;
            featherImages[currentImage].gameObject.SetActive(true);
        }
        else if(!shouldBeActive)
        {
            featherImages[currentImage].gameObject.SetActive(false);
            currentImage += -1;
        }
    }
    private void OnDisable()
    {
        featherAccountant.OnFeathersAmountChangedAndWentUp -= DisplayCurrentImage;
    }
}
