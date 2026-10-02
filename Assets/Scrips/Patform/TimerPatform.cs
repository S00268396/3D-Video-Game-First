using UnityEngine;

public class TimerPatform : Patform
{
    [Header("TimerPatform Settings:")]
    [SerializeField]
    private float Timer = 1200f; //20 minutes in seconds
    [SerializeField]
    private float ElaspedTime = 0f; //Tracks how much time has passed

    //Controls when the platform starts its timer cycle
    private bool StartMove = false;

    protected override void Update()
    {
        //Do nothing until the platform is activated
        if (!StartMove)
        {
            return;
        }

        //Increase timer every frame
        ElaspedTime += Time.deltaTime;

        //When the platform is active, 
        if (IsActive)
        {
            base.Update();

            //Switch off after the timer runs out
            if (ElaspedTime >= Timer)
            {
                NotActive();
                ElaspedTime = 0f;
            }
        }
        else
        {   //Switch ON after the same timer delays 
            if (ElaspedTime >= Timer)
            {
                Active();
                ElaspedTime = 0f;
            }
        }

    }

    //Called when the platform is activated
    public override void Active()
    {
        base.Active();
        StartMove = true;// Start the timer
        ElaspedTime = 0f; //Reset Timer
    }
}
