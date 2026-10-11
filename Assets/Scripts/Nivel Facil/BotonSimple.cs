using UnityEngine;

public class BotonSimple : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public string nombreTrigger = "Pulsar";

    void OnMouseDown()
    {
       

        if (animator != null)
        {
            animator.SetTrigger(nombreTrigger);
        }
        else
        {
            
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}