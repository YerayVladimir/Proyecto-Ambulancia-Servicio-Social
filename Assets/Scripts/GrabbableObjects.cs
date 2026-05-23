using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public string objectID;
    public bool correctlyPlaced = false;

    [HideInInspector]
    public Vector3 startPosition;

    [HideInInspector]
    public Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
}