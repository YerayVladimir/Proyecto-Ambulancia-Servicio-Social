using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    void Start()
    {
        // Desactiva VSync para que permita el límite personalizado
        QualitySettings.vSyncCount = 0; 
        // Fija los fotogramas por segundo a 6
        Application.targetFrameRate = 80; 
    }
}
