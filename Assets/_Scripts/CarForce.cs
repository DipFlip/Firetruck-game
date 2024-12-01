using UnityEngine;
using UnityEngine.InputSystem;

public class CarForce : MonoBehaviour
{
    public float downwardForce = 10f;
    public float jumpForce = 5f;
    public float jumpOffset = 1f; // Distance from center where force is applied
    public float uprightingForce = 5f; // New variable for uprighting force
    public ParticleSystem frontJumpExplosion; // Particle system for front jump explosion
    public ParticleSystem backJumpExplosion;  // Particle system for back jump explosion
    private Rigidbody rb;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        // Setup jump input handlers
        controls.Gameplay.JumpFront.performed += ctx => ApplyJumpForce(true);
        controls.Gameplay.JumpBack.performed += ctx => ApplyJumpForce(false);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // void FixedUpdate()
    // {
    //     // Apply downward force for stability
    //     rb.AddForce(Vector3.down * downwardForce);
    //     float zAngle = NormalizeAngle(rb.rotation.eulerAngles.z); // Normalize the angle
    //     float xAngle = NormalizeAngle(rb.rotation.eulerAngles.x); // Normalize the angle
    //     // Apply uprighting force to prevent the car from falling over
    //     if ((Mathf.Abs(zAngle) > 10f) && (Mathf.Abs(xAngle) < 20f))
    //     {
    //         // Apply uprighting torque along the forward axis to prevent horizontal rotation
    //         rb.AddTorque(transform.forward * uprightingForce * Mathf.Pow(zAngle, 1), ForceMode.Acceleration);
    //     }
    //     else if ((Mathf.Abs(zAngle) > 100f) && (Mathf.Abs(xAngle) < 30f))
    //     {
    //         // Apply uprighting torque along the forward axis to prevent horizontal rotation
    //         rb.AddTorque(transform.forward * uprightingForce * 10 * Mathf.Pow(zAngle, 1), ForceMode.Acceleration);
    //     }
    // }

    private float NormalizeAngle(float angle)
    {
        // Normalize the angle to be within -180 to 180 degrees
        if (angle > 180) angle -= 360;
        return angle;
    }

    private void ApplyJumpForce(bool isFront)
    {
        // Calculate the position where to apply the force
        Vector3 forcePosition = transform.position + (isFront ? -transform.forward : transform.forward) * jumpOffset;
        
        // Play the corresponding explosion particle system
        if (isFront)
        {
            frontJumpExplosion.Play(); // Play front jump explosion
        }
        else
        {
            backJumpExplosion.Play(); // Play back jump explosion
        }

        // Apply the upward force at the calculated position
        rb.AddForceAtPosition(transform.up * jumpForce, forcePosition, ForceMode.Impulse);
    }
}
