using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    CarHandler carHandler;

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
            carHandler = FindObjectOfType<CarHandler>();
        }
        if (carHandler == null)
        {
            Debug.LogError("InputHandler: CarHandler reference not set and not found in parent or self.");
        }
    }

    public void OnMove(InputValue value)
    {
        if (carHandler == null) return;
        Vector2 input = value.Get<Vector2>();
        carHandler.SetInput(input);
    }

    public void OnRestart(InputValue value)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
