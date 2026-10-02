using System.Collections;
using TMPro;
using UnityEngine;

public class FireBallMessege : MonoBehaviour
{
    [Header("FireBall Messege Settings:")]
    [SerializeField]
    private MessageTriggerData triggerData; //The message to be displayed when the player reaches the checkpoint
    private bool HassBeenTriggered = false; //Flag to ensure the checkpoint is only triggered once
    private float messageDisplayTime = 3f; //Duration for which the checkpoint message is displayed on the UI   

    [SerializeField]
    private TMP_Text Messege;//Text component to display the checkpoint message on the UI

    //When the player enters the checkpoint trigger area, display the checkpoint message and update the player's checkpoint position
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerCheckpoint playerCheckpoint = other.GetComponent<PlayerCheckpoint>();

            if (!HassBeenTriggered)
            {
                Messege.SetText($"{triggerData.Text}");
                HassBeenTriggered = true;


                if (playerCheckpoint != null)
                {
                    playerCheckpoint.SetNewCheckPoint(transform);
                }
                StartCoroutine(ClearTextAfterDelay(messageDisplayTime)); //Start the coroutine to clear the checkpoint message after the specified delay

            }

        }
    }

    //Coroutine to clear the checkpoint message after a delay
    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Messege.SetText(string.Empty);
    }
}
