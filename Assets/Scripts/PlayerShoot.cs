using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;
    public float bulletLifeTime = 1f;

    private PlayerInputActions inputActions;

    private void Awake() {
        inputActions = InputManager.Instance;
    }

    private void OnEnable() {
        inputActions.Player.Shoot.performed += Shoot;
    }

    private void OnDisable() {
        inputActions.Player.Shoot.performed -= Shoot;
    }

    private void Shoot(InputAction.CallbackContext context) {

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        if (bullet != null) { Destroy(bullet, bulletLifeTime); }
    }
}
