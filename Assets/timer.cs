using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    // float: start time value
    [SerializeField] float startTime;

    // float: current time
    private float currentTime;

    public TMP_Text TimerText;

    // TMP_Text: timer text component
    [SerializeField] TMP_Text timerText;

    //Wave Spawner object
    [SerializeField] WaveSpawner wavespawner;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0){
            currentTime = 0;
            wavespawner.timerIsDone();
            Destroy(TimerText);
        }
        timerText.text = "Time before battle: " + currentTime.ToString("0.0");
    }
}
