using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetresScript : MonoBehaviour
{
    public Slider slider;
    public Image image;
    public Sprite normalSkull;
    public Sprite scaredSkull;

    public void SetBars(float bars)
    {
        slider.value = bars;
        if (image != null && slider.value >= 0.5)
        {
            image.sprite = scaredSkull;
        }
    }

}
