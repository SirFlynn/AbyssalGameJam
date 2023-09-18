using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetresScript : MonoBehaviour
{
    public Slider slider;

    public void SetBars(float bars)
    {
        slider.value = bars;
    }

}
