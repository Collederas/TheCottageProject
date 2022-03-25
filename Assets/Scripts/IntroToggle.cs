using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroToggle : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject introCanvas;

    private bool active = true;

    private void Awake()
    {
        playerInput.SwitchCurrentActionMap("Menu");
    }

    private void Update()
    {
        if (inputs.toggleDisclaimer)
        {
            active = !active;
            var actionMap = active ? "Menu" : "Player";

            introCanvas.SetActive(active);
            playerInput.SwitchCurrentActionMap(actionMap);
            inputs.toggleDisclaimer = false;
        }
    }
}
