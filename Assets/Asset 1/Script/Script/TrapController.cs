using UnityEngine;

public class TrapController : MonoBehaviour
{
    public Animator trapAnimator;
    public string triggerParameterName = "Activate";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateTrap();
        }
    }

    private void ActivateTrap()
    {
        trapAnimator.SetTrigger(triggerParameterName);
    }
}