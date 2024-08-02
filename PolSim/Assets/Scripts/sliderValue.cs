using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sliderValue : MonoBehaviour
{
    /// <summary>
    /// WW
    /// </summary>
    public Slider swingSlider;
    // public Slider swingSlider;
    void Start()
    {
        // swingSlider = GetComponent<Slider>();
        swingSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        variableStore.instance.setSwing(swingSlider.value);
    }
}
