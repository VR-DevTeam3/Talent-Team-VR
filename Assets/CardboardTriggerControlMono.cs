using UnityEngine;
using System.Collections;

public class CardboardTriggerControlMono : MonoBehaviour
{
    public bool magnetDetectionEnabled = true;

    void Start()
    {
        CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
        // Disable screen dimming:
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


    }

    void Update()
    {
        if (!magnetDetectionEnabled) return;
        if (CardboardMagnetSensor.CheckIfWasClicked())
        {
            Debug.Log("Cardboard trigger was just clicked");
            //do shit
            CardboardMagnetSensor.ResetClick();

        }
    }
}