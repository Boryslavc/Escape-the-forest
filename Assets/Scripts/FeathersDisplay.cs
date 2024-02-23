using UnityEngine;
using UnityEngine.UI;

public class FeathersDisplay : MonoBehaviour
{
    [SerializeField] private FeatherAccountant featherAccountant;

    public Image[] featherImages = new Image[4];
    private int currentImageNumber;

    private void OnEnable()
    {
        currentImageNumber = featherImages.Length - 1;
        featherAccountant.OnFeathersAmountChanged += DisplayCurrentImage;
    }
   // currentImage = arrayIndex, featherCount = amount of feathers
   // currentImage should alway be one less than featherCount
    public void DisplayCurrentImage()
    {
        int feathCount = featherAccountant.GetFeatherCount();
        //player lost a feather
        if (feathCount == currentImageNumber)
        {
            featherImages[currentImageNumber].gameObject.SetActive(false);
            currentImageNumber--;
        }
        //player obtained one feather
        else if ((feathCount - currentImageNumber) == 2)
        {
            currentImageNumber += 1;
            featherImages[currentImageNumber].gameObject.SetActive(true);
        }
    }
    private void OnDisable()
    {
        featherAccountant.OnFeathersAmountChanged -= DisplayCurrentImage;
    }
}
