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
    }

    public void DisplayCurrentImage(bool toSetActive)
    {
        if(toSetActive)
        {
            currentImage += 1;
            featherImages[currentImage].gameObject.SetActive(true);
        }
        else if(!toSetActive)
        {
            featherImages[currentImage].gameObject.SetActive(false);
            currentImage += -1;
        }
    }
}
