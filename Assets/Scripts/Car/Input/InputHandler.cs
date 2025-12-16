using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    CarHandler carHandler;

    [SerializeField]
    [Range(0f, 0.5f)]
    float deadzone = 0.1f;

    void Awake()
    {
        if (carHandler == null)
        {
            carHandler = GetComponentInParent<CarHandler>();
        }
        if (carHandler == null)
        {
            carHandler = GetComponent<CarHandler>();
        }
        if (carHandler == null)
        {
            Debug.LogError("InputHandler: CarHandler reference not set and not found in parent or self.");
        }
    }

    void Update()
    {
        // New Input System restart fallback (works even without a Restart action wired)
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            RestartScene();
    }

    public void OnMove(InputValue value)
    {
        if (carHandler == null)
            return;

        Vector2 input = value.Get<Vector2>();

        if (Mathf.Abs(input.x) < deadzone)
            input.x = 0f;
        if (Mathf.Abs(input.y) < deadzone)
            input.y = 0f;

        carHandler.SetInput(input);
    }

    public void OnRestart(InputValue value)
    {
        RestartScene();
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
