using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
    // Existing variables
    public WheelCollider WheelL; // Left WheelCollider
    public WheelCollider WheelR; // Right WheelCollider
    public Rigidbody carRigidbody;
    public float AntiRoll = 5000.0f; // Anti-roll force


    // New FixedUpdate method to apply anti-roll force
    void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

        bool groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedL)
            carRigidbody.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);   
        if (groundedR)
            carRigidbody.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position);   
    }
}
