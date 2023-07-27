using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] private PauseManager _pauseManager;

    public Controls Controls;

    private InputAction _escapePressedAction;

    private void Awake()
    {
        Controls = new Controls();
    }

    private void OnEnable()
    {
        _escapePressedAction = Controls.UI.Cancel;
        _escapePressedAction.Enable();
        _escapePressedAction.performed += EscapePressed;
    }

    private void OnDisable()
    {
        _escapePressedAction.Disable();
    }
    
    private void EscapePressed(InputAction.CallbackContext context)
    {
        _pauseManager.PauseButtonPressed();
    }
}
