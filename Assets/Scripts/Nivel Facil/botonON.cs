using UnityEngine;

public class botonON : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public controladorSirena perillaSirena;

    private bool sirenaEncendida = false;

    void OnMouseDown()
    {
        Debug.Log("CLICK en botonON: " + gameObject.name);

        sirenaEncendida = !sirenaEncendida;

        if (animator != null)
        {
            animator.SetTrigger("Pulsar");
        }

        if (audioSource == null)
        {
            Debug.LogWarning("Falta asignar el Audio Source");
            return;
        }

        if (sirenaEncendida)
        {
            if (perillaSirena == null || perillaSirena.ModoActual == null)
            {
                Debug.LogWarning("perillaSirena no está asignada o ModoActual está vacío (no hay clip)");
                return;
            }

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