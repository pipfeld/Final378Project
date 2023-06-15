using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battlehealthbar : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setHealthBar(float currentVal, float maxVal){
        if (slider.value >= 0){
            slider.value = currentVal / maxVal;
        }
        else{
            slider.value = 0;
        }
    }
}
