using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform gameModel;

    [SerializeField]
    [Tooltip("Subtle rotation angle when steering sideways")]
    float maxTiltAngle = 10f;

    // Cache the model's original local rotation so we can tilt relative to it
    Quaternion modelBaseRotation;

    float maxSteerVelocity = 2;
    float maxForwardVelocity = 30;

    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 10;
    Vector2 input = Vector2.zero;
    void Start()
    {
        modelBaseRotation = gameModel.localRotation;
    }

    void Update()
    {
        // Subtle rotation based on sideways velocity - smoothly interpolated
        float tiltAngle = Mathf.Clamp(rb.linearVelocity.x, -1f, 1f) * maxTiltAngle;
        Quaternion targetRotation = modelBaseRotation * Quaternion.Euler(0, tiltAngle, 0);
        gameModel.transform.localRotation = Quaternion.Lerp(
            gameModel.transform.localRotation,
            targetRotation,
            Time.deltaTime * 5f
        );
    }

    void FixedUpdate()
    {
        // Always constrain movement to Z-axis (forward) and X-axis (sideways only)
        // Lock Y velocity to prevent flying
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        
        // Keep rotation locked to prevent tilting
        rb.rotation = Quaternion.Euler(0, 0, 0);
        
        if(input.y > 0) Accelarate();
        else rb.linearDamping = 0.2f;

        if(input.y < 0) Brake();
        Steer();
        if(rb.linearVelocity.z <= 0){
            rb.linearVelocity = Vector3.zero;
        }
    }

    void Accelarate()
    {
        rb.linearDamping = 0;
        if(rb.linearVelocity.z >= maxForwardVelocity)
            return;
        // Always push along world Z so the rickshaw moves frontward
        rb.AddForce(Vector3.forward * accelerationMultiplier * input.y);
    }

    void Brake()
    {
        if(rb.linearVelocity.z <= 0)
        {
            return;
        }
         rb.AddForce(Vector3.forward * brakeMultiplier * input.y);
    }
    void Steer()
    {
        if(Mathf.Abs(input.x) > 0)
        {
            float speedBaseSteerLimit = rb.linearVelocity.z / 5.0f;
            speedBaseSteerLimit = Mathf.Clamp01(speedBaseSteerLimit);
            // Sideways force along world X to keep heading consistent
            rb.AddForce(Vector3.right * steeringMultiplier * input.x * speedBaseSteerLimit);
            float normalizedX = rb.linearVelocity.x/maxSteerVelocity;
            normalizedX = Mathf.Clamp(normalizedX,-1.0f,1.0f);
            rb.linearVelocity = new Vector3(normalizedX*maxSteerVelocity,0,rb.linearVelocity.z);
        }
        else{
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity,new Vector3(0,0,rb.linearVelocity.z),Time.fixedDeltaTime * 3);
        }

    }
    public void SetInput(Vector2 inputVector)
    {
        // Don't normalize - we want to keep the input magnitude
        input = inputVector;
    }
}
