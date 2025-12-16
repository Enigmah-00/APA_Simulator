using UnityEngine;
using Unity.Cinemachine;

public class CustomCameraAvoidance : CinemachineExtension
{
    [Tooltip("How far down to push camera when obstacle detected")]
    public float downwardOffset = 3f;
    
    [Tooltip("How far forward to pull camera")]
    public float forwardPull = 2f;
    
    [Tooltip("Layers to check for obstacles")]
    public LayerMask obstacleLayer = ~0;
    
    [Tooltip("Camera collision radius")]
    public float cameraRadius = 0.4f;
    
    [Tooltip("Smoothing speed")]
    public float smoothSpeed = 3f;

    private float currentDownOffset = 0f;
    private float currentForwardOffset = 0f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, 
        ref CameraState state, 
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            if (vcam.Follow == null) return;
            
            Vector3 cameraPos = state.GetFinalPosition();
            Vector3 targetPos = vcam.Follow.position;
            Vector3 direction = cameraPos - targetPos;
            float distance = direction.magnitude;

            // Check if there's an obstacle between camera and target
            if (Physics.SphereCast(targetPos, cameraRadius, direction.normalized, 
                out RaycastHit hit, distance, obstacleLayer))
            {
                // Push camera DOWN and FORWARD to go under obstacle
                currentDownOffset = Mathf.Lerp(currentDownOffset, downwardOffset, 
                    deltaTime * smoothSpeed);
                currentForwardOffset = Mathf.Lerp(currentForwardOffset, forwardPull, 
                    deltaTime * smoothSpeed);
            }
            else
            {
                // Return to normal position
                currentDownOffset = Mathf.Lerp(currentDownOffset, 0f, 
                    deltaTime * smoothSpeed);
                currentForwardOffset = Mathf.Lerp(currentForwardOffset, 0f, 
                    deltaTime * smoothSpeed);
            }

            // Apply offsets - DOWN and FORWARD toward target
            state.PositionCorrection += Vector3.down * currentDownOffset;
            state.PositionCorrection += direction.normalized * currentForwardOffset;
        }
    }
}
