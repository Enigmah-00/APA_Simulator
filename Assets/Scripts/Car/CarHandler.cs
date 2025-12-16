using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 10;
    Vector2 input = Vector2.zero;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(input.y > 0) Accelarate();
        else rb.linearDamping = 0.2f;

        if(input.y < 0) Brake();
        Steer();
    }

    void Accelarate()
    {
        rb.linearDamping = 0;
        rb.AddForce(rb.transform.forward * accelerationMultiplier * input.y);
    }

    void Brake()
    {
        if(rb.linearVelocity.z <= 0)
        {
            return;
        }
         rb.AddForce(rb.transform.forward * brakeMultiplier * input.y);
    }
    void Steer()
    {
        if(Mathf.Abs(input.x) > 0)
        {
            rb.AddForce(rb.transform.right * steeringMultiplier * input.x);
        }
    }
    public void SetInput(Vector2 inputVector)
    {
        // Don't normalize - we want to keep the input magnitude
        input = inputVector;
    }
}
