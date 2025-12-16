using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform gameModel;

    [SerializeField]
    [Tooltip("Subtle rotation angle when steering sideways")]
    float maxTiltAngle = 10f;

    [Header("Movement")]
    [SerializeField]
    float maxForwardSpeed = 30f;

    [SerializeField]
    float forwardAcceleration = 20f;

    [SerializeField]
    float brakeAcceleration = 35f;

    [Header("Steering")]
    [SerializeField]
    float maxLateralSpeed = 3f;

    [SerializeField]
    float lateralResponsiveness = 12f;

    [SerializeField]
    float lateralDrag = 8f;

    [Header("Auto Center")]
    [SerializeField]
    bool autoCenterToRoad = true;

    [SerializeField]
    float roadCenterX = 0f;

    [SerializeField]
    float centerStrength = 6f;

    [SerializeField]
    float centerDamping = 3f;

    [SerializeField]
    [Range(0f, 0.5f)]
    float inputDeadzone = 0.05f;

    // Cache the model's original local rotation so we can tilt relative to it
    Quaternion modelBaseRotation;

    Vector2 input = Vector2.zero;
    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (gameModel == null)
            gameModel = transform;

        modelBaseRotation = gameModel.localRotation;

        // Keep the body from tipping/rotating; we only tilt the visual model.
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
        // Optional but useful for flat-road gameplay
        rb.constraints |= RigidbodyConstraints.FreezePositionY;

        // Default stability
        rb.linearDamping = Mathf.Max(rb.linearDamping, 0.0f);
    }

    void Update()
    {
        // Subtle rotation based on sideways velocity - smoothly interpolated
        float lateralNormalized = (maxLateralSpeed <= 0.001f)
            ? 0f
            : Mathf.Clamp(rb.linearVelocity.x / maxLateralSpeed, -1f, 1f);
        float tiltAngle = lateralNormalized * maxTiltAngle;
        Quaternion targetRotation = modelBaseRotation * Quaternion.Euler(0, tiltAngle, 0);
        gameModel.transform.localRotation = Quaternion.Lerp(
            gameModel.transform.localRotation,
            targetRotation,
            Time.deltaTime * 5f
        );
    }

    void FixedUpdate()
    {
        // Always constrain movement to X/Z plane
        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;

        float steerInput = Mathf.Abs(input.x) < inputDeadzone ? 0f : input.x;
        float throttleInput = Mathf.Abs(input.y) < inputDeadzone ? 0f : input.y;

        ApplyForwardForces(throttleInput, velocity);
        ApplyLateralForces(steerInput, velocity);

        // Clamp speeds (no reverse)
        velocity = rb.linearVelocity;
        velocity.y = 0f;
        velocity.z = Mathf.Clamp(velocity.z, 0f, maxForwardSpeed);
        velocity.x = Mathf.Clamp(velocity.x, -maxLateralSpeed, maxLateralSpeed);
        rb.linearVelocity = velocity;
    }

    void ApplyForwardForces(float throttleInput, Vector3 velocity)
    {
        if (throttleInput > 0f)
        {
            if (velocity.z < maxForwardSpeed)
                rb.AddForce(Vector3.forward * (forwardAcceleration * throttleInput), ForceMode.Acceleration);
        }
        else if (throttleInput < 0f)
        {
            // Brake harder, but never push into reverse
            if (velocity.z > 0f)
                rb.AddForce(Vector3.forward * (brakeAcceleration * throttleInput), ForceMode.Acceleration);
        }
        else
        {
            // Gentle forward drag when not accelerating
            rb.AddForce(Vector3.forward * (-velocity.z * 0.5f), ForceMode.Acceleration);
        }
    }

    void ApplyLateralForces(float steerInput, Vector3 velocity)
    {
        // Target lateral speed from steering input
        float desiredX = steerInput * maxLateralSpeed;
        float deltaX = desiredX - velocity.x;

        // Move toward desired lateral speed
        rb.AddForce(Vector3.right * (deltaX * lateralResponsiveness), ForceMode.Acceleration);

        // Always apply lateral drag to kill drift
        rb.AddForce(Vector3.right * (-velocity.x * lateralDrag), ForceMode.Acceleration);

        // If no steering, softly pull back to the road center so you re-align after going off-road
        if (autoCenterToRoad && Mathf.Abs(steerInput) < 0.001f)
        {
            float error = roadCenterX - rb.position.x;
            float centerAccel = (error * centerStrength) - (velocity.x * centerDamping);
            rb.AddForce(Vector3.right * centerAccel, ForceMode.Acceleration);
        }
    }
    public void SetInput(Vector2 inputVector)
    {
        input = Vector2.ClampMagnitude(inputVector, 1f);
    }
}
