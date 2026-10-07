using UnityEngine;

public class BotonSimple : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public string nombreTrigger = "Pulsar";

    void OnMouseDown()
    {
        Debug.Log("CLICK recibido en: " + gameObject.name);

        if (animator != null)
        {
            animator.SetTrigger(nombreTrigger);
        }
        else
        {
            Debug.LogWarning("No hay Animator asignado");
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}