using UnityEngine;

public class PlacementZone : MonoBehaviour
{
    public string zoneID;
    public GameObject visual;

    private void Start()
    {
        if (visual != null)
        {
            visual.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position,transform.localScale);
    }
}