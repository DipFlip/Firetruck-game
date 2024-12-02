using UnityEngine;

public class BrakeLightsController : MonoBehaviour
{
    [SerializeField] private Light leftBrakeLight;
    [SerializeField] private Light rightBrakeLight;
    [SerializeField] private MeshRenderer leftBrakeLightMesh;
    [SerializeField] private MeshRenderer rightBrakeLightMesh;

    private Color brakeColor = Color.red;
    private Color normalColor = Color.white;
    private float brakeIntensity = 1f;
    private float normalIntensity = 0.25f;

    public void BrakeLightsOn()
    {
        // Set lights intensity
        leftBrakeLight.intensity = brakeIntensity;
        rightBrakeLight.intensity = brakeIntensity;
        
        // Set lights color
        leftBrakeLight.color = brakeColor;
        rightBrakeLight.color = brakeColor;
        
        // Set mesh material emission
        leftBrakeLightMesh.material.SetColor("_EmissionColor", brakeColor);
        rightBrakeLightMesh.material.SetColor("_EmissionColor", brakeColor);
    }

    public void BrakeLightsOff()
    {
        // Reset lights intensity
        leftBrakeLight.intensity = normalIntensity;
        rightBrakeLight.intensity = normalIntensity;
        
        // Reset lights color
        leftBrakeLight.color = normalColor;
        rightBrakeLight.color = normalColor;
        
        // Reset mesh material emission
        leftBrakeLightMesh.material.SetColor("_EmissionColor", normalColor);
        rightBrakeLightMesh.material.SetColor("_EmissionColor", normalColor);
    }
} 