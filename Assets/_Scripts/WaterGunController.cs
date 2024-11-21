using UnityEngine;
using UnityEngine.InputSystem;

public class WaterGunController : MonoBehaviour
{
    public ParticleSystem waterEffect;
    public ParticleSystem secondWaterEffect;
    public float waterRecoilForce = 5f;  // Force applied to the car
    public Rigidbody carRigidbody;       // Reference to the car's rigidbody
    
    private PlayerControls controls;
    private bool isWaterActive = false;

    void Awake()
    {
        controls = new PlayerControls();
        
        controls.Gameplay.WaterGunBack.started += _ => StartWater();
        controls.Gameplay.WaterGunBack.canceled += _ => StopWater();
        controls.Gameplay.WaterGunBack.performed += OnWaterGunRotation;
    }


    void FixedUpdate()
    {
        if (isWaterActive && carRigidbody != null)
        {
            // Apply force in the opposite direction of where the water gun is pointing
            Vector3 forceDirection = -transform.forward;
            float carZAngle = Mathf.Abs(NormalizeAngle(carRigidbody.rotation.eulerAngles.z));
            Vector3 force = forceDirection * waterRecoilForce * Mathf.Pow(carZAngle, 0.5f);
            
            // Apply force at the position of the water gun
            carRigidbody.AddForceAtPosition(force, transform.position, ForceMode.Force);
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private float NormalizeAngle(float angle)
    {
        // Normalize the angle to be within -180 to 180 degrees
        if (angle > 180) angle -= 360;
        return angle;
    }
    private void StartWater()
    {
        if (!isWaterActive)
        {
            waterEffect.Play();
            secondWaterEffect.Play();
            isWaterActive = true;
        }
    }

    private void StopWater()
    {
        if (isWaterActive)
        {
            waterEffect.Stop();
            secondWaterEffect.Stop();
            isWaterActive = false;
        }
    }

    private void OnWaterGunRotation(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        
        // Calculate angle using atan2 which takes into account both x and y components
        float rotationAngle = Mathf.Atan2(inputValue.y, inputValue.x) * Mathf.Rad2Deg;
        
        rotationAngle += 90f;
        
        // Apply rotation to the water gun
        transform.localEulerAngles = new Vector3(0, -rotationAngle, 0);
    }
}
