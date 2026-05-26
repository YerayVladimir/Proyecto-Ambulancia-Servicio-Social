using UnityEngine;

public class Grabber : MonoBehaviour
{
    public float grabDistance = 3f;
    public float moveSpeed = 15f;
    public Transform holdPoint;

    public PlacementUI placementUI;

    private GameObject grabbedObject;
    private Rigidbody grabbedRb;

    void Update()
    {
        // CLICK IZQUIERDO
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedObject == null)
            {
                TryGrab();
            }
            else
            {
                DropObject();
            }
        }
    }
    private void FixedUpdate()
    {
        // MOVER OBJETO
        if (grabbedObject != null)
        {
            MoveObject();
        }
    }

    void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.collider.CompareTag("drag"))
            {
                grabbedObject = hit.collider.gameObject;
                grabbedRb = grabbedObject.GetComponent<Rigidbody>();

                GrabbableObject grabbable = grabbedObject.GetComponent<GrabbableObject>();

                if (grabbable != null)
                {
                    ShowOnlyCorrectZone(grabbable.objectID);
                }

                if (grabbedRb == null)
                {
                    grabbedObject = null;
                    return;
                }

                grabbedRb.useGravity = false;
                grabbedRb.isKinematic = true;
                grabbedRb.drag = 10;
                grabbedRb.freezeRotation = true;
                grabbedRb.WakeUp();
            }
        }
    }

    void ShowOnlyCorrectZone(string objectID)
    {
        PlacementZone[] zones = FindObjectsByType<PlacementZone>(FindObjectsSortMode.None);

        foreach (PlacementZone zone in zones)
        {
            if (zone.visual != null)
            {
                zone.visual.SetActive(zone.zoneID == objectID);
            }
        }
    }

    void HideAllZones()
    {
        PlacementZone[] zones = FindObjectsByType<PlacementZone>(FindObjectsSortMode.None);

        foreach (PlacementZone zone in zones)
        {
            if (zone.visual != null)
            {
                zone.visual.SetActive(false);
            }
        }
    }

    void MoveObject()
    {
        grabbedRb.MovePosition(Vector3.Lerp(grabbedObject.transform.position, holdPoint.position, Time.fixedDeltaTime * moveSpeed));
    }

    void DropObject()
    {
        bool correctZone = false;

        GrabbableObject grabbable =
            grabbedObject.GetComponent<GrabbableObject>();

        Collider[] colliders = Physics.OverlapSphere(
            grabbedObject.transform.position,
            0.5f
        );

        foreach (Collider col in colliders)
        {
            PlacementZone zone =
                col.GetComponent<PlacementZone>();

            if (zone != null)
            {
                if (zone.zoneID == grabbable.objectID)
                {
                    correctZone = true;

                    grabbable.correctlyPlaced = true;

                    grabbedObject.transform.position =
                        zone.transform.position;

                    grabbedObject.transform.rotation =
                        zone.transform.rotation;

                    if (zone.visual != null)
                    {
                        zone.visual.SetActive(false);
                    }

                    if (placementUI != null)
                    {
                        placementUI.AddCorrectObject();
                    }

                    break;
                }
            }
        }

        if (!correctZone)
        {
            grabbedObject.transform.position =
                grabbable.startPosition;

            grabbedObject.transform.rotation =
                grabbable.startRotation;

            if (placementUI != null)
            {
                placementUI.ShowIncorrectMessage();
            }
        }

        grabbedRb.useGravity = true;
        grabbedRb.isKinematic = false;
        grabbedRb.drag = 1;
        grabbedRb.freezeRotation = false;

        grabbedRb.velocity = Vector3.zero;
        grabbedRb.angularVelocity = Vector3.zero;

        HideAllZones();

        grabbedObject = null;
        grabbedRb = null;
    }
}