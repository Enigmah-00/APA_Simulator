using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    CarHandler carHandler;

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        carHandler.SetInput(input);
    }

    public void OnRestart(InputValue value)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
