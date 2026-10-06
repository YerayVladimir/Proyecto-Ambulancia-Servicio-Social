using UnityEngine;

public class botonON : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public controladorSirena perillaSirena;

    private bool sirenaEncendida = false;

    void OnMouseDown()
    {
        sirenaEncendida = !sirenaEncendida;

        if (animator != null)
        {
            animator.SetTrigger("Pulsar");
        }

        if (sirenaEncendida)
        {
            audioSource.clip = perillaSirena.ModoActual;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void ActualizarSiEstaEncendida(AudioClip nuevoClip)
    {
        if (sirenaEncendida)
        {
            audioSource.clip = nuevoClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}