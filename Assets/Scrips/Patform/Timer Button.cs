using System;
using UnityEngine;

public class TimerButton : MonoBehaviour
{
    [Header("Timer Button Settings:")]

    //The platform that will be activated when the player steps on the button.
    [SerializeField]
    private TimerPatform Patform;

    private void OnTriggerEnter(Collider other)
    {
        //When the player enters the trigger area of the button, activate the platform.
        if (other.CompareTag("Player"))
        {
            Patform.Active();
        }
    }    
}


