using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zonasManger : MonoBehaviour
{
    public enum ModoAyuda { Todas, Porcentaje, Ninguna }

    [Header("Configuraci�n de ayuda visual")]
    public ModoAyuda modo = ModoAyuda.Todas;

    [Range(0f, 1f)]
    public float porcentajeZonas = 0.5f; // Solo se usa en modo Porcentaje

    private HashSet<string> idsHabilitados = new HashSet<string>();

    private void Awake()
    {
        ubicacionZona[] zonas = FindObjectsByType<ubicacionZona>(FindObjectsSortMode.None);

        // IDs �nicos de todas las zonas de la escena
        List<string> ids = new List<string>();
        foreach (ubicacionZona zona in zonas)
        {
            if (!ids.Contains(zona.zonaID))
            {
                ids.Add(zona.zonaID);
            }
        }

        switch (modo)
        {
            case ModoAyuda.Todas:
                idsHabilitados = new HashSet<string>(ids);
                break;

            case ModoAyuda.Porcentaje:
                // Mezcla Fisher-Yates
                for (int i = ids.Count - 1; i > 0; i--)
                {
                    int j = Random.Range(0, i + 1);
                    string temp = ids[i];
                    ids[i] = ids[j];
                    ids[j] = temp;
                }

                int cantidad = Mathf.RoundToInt(ids.Count * porcentajeZonas);
                for (int i = 0; i < cantidad; i++)
                {
                    idsHabilitados.Add(ids[i]);
                }
                break;

            case ModoAyuda.Ninguna:
                break;
        }
    }

    public bool ZonaHabilitada(string zonaID)
    {
        return idsHabilitados.Contains(zonaID);
    }
}
