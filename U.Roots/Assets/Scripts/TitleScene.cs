using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    private PlayerInputs _inputs;
    
    void OnEnable()
    {
        _inputs.UI.Enable();
    }
    void OnDisable()
    {
        _inputs.UI.Disable();
    }
    
    void Awake()
    {
        _inputs = new PlayerInputs();
        _inputs.UI.Start.performed += StartGame;
    }

    void StartGame(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene("Steve Scene");
    }
}
