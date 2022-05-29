using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillbarScript : MonoBehaviour
{
    public Slider slider;
    public Text sliderText;

    public float MaxTime = 1.5f;
    public float ActiveTime = 1.5f;

    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
            //sliderText.text = (slider.value * 100).ToString("0.00") + "%";
            sliderText.text = Mathf.Max((MaxTime - ActiveTime),0).ToString() + "s";
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveTime += Time.deltaTime;
        var percent = ActiveTime / MaxTime;
        CurrentValue = Mathf.Lerp(0, 1, percent);

    }

}
