using UnityEngine;

public class ubicacionZona : MonoBehaviour
{
    public string idZona;
    public GameObject objetoVisual;

    private void Start()
    {
        if (objetoVisual != null)
        {
            objetoVisual.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,transform.localScale);
    }
}