using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepSound;

    public float stepInterval = 0.5f;

    private float stepTimer;

    void Update()
    {
        float movement = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

        if (movement > 0)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                audioSource.PlayOneShot(footstepSound);
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0;
        }
    }
}