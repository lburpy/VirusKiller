using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInputActions Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = new PlayerInputActions();
            Instance.Enable();
        }
    }
}
